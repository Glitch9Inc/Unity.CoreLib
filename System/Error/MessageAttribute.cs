using System;

namespace Glitch9
{
    public class MessageAttribute : Attribute
    {
        public string Message { get; }

        public MessageAttribute(string message)
        {
            Message = message;
        }
    }
}
