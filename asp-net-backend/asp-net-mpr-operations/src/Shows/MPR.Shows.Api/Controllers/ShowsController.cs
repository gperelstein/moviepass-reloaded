using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MPR.Shared.Domain.Authorization;
using MPR.Shared.Logic.Pagination;
using MPR.Shared.Logic.Responses.Features.Shows;
using MPR.Shows.Logic.Features.Shows.Commands;
using MPR.Shows.Logic.Features.Shows.Queries;
using NSwag.Annotations;
using System.Net;

namespace MPR.Shows.Api.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    [Authorize(Roles = RoleCodes.ADMIN)]
    public class ShowsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ShowsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Produces("application/json")]
        [OpenApiOperation(
            summary: "Retrieves a list of shows",
            description: "Retrieves a list of shows"
        )]
        [SwaggerResponse(HttpStatusCode.OK, typeof(PaginatedResult<ShowResponse>), Description = "Ok")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, typeof(ValidationProblemDetails), Description = "Error while processing the request")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, null, Description = "User must be authenticated")]
        public async Task<ActionResult<PaginatedResult<ShowResponse>>> ListMovies([FromQuery] ListShows.Query query)
        {
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
            summary: "Creates a new show",
            description: "Creates a new show and return it"
        )]
        [SwaggerResponse(HttpStatusCode.OK, typeof(ShowDetailedResponse), Description = "Ok")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, typeof(ValidationProblemDetails), Description = "Error while processing the request")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, null, Description = "User must be authenticated")]
        public async Task<ActionResult<ShowDetailedResponse>> CreateCinema([FromBody] CreateShows.Command command)
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
