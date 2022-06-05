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
    public class InitiatePasswordReset
    {
        [JsonSchema("InitiatePasswordResetCommand")]
        public class Command : IRequest<Response<Unit>>
        {
            public string Email { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(x => x.Email)
                    .Cascade(CascadeMode.Stop)
                    .NotEmpty()
                    .EmailAddress();
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
                var userProfile = await _context.Profiles
                                                    .Include(x => x.User)
                                                    .FirstOrDefaultAsync(x => x.User.Email == request.Email, cancellationToken);

                if (userProfile == null)
                {
                    return Response.CreateBadRequestResponse<Unit>(ErrorCodes.INVALID_USER,
                        "Invalid user");
                }

                if (!userProfile.User.IsActive)
                {
                    return Response.CreateBadRequestResponse<Unit>(ErrorCodes.USER_NOTACTIVE,
                        "User is not active");
                }

                var token = await _userManager.GeneratePasswordResetTokenAsync(userProfile.User);

                return new Response<Unit> { Payload = Unit.Value };
            }
        }
    }
}
