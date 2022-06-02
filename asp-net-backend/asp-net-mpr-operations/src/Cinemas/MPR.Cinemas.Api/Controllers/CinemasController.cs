using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MPR.Cinemas.Logic.Features.Cinemas.Commands;
using MPR.Cinemas.Logic.Features.Cinemas.Queries;
using MPR.Cinemas.Logic.Features.Cinemas.Responses;
using MPR.Shared.Logic.Pagination;
using NSwag.Annotations;
using System.Net;

namespace MPR.Cinemas.Api.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    [Authorize]
    public class CinemasController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CinemasController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region Queries

        [HttpGet]
        [Produces("application/json")]
        [OpenApiOperation(
            summary: "Retrieves a list of cinemas",
            description: "Retrieves a list of cinemas"
        )]
        [SwaggerResponse(HttpStatusCode.OK, typeof(PaginatedResult<CinemaResponse>), Description = "Ok")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, typeof(ValidationProblemDetails), Description = "Error while processing the request")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, null, Description = "User must be authenticated")]
        public async Task<ActionResult<PaginatedResult<CinemaResponse>>> ListCinemas([FromQuery] ListCinemas.Query query)
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
            summary: "Gets a cinema",
            description: "Gets a cinema"
        )]
        [SwaggerResponse(HttpStatusCode.OK, typeof(CinemaResponse), Description = "Ok")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, typeof(ValidationProblemDetails), Description = "Error while processing the request")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, null, Description = "User must be authenticated")]
        public async Task<ActionResult<CinemaResponse>> GetCinema(Guid id)
        {
            var query = new GetCinema.Query { Id = id };

            var response = await _mediator.Send(query);

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, response.ValidationIssue);
            }

            return Ok(response.Payload);
        }

        [HttpGet("{id}/Rooms")]
        [Produces("application/json")]
        [OpenApiOperation(
            summary: "Retrieves a list of rooms",
            description: "Retrieves a list of rooms"
        )]
        [SwaggerResponse(HttpStatusCode.OK, typeof(List<RoomResponse>), Description = "Ok")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, typeof(ValidationProblemDetails), Description = "Error while processing the request")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, null, Description = "User must be authenticated")]
        public async Task<ActionResult<List<RoomResponse>>> ListRooms(Guid id)
        {
            var query = new ListRooms.Query { CinemaId = id };

            var response = await _mediator.Send(query);

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, response.ValidationIssue);
            }

            return Ok(response.Payload);
        }

        [HttpGet("{id}/Rooms/{roomId}")]
        [Produces("application/json")]
        [OpenApiOperation(
            summary: "Gets a room",
            description: "Gets a room"
        )]
        [SwaggerResponse(HttpStatusCode.OK, typeof(RoomResponse), Description = "Ok")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, typeof(ValidationProblemDetails), Description = "Error while processing the request")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, null, Description = "User must be authenticated")]
        public async Task<ActionResult<RoomResponse>> GetRoom([FromRoute] Guid id, Guid roomId)
        {
            var query = new GetRoom.Query { CinemaId = id, RoomId = roomId };

            var response = await _mediator.Send(query);

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, response.ValidationIssue);
            }

            return Ok(response.Payload);
        }

        #endregion

        #region Commands

        [HttpPost]
        [Produces("application/json")]
        [OpenApiOperation(
            summary: "Creates a new cinema",
            description: "Creates a new cinema and return it"
        )]
        [SwaggerResponse(HttpStatusCode.OK, typeof(CinemaResponse), Description = "Ok")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, typeof(ValidationProblemDetails), Description = "Error while processing the request")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, null, Description = "User must be authenticated")]
        public async Task<ActionResult<CinemaResponse>> CreateCinema([FromBody] CreateCinema.Command command)
        {
            var response = await _mediator.Send(command);

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, response.ValidationIssue);
            }

            return Ok(response.Payload);
        }

        [HttpPut("{id}")]
        [Produces("application/json")]
        [OpenApiOperation(
            summary: "Updates a cinema",
            description: "Updates a cinema and return it"
        )]
        [SwaggerResponse(HttpStatusCode.OK, typeof(CinemaResponse), Description = "Ok")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, typeof(ValidationProblemDetails), Description = "Error while processing the request")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, null, Description = "User must be authenticated")]
        public async Task<ActionResult<CinemaResponse>> UpdateCinema(Guid id, [FromBody] UpdateCinema.Command command)
        {
            command.Id = id;

            var response = await _mediator.Send(command);

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, response.ValidationIssue);
            }

            return Ok(response.Payload);
        }

        [HttpDelete("{id}")]
        [Produces("application/json")]
        [OpenApiOperation(
            summary: "Deletes a cinema",
            description: "Deletes a cinema"
        )]
        [SwaggerResponse(HttpStatusCode.NoContent, typeof(void), Description = "Ok")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, typeof(ValidationProblemDetails), Description = "Error while processing the request")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, null, Description = "User must be authenticated")]
        public async Task<ActionResult> DeleteCinema(Guid id)
        {
            var command = new DeleteCinema.Command { Id = id };

            var response = await _mediator.Send(command);

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, response.ValidationIssue);
            }

            return NoContent();
        }

        [HttpPost("{id}/Rooms")]
        [Produces("application/json")]
        [OpenApiOperation(
            summary: "Creates a new room",
            description: "Creates a new room and return it"
        )]
        [SwaggerResponse(HttpStatusCode.OK, typeof(RoomResponse), Description = "Ok")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, typeof(ValidationProblemDetails), Description = "Error while processing the request")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, null, Description = "User must be authenticated")]
        public async Task<ActionResult<RoomResponse>> CreateRoom(Guid id, [FromBody] CreateRoom.Command command)
        {
            command.CinemaId = id;

            var response = await _mediator.Send(command);

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, response.ValidationIssue);
            }

            return Ok(response.Payload);
        }

        [HttpPut("{id}/Rooms/{roomId}")]
        [Produces("application/json")]
        [OpenApiOperation(
            summary: "Updates a room",
            description: "Updates a room and return it"
        )]
        [SwaggerResponse(HttpStatusCode.OK, typeof(RoomResponse), Description = "Ok")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, typeof(ValidationProblemDetails), Description = "Error while processing the request")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, null, Description = "User must be authenticated")]
        public async Task<ActionResult<RoomResponse>> UpdateRoom(Guid id, Guid roomId, [FromBody] UpdateRoom.Command command)
        {
            command.CinemaId = id;
            command.RoomId = roomId;

            var response = await _mediator.Send(command);

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, response.ValidationIssue);
            }

            return Ok(response.Payload);
        }

        [HttpDelete("{id}/Rooms/{roomId}")]
        [Produces("application/json")]
        [OpenApiOperation(
            summary: "Deletes a room",
            description: "Deletes a room"
        )]
        [SwaggerResponse(HttpStatusCode.NoContent, typeof(void), Description = "Ok")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, typeof(ValidationProblemDetails), Description = "Error while processing the request")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, null, Description = "User must be authenticated")]
        public async Task<ActionResult<CinemaResponse>> DeleteRoom(Guid id, Guid roomId)
        {
            var command = new DeleteRoom.Command { CinemaId = id, RoomId = roomId };

            var response = await _mediator.Send(command);

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, response.ValidationIssue);
            }

            return NoContent();
        }

        #endregion
    }
}
