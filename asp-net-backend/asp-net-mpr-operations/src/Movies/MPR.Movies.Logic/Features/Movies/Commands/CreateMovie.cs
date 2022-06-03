using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MPR.Movies.Logic.Abstractions;
using MPR.Movies.Logic.Errors;
using MPR.Movies.Logic.Features.Movies.Extensions;
using MPR.Movies.Logic.Features.Movies.Responses;
using MPR.Shared.Domain.Models;
using MPR.Shared.Logic.Responses;
using NJsonSchema.Annotations;

namespace MPR.Movies.Logic.Features.Movies.Commands
{
    public class CreateMovie
    {
        [JsonSchema("CreateMovieCommand")]
        public class Command : IRequest<Response<MovieResponse>>
        {
            public int? TheMovieDbId { get; set; }
            public string Title { get; set; }
            public string Language { get; set; }
            public string Poster { get; set; }
            public int Duration { get; set; }
            public string TagLine { get; set; }
            public string Trailer { get; set; }
            public IList<string> GenreNames { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {

        }

        public class Handler : IRequestHandler<Command, Response<MovieResponse>>
        {
            private readonly IMprMoviesDbContext _context;

            public Handler(IMprMoviesDbContext context)
            {
                _context = context;
            }

            public async Task<Response<MovieResponse>> Handle(Command request, CancellationToken cancellationToken)
            {
                var movieExists = _context.Movies.Any(x => x.TheMovieDbId == request.TheMovieDbId || x.Title == request.Title);

                if (movieExists)
                {
                    return Response.CreateBadRequestResponse<MovieResponse>(ErrorCodes.MOVIE_ALREADYEXISTS,
                        $"Movie with TheMovieDb Id {request.TheMovieDbId} already exists");
                }

                var genres = await _context.Genres.Where(x => request.GenreNames.Any(y => y == x.Name)).ToListAsync(cancellationToken);

                var newMovie = new Movie
                {
                    Title = request.Title,
                    Language = request.Language,
                    Duration = TimeSpan.FromMinutes(request.Duration),
                    Poster = request.Poster,
                    Trailer = request.Trailer,
                    TagLine = request.TagLine,
                    TheMovieDbId = request.TheMovieDbId,
                    Genres = genres,
                };

                await _context.Movies.AddAsync(newMovie, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                return new Response<MovieResponse> { Payload = newMovie.ToResponse() };
            }
        }
    }
}
