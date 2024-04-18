using System;

namespace Glitch9
{
    public class GNException : Exception
    {
        public Issue Issue { get; set; }
        public GNException() { }
        public GNException(Issue issue, string message = null) : base(message)
        {
            Issue = issue;
        }
        public GNException(Exception e, string message = null) : base(message, e)
        {
            Issue = e.Convert();
        }
    }
}