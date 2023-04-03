using Domain.Common;

namespace Domain.Events
{
    public class UserRegisteredEvent : BaseEvent
    {
        public string Email { get; }

        public UserRegisteredEvent(string email)
        {
            Email = email;
        }
    }
}
