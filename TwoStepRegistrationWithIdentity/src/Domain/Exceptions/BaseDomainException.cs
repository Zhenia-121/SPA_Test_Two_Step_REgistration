using System.Runtime.Serialization;

namespace Domain.Exceptions
{
    [Serializable]
    public abstract class BaseException : ApplicationException
    {
        protected BaseException()
        {
        }

        protected BaseException(string message) : base(message)
        {
        }

        protected BaseException(string message, Exception inner) : base(message, inner)
        {
        }

        protected BaseException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
