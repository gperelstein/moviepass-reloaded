using FluentValidation;
using MediatR;
using MPR.Shared.Domain.Models;
using MPR.Shared.Logic.Abstractions;
using MPR.Shared.Logic.Responses;
using MPR.Shared.Logic.Responses.Features.Cinemas;
using MPR.Shared.Logic.Responses.Features.Movies;
using MPR.Shared.Logic.Responses.Features.Shows;
using MPR.Shows.Logic.Abstractions;
using MPR.Shows.Logic.Errors;
using MPR.Shows.Logic.Features.Shows.Extensions;
using NJsonSchema.Annotations;
using System.Net.Http.Headers;
using System.Text.Json;

namespace MPR.Shows.Logic.Features.Shows.Commands
{
    public class CreateShows
    {
        [JsonSchema("CreateShowsCommand")]
        public class Command : IRequest<Response<List<ShowDetailedResponse>>>
        {
            public List<DateTime> StartAt { get; set; }
            public Guid MovieId { get; set; }
            public Guid CinemaId { get; set; }
            public Guid RoomId { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                /*RuleFor(x => x.StartAt).NotEmpty();
                RuleFor(x => x.RoomId).NotEmpty();
                RuleFor(x => x.MovieId).NotEmpty();
                RuleFor(x => x.CinemaId).NotEmpty();*/
            }
        }

        public class Handler : IRequestHandler<Command, Response<List<ShowDetailedResponse>>>
        {
            private readonly IMprShowsDbContext _context;
            private readonly IHttpClientFactory _httpClientFactory;
            private readonly ICurrentUserService _currentUserService;

            public Handler(IMprShowsDbContext context,
                IHttpClientFactory httpClientFactory,
                ICurrentUserService currentUserService)
            {
                _context = context;
                _httpClientFactory = httpClientFactory;
                _currentUserService = currentUserService;
            }

            public async Task<Response<List<ShowDetailedResponse>>> Handle(Command request, CancellationToken cancellationToken)
            {
                var token = _currentUserService.GetToken();
                var movie = await GetMovie(token, request.MovieId, cancellationToken);
                if(movie == null)
                {
                    return Response.CreateBadRequestResponse<List<ShowDetailedResponse>>(ErrorCodes.MOVIE_NOTEXISTS,
                        $"Movie with Id {request.MovieId} not exists");
                }


                var room = await GetRoom(token, request.CinemaId, request.RoomId, cancellationToken);
                if (room == null)
                {
                    return Response.CreateBadRequestResponse<List<ShowDetailedResponse>>(ErrorCodes.ROOM_NOTEXISTS,
                        $"Room with Id {request.RoomId} not exists");
                }

                var newShows = request.StartAt.Select(x => new Show
                {
                    StartAt = x,
                    EndAt = x.AddMinutes(movie.Duration).AddMinutes(15),
                    MovieId = movie.Id,
                    RoomId = room.Id,
                    CinemaId = request.CinemaId,
                }).ToList();

                var minStartDate = newShows.Min(y => y.StartAt);
                var maxEndDate = newShows.Max(y => y.EndAt);
                var existingShows = _context.Shows
                    .Where(x => x.RoomId == request.RoomId)
                    .Where(x => x.StartAt <= maxEndDate && x.EndAt >= minStartDate)
                    .ToList();

                var areShowsValid = ValidateShows(newShows, existingShows);

                if(!areShowsValid)
                {
                    return Response.CreateBadRequestResponse<List<ShowDetailedResponse>>(ErrorCodes.SHOW_DATETIME_OVERLAPS,
                        $"At least one of the shows is overlaping with another");
                }

                await _context.Shows.AddRangeAsync(newShows, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                return new Response<List<ShowDetailedResponse>>
                {
                    Payload = newShows.Select(x => x.ToDetailedResponse(movie, room)).ToList()
                };
            }

            private async Task<MovieResponse> GetMovie(string token, Guid movieId, CancellationToken cancellationToken)
            {
                var httpClient = _httpClientFactory.CreateClient();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var movieHttpResponse = await httpClient.GetAsync($"https://localhost:5010/Movies/{movieId}", cancellationToken);
                if (!movieHttpResponse.IsSuccessStatusCode)
                {
                    return null;
                }
                var movieString = await movieHttpResponse.Content.ReadAsStringAsync(cancellationToken);
                var movie = JsonSerializer.Deserialize<MovieResponse>(movieString, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return movie;
            }

            private async Task<RoomResponse> GetRoom(string token, Guid cinemaId, Guid roomId, CancellationToken cancellationToken)
            {
                var httpClient = _httpClientFactory.CreateClient();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var roomHttpResponse = await httpClient
                    .GetAsync($"https://localhost:5020/Cinemas/{cinemaId}/Rooms/{roomId}", cancellationToken);
                if (!roomHttpResponse.IsSuccessStatusCode)
                {
                    return null;
                }
                var roomString = await roomHttpResponse.Content.ReadAsStringAsync(cancellationToken);
                var room = JsonSerializer.Deserialize<RoomResponse>(roomString, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return room;
            }

            private bool ValidateShows(List<Show> newShows, List<Show> existingShows)
            {
                existingShows.AddRange(newShows);
                var orderedShows = existingShows.OrderBy(x => x.StartAt).ToList();
                return !orderedShows.Any(x => orderedShows
                                                .Where(y => y != x)
                                                .Any(y => y.EndAt >= x.StartAt && y.StartAt <= x.EndAt));
            }
        }
    }
}
