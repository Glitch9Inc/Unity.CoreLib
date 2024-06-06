using System;

namespace Glitch9
{
    public class IssueException : Exception
    {
        public Issue Issue { get; set; }
        public IssueException() { }
        public IssueException(Issue issue, string message = null) : base(message)
        {
            Issue = issue;
        }
        public IssueException(Exception e, string message = null) : base(message, e)
        {
            Issue = e.Convert();
        }
    }
}