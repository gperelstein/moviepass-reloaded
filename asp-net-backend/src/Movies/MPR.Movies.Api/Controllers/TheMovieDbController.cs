using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MPR.Movies.Logic.Features.TheMovieDb.Queries;
using MPR.Movies.Logic.Features.TheMovieDb.Responses;
using MPR.Shared.Domain.Authorization;
using MPR.Shared.Logic.Pagination;
using NSwag.Annotations;
using System.Net;

namespace MPR.Movies.Api.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    [Authorize(Roles = RoleCodes.ADMIN)]
    public class TheMovieDbController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TheMovieDbController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("Genres")]
        [Produces("application/json")]
        [OpenApiOperation(
            summary: "Retrieves a list of genres in The movies Db",
            description: "Retrieves a list of genres in The movies Db"
        )]
        [SwaggerResponse(HttpStatusCode.OK, typeof(List<GenreTmdbResponse>), Description = "Ok")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, typeof(ValidationProblemDetails), Description = "Error while processing the request")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, null, Description = "User must be authenticated")]
        public async Task<ActionResult<List<GenreTmdbResponse>>> ListGenresTmdb()
        {
            var query = new ListGenresTmdb.Query();
            var response = await _mediator.Send(query);

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, response.ValidationIssue);
            }

            return Ok(response.Payload);
        }

        [HttpGet("Movies")]
        [Produces("application/json")]
        [OpenApiOperation(
            summary: "Retrieves a list of movies in The movies Db",
            description: "Retrieves a list of movies in The movies Db"
        )]
        [SwaggerResponse(HttpStatusCode.OK, typeof(PaginatedResult<MovieTmdbResponse>), Description = "Ok")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, typeof(ValidationProblemDetails), Description = "Error while processing the request")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, null, Description = "User must be authenticated")]
        public async Task<ActionResult<PaginatedResult<MovieTmdbResponse>>> ListMoviesTmdb([FromQuery] int pageNumber)
        {
            var query = new ListMoviesTmdb.Query { PageNumber = pageNumber };
            var response = await _mediator.Send(query);

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, response.ValidationIssue);
            }

            return Ok(response.Payload);
        }

        [HttpGet("Movie/{theMovieDbId}")]
        [Produces("application/json")]
        [OpenApiOperation(
            summary: "Retrieves a list of movies in The movies Db",
            description: "Retrieves a list of movies in The movies Db"
        )]
        [SwaggerResponse(HttpStatusCode.OK, typeof(MovieDetailsTmdbResponse), Description = "Ok")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, typeof(ValidationProblemDetails), Description = "Error while processing the request")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, null, Description = "User must be authenticated")]
        public async Task<ActionResult<MovieDetailsTmdbResponse>> GetMovieDetailsTmdb([FromRoute] int theMovieDbId)
        {
            var query = new GetMovieTmdb.Query { TheMovieDbId = theMovieDbId };
            var response = await _mediator.Send(query);

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, response.ValidationIssue);
            }

            return Ok(response.Payload);
        }
    }
}
