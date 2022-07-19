using MediatR;
using Microsoft.AspNetCore.Identity;
using MPR.Auth.Data;
using MPR.Shared.Domain.Authorization;
using MPR.Shared.Domain.Models;
using MPR.Shared.Logic.Responses;
using MPR.Shared.Logic.Responses.Features.Users;
using MPR.Shared.Notifications.Messages;
using MPR.Shared.Notifications.Templates;
using MPR.Users.Logic.Errors;
using MPR.Users.Logic.Notifications;

namespace MPR.Users.Logic.Features.Users.Commands
{
    public class CreateRegularUser
    {
        public class Command : IRequest<Response<UserResponse>>
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
        }

        public class Handler : IRequestHandler<Command, Response<UserResponse>>
        {
            private readonly UserManager<User> _userManager;
            private readonly MprAuthDbContext _context;
            private readonly NotificationsProducer _notificationProducer;

            public Handler(UserManager<User> userManager,
                MprAuthDbContext context,
                NotificationsProducer notificationProducer)
            {
                _userManager = userManager;
                _context = context;
                _notificationProducer = notificationProducer;
            }

            public async Task<Response<UserResponse>> Handle(Command request, CancellationToken cancellationToken)
            {
                var newUser = new User
                {
                    Email = request.Email,
                    UserName = request.Email,
                    IsActive = false
                };

                var result = await _userManager.CreateAsync(newUser);
                if (!result.Succeeded)
                {
                    return Response.CreateBadRequestResponse<UserResponse>(ErrorCodes.EMAIL_ALREADYEXISTS,
                        $"Email already in use");
                }

                await _userManager.AddToRoleAsync(newUser, RoleCodes.REGULAR_USER);

                var profile = new Profile
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    User = newUser
                };

                await _context.Profiles.AddAsync(profile, cancellationToken);
                await _context.SaveChangesAsync(newUser.Id, cancellationToken);

                var token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
                var notification = CreateNotification(token, request.Email, request.FirstName);

                _notificationProducer.SendMessage(notification);

                var user = new UserResponse();

                return new Response<UserResponse> { Payload = user };
            }

            private UserRegistration CreateNotification(string token, string email, string firstName) => new UserRegistration
            {
                RegistrationLink = token,
                To = email,
                FirstName = firstName,
                Subject = "Welcome to MoviePass"
            };
        }
    }
}
