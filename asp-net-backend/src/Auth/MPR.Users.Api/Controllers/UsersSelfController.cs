using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MPR.Shared.Logic.Responses.Features.Users;
using MPR.Users.Logic.Features.UsersSelf.Commands;
using MPR.Users.Logic.Features.UsersSelf.Queries;
using NSwag.Annotations;
using System.Net;

namespace MPR.Users.Api.Controllers
{
    [ApiController]
    [Route("Users/Self")]
    [Authorize]
    public class UsersSelfController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersSelfController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("Profile")]
        [Produces("application/json")]
        [OpenApiOperation(
            summary: "Retrive the user profile",
            description: "Retrive the user profile"
        )]
        [SwaggerResponse(HttpStatusCode.OK, typeof(ProfileResponse), Description = "Ok")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, typeof(ValidationProblemDetails), Description = "Error while processing the request")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, null, Description = "User must be authenticated")]
        public async Task<IActionResult> GetProfile(CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetProfile.Command(), cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, response.ValidationIssue);
            }

            return Ok(response.Payload);
        }

        [HttpPut("Profile")]
        [Produces("application/json")]
        [OpenApiOperation(
            summary: "Update the user profile",
            description: "Update the user profile"
        )]
        [SwaggerResponse(HttpStatusCode.OK, typeof(ProfileResponse), Description = "Ok")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, typeof(ValidationProblemDetails), Description = "Error while processing the request")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, null, Description = "User must be authenticated")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfile.Command request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, response.ValidationIssue);
            }

            return Ok(response.Payload);
        }

        [HttpPut("ChangePassword")]
        [Produces("application/json")]
        [OpenApiOperation(
            summary: "Change the user password",
            description: "Change the user password"
        )]
        [SwaggerResponse(HttpStatusCode.NoContent, typeof(ProfileResponse), Description = "Ok")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, typeof(ValidationProblemDetails), Description = "Error while processing the request")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, null, Description = "User must be authenticated")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePassword.Command request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, response.ValidationIssue);
            }

            return NoContent();
        }
    }
}
