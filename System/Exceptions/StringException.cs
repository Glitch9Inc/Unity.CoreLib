using System;

namespace Glitch9
{
    public class StringException : Exception
    {
        public StringException(string message) : base(message)
        {
        }
    }
}