using System;

namespace SDating.Exceptions
{
    public class AuthException : Exception
    {
        public AuthException(string messsage, Exception innerException = null) : base(messsage, innerException)
        {
        }
    }
}
