using System;

namespace Glitch9
{
    public class IssueCodeAttribute : Attribute
    {
        public string Message { get; }

        public IssueCodeAttribute(string message)
        {
            Message = message;
        }
    }
}
