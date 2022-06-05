using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MPR.Auth.Data;
using MPR.Shared.Domain.Models;
using MPR.Shared.Logic.Abstractions;
using MPR.Shared.Logic.Responses;
using MPR.Users.Logic.Errors;
using NJsonSchema.Annotations;

namespace MPR.Users.Logic.Features.UsersSelf.Commands
{
    public class ChangePassword
    {
        [JsonSchema("ChangePasswordCommand")]
        public class Command : IRequest<Response<Unit>>
        {
            public string CurrentPassword { get; set; }
            public string NewPassword { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(x => x.CurrentPassword)
                        .Cascade(CascadeMode.Stop)
                        .NotEmpty();

                RuleFor(x => x.NewPassword)
                    .Cascade(CascadeMode.Stop)
                    .NotEmpty()
                    .Matches(@"\d")
                    .Matches(@"[a-z]")
                    .Matches(@"[A-Z]")
                    .Matches(@"[\^$*.[\]{}(\)?""!@#%&/\\,><':;|_~`]");
            }
        }

        public class Handler : IRequestHandler<Command, Response<Unit>>
        {
            private readonly ICurrentUserService _currentUserService;
            private readonly UserManager<User> _userManager;
            private readonly MprAuthDbContext _context;

            public Handler(ICurrentUserService currentUserService,
                MprAuthDbContext context,
                UserManager<User> userManager)
            {
                _currentUserService = currentUserService;
                _context = context;
                _userManager = userManager;
            }

            public async Task<Response<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var userId = _currentUserService.GetUserId();

                var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);

                if (user == null)
                {
                    return Response.CreateBadRequestResponse<Unit>(ErrorCodes.INVALID_USER, "Invalid user");
                }

                if (!user.IsActive)
                {
                    return Response.CreateBadRequestResponse<Unit>(ErrorCodes.USER_NOTACTIVE, "User is not active");
                }

                var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);

                if (!result.Succeeded)
                {
                    return Response.GetResponseFromError<Unit>(errorCode: ErrorCodes.UNEXPECTED_ERROR, errorDescription: "Something went wrong");
                }

                return new Response<Unit> { Payload = Unit.Value };
            }
        }
    }
}
