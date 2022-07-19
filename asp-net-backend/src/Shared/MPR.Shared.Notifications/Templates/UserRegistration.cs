using MPR.Shared.Notifications.Messages;

namespace MPR.Shared.Notifications.Templates
{
    public class UserRegistration : EmailNotification
    {
        public override string TemplateName => "UserRegistration.html";
        public string RegistrationLink { get; set; }
        public string FirstName { get; set; }
    }
}
