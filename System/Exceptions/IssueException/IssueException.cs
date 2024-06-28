using System;
using UnityEngine;

namespace Glitch9
{
    public class IssueException : Exception
    {
        public Issue Issue { get; set; }
 
        public IssueException(Issue issue, string message = null) : base($"{issue.GetMessage()}: {message ?? string.Empty}")
        {
            //Debug.LogException(this);
            Issue = issue;
        }
    }
}