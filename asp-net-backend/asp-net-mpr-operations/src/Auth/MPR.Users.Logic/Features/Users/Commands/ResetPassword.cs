using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MPR.Auth.Data;
using MPR.Shared.Domain.Models;
using MPR.Shared.Logic.Responses;
using MPR.Users.Logic.Errors;
using NJsonSchema.Annotations;

namespace MPR.Users.Logic.Features.Users.Commands
{
    public class ResetPassword
    {
        [JsonSchema("ResetPasswordCommand")]
        public class Command : IRequest<Response<Unit>>
        {
            public string Email { get; set; }
            public string Password { get; set; }
            public string Token { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(x => x.Email)
                    .Cascade(CascadeMode.Stop)
                    .NotEmpty()
                    .EmailAddress();

                RuleFor(x => x.Password)
                    .Cascade(CascadeMode.Stop)
                    .NotEmpty()
                    .Matches(@"\d")
                    .Matches(@"[a-z]")
                    .Matches(@"[A-Z]")
                    .Matches(@"[\^$*.[\]{}(\)?""!@#%&/\\,><':;|_~`]");

                RuleFor(x => x.Token)
                    .Cascade(CascadeMode.Stop)
                    .NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Command, Response<Unit>>
        {
            private readonly UserManager<User> _userManager;
            private readonly MprAuthDbContext _context;

            public Handler(UserManager<User> userManager, MprAuthDbContext context)
            {
                _userManager = userManager;
                _context = context;
            }
            public async Task<Response<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken);

                if (user == null)
                {
                    return Response.CreateBadRequestResponse<Unit>(ErrorCodes.INVALID_USER, "Invalid user");
                }

                if (!user.IsActive)
                {
                    return Response.CreateBadRequestResponse<Unit>(ErrorCodes.USER_NOTACTIVE, "User is not active");
                }

                var result = await _userManager.ResetPasswordAsync(user, request.Token, request.Password);

                if (!result.Succeeded)
                {
                    return Response.GetResponseFromError<Unit>(errorCode: ErrorCodes.UNEXPECTED_ERROR, errorDescription: "Something went wrong");
                }

                return new Response<Unit> { Payload = Unit.Value };
            }
        }
    }
}
