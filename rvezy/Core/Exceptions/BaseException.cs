using System;

namespace rvezy.Core.Exceptions
{
    public class BaseException : Exception
    {
        public virtual int StatusCode { get; set; }

        public BaseException()
        {
        }

        public BaseException(string message) : base(message)
        {
        }

        public BaseException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}