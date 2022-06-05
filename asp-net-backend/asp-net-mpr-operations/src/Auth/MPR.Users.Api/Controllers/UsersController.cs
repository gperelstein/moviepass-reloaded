using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MPR.Shared.Domain.Authorization;
using MPR.Shared.Logic.Responses.Features.Users;
using MPR.Users.Logic.Features.Users.Commands;
using NSwag.Annotations;
using System.Net;

namespace MPR.Users.Api.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    [Authorize(Roles = RoleCodes.ADMIN)]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("CreateAdmin")]
        [Produces("application/json")]
        [OpenApiOperation(
            summary: "Retrieves a list of movies",
            description: "Retrieves a list of movies"
        )]
        [SwaggerResponse(HttpStatusCode.OK, typeof(UserResponse), Description = "Ok")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, typeof(ValidationProblemDetails), Description = "Error while processing the request")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, null, Description = "User must be authenticated")]
        public async Task<ActionResult<UserResponse>> CreateAdmin([FromBody] CreateAdmin.Command request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, response.ValidationIssue);
            }

            return Ok(response.Payload);
        }

        [AllowAnonymous]
        [HttpPost("CreateRegularUser")]
        [Produces("application/json")]
        [OpenApiOperation(
            summary: "Retrieves a list of movies",
            description: "Retrieves a list of movies"
        )]
        [SwaggerResponse(HttpStatusCode.OK, typeof(UserResponse), Description = "Ok")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, typeof(ValidationProblemDetails), Description = "Error while processing the request")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, null, Description = "User must be authenticated")]
        public async Task<ActionResult<UserResponse>> CreateRegularUser([FromBody] CreateRegularUser.Command request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, response.ValidationIssue);
            }

            return Ok(response.Payload);
        }

        [AllowAnonymous]
        [HttpPost("RegisterUser")]
        [Produces("application/json")]
        [OpenApiOperation(
            summary: "Register a user",
            description: "Complete the registration of a user"
        )]
        [SwaggerResponse(HttpStatusCode.NoContent, typeof(void), Description = "Ok")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, typeof(ValidationProblemDetails), Description = "Error while processing the request")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, null, Description = "User must be authenticated")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUser.Command request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, response.ValidationIssue);
            }

            return Ok(response.Payload);
        }

        [AllowAnonymous]
        [HttpPost("InitiatePasswordReset")]
        [Produces("application/json")]
        [OpenApiOperation(
            summary: "Initiate the reset of the user password",
            description: "Initiate the reset of the user password"
        )]
        [SwaggerResponse(HttpStatusCode.NoContent, typeof(void), Description = "Ok")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, typeof(ValidationProblemDetails), Description = "Error while processing the request")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, null, Description = "User must be authenticated")]
        public async Task<IActionResult> InitiatePasswordReset([FromBody] InitiatePasswordReset.Command request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, response.ValidationIssue);
            }

            return NoContent();
        }

        [AllowAnonymous]
        [HttpPost("ResetPassword")]
        [Produces("application/json")]
        [OpenApiOperation(
            summary: "Reset the user password",
            description: "Reset the user password"
        )]
        [SwaggerResponse(HttpStatusCode.NoContent, typeof(void), Description = "Ok")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, typeof(ValidationProblemDetails), Description = "Error while processing the request")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, null, Description = "User must be authenticated")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPassword.Command request, CancellationToken cancellationToken)
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
