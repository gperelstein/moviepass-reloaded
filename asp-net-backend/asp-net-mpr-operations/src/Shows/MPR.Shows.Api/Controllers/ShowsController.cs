using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MPR.Shared.Domain.Authorization;
using MPR.Shared.Logic.Responses.Features.Shows;
using MPR.Shows.Logic.Features.Shows.Commands;
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

        [HttpPost]
        [Produces("application/json")]
        [OpenApiOperation(
            summary: "Creates a new cinema",
            description: "Creates a new cinema and return it"
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
