using System.Runtime.Serialization;

namespace Domain.Exceptions
{
    [Serializable]
    public class LoginIsAlreadyTakenException : BaseException 
    {
        public LoginIsAlreadyTakenException() { }

        public LoginIsAlreadyTakenException(string message) : base(message) { }

        public LoginIsAlreadyTakenException(string message, Exception inner) : base(message, inner) { }

        protected LoginIsAlreadyTakenException(
            SerializationInfo info,
            StreamingContext context) : base(info, context) { }
    }
}
