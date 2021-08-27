using System;
using System.Net;

namespace rvezy.Core.Exceptions
{
    public class BadRequestException : BaseException
    {
        public BadRequestException()
        {
        }

        public BadRequestException(string message) : base(message)
        {
        }

        public BadRequestException(string message, Exception inner) : base(message, inner)
        {
        }

        public override int StatusCode => (int)HttpStatusCode.BadRequest;
    }
}