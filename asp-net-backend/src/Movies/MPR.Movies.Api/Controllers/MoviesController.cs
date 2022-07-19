using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MPR.Movies.Logic.Features.Movies.Commands;
using MPR.Movies.Logic.Features.Movies.Queries;
using MPR.Shared.Domain.Authorization;
using MPR.Shared.Logic.Pagination;
using MPR.Shared.Logic.Responses.Features.Movies;
using NSwag.Annotations;
using System.Net;

namespace MPR.Movies.Api.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    [Authorize(Roles = RoleCodes.ADMIN)]
    public class MoviesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MoviesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Produces("application/json")]
        [OpenApiOperation(
            summary: "Retrieves a list of movies",
            description: "Retrieves a list of movies"
        )]
        [SwaggerResponse(HttpStatusCode.OK, typeof(PaginatedResult<ListMoviesResponse>), Description = "Ok")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, typeof(ValidationProblemDetails), Description = "Error while processing the request")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, null, Description = "User must be authenticated")]
        public async Task<ActionResult<PaginatedResult<ListMoviesResponse>>> ListMovies([FromQuery] ListMovies.Query query)
        {
            var response = await _mediator.Send(query);

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, response.ValidationIssue);
            }

            return Ok(response.Payload);
        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        [OpenApiOperation(
            summary: "Gets a movie",
            description: "Gets a movie"
        )]
        [SwaggerResponse(HttpStatusCode.OK, typeof(MovieResponse), Description = "Ok")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, typeof(ValidationProblemDetails), Description = "Error while processing the request")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, null, Description = "User must be authenticated")]
        public async Task<ActionResult<MovieResponse>> GetMovies(Guid id)
        {
            var query = new GetMovie.Query { Id = id };

            var response = await _mediator.Send(query);

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, response.ValidationIssue);
            }

            return Ok(response.Payload);
        }

        [HttpPost]
        [Produces("application/json")]
        [OpenApiOperation(
            summary: "Creates a new movie",
            description: "Creates a new movie and return it"
        )]
        [SwaggerResponse(HttpStatusCode.OK, typeof(MovieResponse), Description = "Ok")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, typeof(ValidationProblemDetails), Description = "Error while processing the request")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, null, Description = "User must be authenticated")]
        public async Task<ActionResult<MovieResponse>> CreateMovie(CreateMovie.Command command)
        {
            var response = await _mediator.Send(command);

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, response.ValidationIssue);
            }

            return Ok(response.Payload);
        }
    }
}
