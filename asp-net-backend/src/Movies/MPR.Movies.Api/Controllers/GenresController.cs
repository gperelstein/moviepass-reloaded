using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MPR.Movies.Logic.Features.Genres.Commands;
using MPR.Shared.Domain.Authorization;
using MPR.Shared.Logic.Responses.Features.Genres;
using NSwag.Annotations;
using System.Net;

namespace MPR.Movies.Api.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    [Authorize(Roles = RoleCodes.ADMIN)]
    public class GenresController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GenresController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /*[HttpGet]
        [Produces("application/json")]
        [OpenApiOperation(
            summary: "Creates a new movie",
            description: "Creates a new movie and return it"
        )]
        [SwaggerResponse(HttpStatusCode.OK, typeof(GenreResponse), Description = "Ok")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, typeof(ValidationProblemDetails), Description = "Error while processing the request")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, null, Description = "User must be authenticated")]
        public async Task<ActionResult<GenreResponse>> ListGenres(CreateGenre.Command command)
        {
            var response = await _mediator.Send(command);

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, response.ValidationIssue);
            }

            return Ok(response.Payload);
        }*/

        [HttpPost]
        [Produces("application/json")]
        [OpenApiOperation(
            summary: "Creates a new movie",
            description: "Creates a new movie and return it"
        )]
        [SwaggerResponse(HttpStatusCode.OK, typeof(GenreResponse), Description = "Ok")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, typeof(ValidationProblemDetails), Description = "Error while processing the request")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, null, Description = "User must be authenticated")]
        public async Task<ActionResult<GenreResponse>> CreateGenre(CreateGenre.Command command)
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
