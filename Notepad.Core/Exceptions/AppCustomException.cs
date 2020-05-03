using System;
using System.Collections.Generic;

namespace Notepad.Core.Exceptions
{
    public class AppCustomException : Exception
    {
        public int? StatusCode { get; set; }
        public AppCustomException(string message) : base(message)
        {
        }

        public AppCustomException(List<string> messages) : base(string.Join(", ", messages.ToArray()))
        {
        }
        public AppCustomException(int? statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
