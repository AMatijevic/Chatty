using System;

namespace Chatty.Infrastructure.Exceptions
{
    public class PersistenceException : Exception
    {
        public string[] ErrorMessages { get; }

        public PersistenceException(params string[] errorMessages)
        {
            ErrorMessages = errorMessages;
        }

        public PersistenceException(string errorMessage, Exception ex) : base(errorMessage, ex)
        {
            ErrorMessages = new[] { errorMessage };
        }
        public PersistenceException(Exception ex) : base("Generic.DbOperationFailed", ex)
        {
            ErrorMessages = new[] { "Generic.DbOperationFailed" };
        }
    }
}
