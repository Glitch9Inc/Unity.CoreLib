using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Glitch9
{
    public class Error : Result
    {
        public static implicit operator string(Error error) => error.ToString();
        public static implicit operator Error(Exception exception) => new(exception);
        public static implicit operator Error(Issue issue) => new(issue);
        public static implicit operator Error(string errorMessage) => new(errorMessage);

        [JsonIgnore] public Issue Issue { get; set; }
        [JsonIgnore] public List<string> ErrorMessages { get; set; }
        [JsonIgnore] public string StackTrace { get; set; }

        public Error()
        {
        }
        
        public Error(params string[] errorMessages)
        {
            AddErrorMessages(errorMessages);
            GNLog.Error(this);
        }

        public Error(Exception exception, params string[] additionalMessages)
        {
            ErrorMessages = new List<string> { exception.Message };
            StackTrace = exception.StackTrace;
            AddErrorMessages(additionalMessages);
            GNLog.Error(this);
        }

        public Error(Issue issue, params string[] additionalMessages)
        {
            Issue = issue;
            ErrorMessages = new List<string> { issue.GetMessage() };
            AddErrorMessages(additionalMessages);
            GNLog.Error(this);
        }

        private void AddErrorMessages(params string[] errorMessages)
        {
            ErrorMessages ??= new List<string>();
            ErrorMessages.AddRange(errorMessages);
        }

        private string ParseToOneString(bool includeStackTrace)
        {
            if (ErrorMessages == null || ErrorMessages.Count == 0)
            {
                return string.IsNullOrEmpty(StackTrace) ? string.Empty : StackTrace;
            }

            if (includeStackTrace && !string.IsNullOrEmpty(StackTrace))
            {
                ErrorMessages.Add(StackTrace);
            }

            return string.Join("\n", ErrorMessages);
        }

        public override string ToString() => ParseToOneString(false);
        
        public string ToString(bool includeStackTrace) => ParseToOneString(includeStackTrace);
        
        public static bool operator ==(Error left, Error right)
        {
            if (ReferenceEquals(left, right)) return true;
            if (left is null || right is null) return false;
            return Equals(left.ErrorMessages, right.ErrorMessages);
        }

        public static bool operator !=(Error left, Error right) => !(left == right);

        public override bool Equals(object obj)
        {
            if (obj is Error error)
            {
                return this == error;
            }
            return false;
        }

        public override int GetHashCode() => ToString().GetHashCode();
    }

    public class Error<T> : Result<T>
    {
        [JsonIgnore] public Issue Issue { get; set; }
        [JsonIgnore] public List<string> ErrorMessages { get; private set; }
        [JsonIgnore] public string StackTrace { get; private set; }

        public Error(params string[] errorMessages)
        {
            AddErrorMessages(errorMessages);
            GNLog.Error(this);
        }

        public Error(Exception exception, params string[] additionalMessages)
        {
            ErrorMessages = new List<string> { exception.Message };
            StackTrace = exception.StackTrace;
            AddErrorMessages(additionalMessages);
            GNLog.Error(this);
        }

        public Error(Issue issue, params string[] additionalMessages)
        {
            Issue = issue;
            ErrorMessages = new List<string> { issue.GetMessage() };
            AddErrorMessages(additionalMessages);
            GNLog.Error(this);
        }

        private void AddErrorMessages(params string[] errorMessages)
        {
            ErrorMessages ??= new List<string>();
            ErrorMessages.AddRange(errorMessages);
        }

        private string ParseToOneString(bool includeStackTrace)
        {
            if (ErrorMessages == null || ErrorMessages.Count == 0)
            {
                return string.IsNullOrEmpty(StackTrace) ? string.Empty : StackTrace;
            }

            if (includeStackTrace && !string.IsNullOrEmpty(StackTrace))
            {
                ErrorMessages.Add(StackTrace);
            }

            return string.Join("\n", ErrorMessages);
        }

        public override string ToString() => ParseToOneString(false);
        
        public string ToString(bool includeStackTrace) => ParseToOneString(includeStackTrace);

        public static bool operator ==(Error<T> left, Error<T> right)
        {
            if (ReferenceEquals(left, right)) return true;
            if (left is null || right is null) return false;
            return Equals(left.ErrorMessages, right.ErrorMessages);
        }

        public static bool operator !=(Error<T> left, Error<T> right) => !(left == right);

        public override bool Equals(object obj)
        {
            if (obj is Error<T> error)
            {
                return this == error;
            }
            return false;
        }

        public override int GetHashCode() => ToString().GetHashCode();
    }
}