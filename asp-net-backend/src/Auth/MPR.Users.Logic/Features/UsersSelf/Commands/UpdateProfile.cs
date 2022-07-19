using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MPR.Auth.Data;
using MPR.Shared.Logic.Abstractions;
using MPR.Shared.Logic.Responses;
using MPR.Shared.Logic.Responses.Features.Users;
using MPR.Users.Logic.Errors;
using NJsonSchema.Annotations;

namespace MPR.Users.Logic.Features.UsersSelf.Commands
{
    public class UpdateProfile
    {
        [JsonSchema("UpdateProfileCommand")]
        public class Command : IRequest<Response<ProfileResponse>>
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(x => x.FirstName)
                    .Cascade(CascadeMode.Stop)
                    .NotEmpty();

                RuleFor(x => x.LastName)
                    .Cascade(CascadeMode.Stop)
                    .NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Command, Response<ProfileResponse>>
        {
            private readonly ICurrentUserService _currentUserService;
            private readonly MprAuthDbContext _context;

            public Handler(ICurrentUserService currentUserService, MprAuthDbContext context)
            {
                _currentUserService = currentUserService;
                _context = context;
            }

            public async Task<Response<ProfileResponse>> Handle(Command request, CancellationToken cancellationToken)
            {
                var userId = _currentUserService.GetUserId();

                var profile = await _context.Profiles
                                            .Include(x => x.User)
                                            .FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);

                if (profile == null)
                {
                    return Response.CreateBadRequestResponse<ProfileResponse>(ErrorCodes.INVALID_USER, "Invalid user");
                }

                if (!profile.User.IsActive)
                {
                    return Response.CreateBadRequestResponse<ProfileResponse>(ErrorCodes.USER_NOTACTIVE, "User is not active");
                }

                profile.FirstName = request.FirstName;
                profile.LastName = request.LastName;

                await _context.SaveChangesAsync(cancellationToken);

                var profileResponse = new ProfileResponse();

                return new Response<ProfileResponse> { Payload = profileResponse };
            }
        }
    }
}
