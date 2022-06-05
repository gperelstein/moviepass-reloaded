using FluentValidation;
using MediatR;
using MPR.Shared.Domain.Models;
using MPR.Shared.Logic.Abstractions;
using MPR.Shared.Logic.Responses;
using MPR.Shared.Logic.Responses.Features.Cinemas;
using MPR.Shared.Logic.Responses.Features.Movies;
using MPR.Shared.Logic.Responses.Features.Shows;
using MPR.Shows.Logic.Abstractions;
using MPR.Shows.Logic.Features.Shows.Extensions;
using NJsonSchema.Annotations;
using System.Net.Http.Headers;
using System.Text.Json;

namespace MPR.Shows.Logic.Features.Shows.Commands
{
    public class CreateShow
    {
        [JsonSchema("CreateShowCommand")]
        public class Command : IRequest<Response<ShowResponse>>
        {
            public DateTime StartAt { get; set; }
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

        public class Handler : IRequestHandler<Command, Response<ShowResponse>>
        {
            //private readonly IMprShowsDbContext _context;
            private readonly IHttpClientFactory _httpClientFactory;
            private readonly ICurrentUserService _currentUserService;

            public Handler(IHttpClientFactory httpClientFactory, ICurrentUserService currentUserService)
            {
                //_context = context;
                _httpClientFactory = httpClientFactory;
                _currentUserService = currentUserService;
            }

            public async Task<Response<ShowResponse>> Handle(Command request, CancellationToken cancellationToken)
            {
                var token = _currentUserService.GetToken();
                var movie = await GetMovie(token, request.MovieId, cancellationToken);
                var room = await GetRoom(token, request.CinemaId, request.RoomId, cancellationToken);
                /*await Task.WhenAll(movieTask, roomTask);
                var movie = movieTask.Result;
                var room = roomTask.Result;*/

                var show = new Show
                {
                    StartAt = request.StartAt,
                    EndAt = request.StartAt.AddMinutes(movie.Duration).AddMinutes(15),
                    MovieId = movie.Id,
                    RoomId = room.Id,
                };

                //await _context.Shows.AddAsync(show, cancellationToken);
                //await _context.SaveChangesAsync(cancellationToken);

                return new Response<ShowResponse> { Payload = show.ToResponse(movie, room) };
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
                var roomHttpResponse = await httpClient.GetAsync($"https://localhost:5020/Cinemas/{cinemaId}/Rooms/{roomId}", cancellationToken);
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
        }
    }
}
