﻿using FluentValidation;
using MediatR;
using MPR.Movies.Logic.Abstractions;
using MPR.Movies.Logic.Errors;
using MPR.Movies.Logic.Features.Genres.Extensions;
using MPR.Movies.Logic.Features.Genres.Responses;
using MPR.Shared.Domain.Models;
using MPR.Shared.Logic.Responses;

namespace MPR.Movies.Logic.Features.Genres.Commands
{
    public class CreateGenre
    {
        public class Command : IRequest<Response<GenreResponse>>
        {
            public int? TheMovieDbId { get; set; }
            public string Name { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(c => c.Name)
                    .NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Command, Response<GenreResponse>>
        {
            private readonly IMprMoviesDbContext _context;

            public Handler(IMprMoviesDbContext context)
            {
                _context = context;
            }

            public async Task<Response<GenreResponse>> Handle(Command request, CancellationToken cancellationToken)
            {
                var genreExists = _context.Genres.ToList().Any(x => x.Name.Equals(request.Name, StringComparison.OrdinalIgnoreCase));
                if (genreExists)
                {
                    Response.CreateBadRequestResponse(ErrorCodes.GENRE_ALREADYEXISTS,
                        $"Genre with TheMovieDb Id {request.TheMovieDbId} already exists");
                }

                var genre = new Genre
                {
                    TheMovieDbId = request.TheMovieDbId,
                    Name = request.Name
                };
                await _context.Genres.AddAsync(genre, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                return new Response<GenreResponse> { Payload = genre.ToResponse() };
            }
        }
    }
}