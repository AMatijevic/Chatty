using System;

namespace Chatty.Domain.Exceptions
{
    public class DomainModelException : Exception
    {
        public DomainModelException(params string[] errorMessages)
        {
            ErrorMessages = errorMessages;
        }

        public string[] ErrorMessages { get; }
    }
}
