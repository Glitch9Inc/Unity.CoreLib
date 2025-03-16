using Newtonsoft.Json;
using System;

namespace Glitch9
{
    public class Error : Result
    {
        public static implicit operator string(Error error) => error.ToString();
        public static implicit operator Error(Exception exception) => new(exception);
        public static implicit operator Error(Issue issue) => new(issue);
        public static implicit operator Error(string errorMessage) => new(errorMessage);

        [JsonIgnore] public Issue Issue { get; set; }
        [JsonIgnore] public string ErrorMessage { get; set; }
        [JsonIgnore] public string StackTrace { get; set; }

        public Error()
        {
        }

        public Error(params string[] errorMessages)
        {
            JoinMessage(errorMessages);
            GNLog.Error(this);
        }

        public Error(Exception exception, params string[] additionalMessages)
        {
            ErrorMessage = exception.Message;
            StackTrace = exception.StackTrace;
            JoinMessage(additionalMessages);
            GNLog.Error(this);
        }

        public Error(Issue issue, params string[] additionalMessages)
        {
            Issue = issue;
            ErrorMessage = issue.GetMessage();
            JoinMessage(additionalMessages);
            GNLog.Error(this);
        }

        private void JoinMessage(params string[] errorMessages)
        {
            if (errorMessages == null || errorMessages.Length == 0)
            {
                return;
            }

            string joinedString = string.Join("\n", errorMessages);

            if (!string.IsNullOrEmpty(TextOutput))
            {
                ErrorMessage += "\n" + joinedString;
            }
            else
            {
                ErrorMessage = joinedString;
            }
        }

        public override string ToString() => ErrorMessage;
        public string ToString(bool includeStackTrace) => includeStackTrace ? $"{ErrorMessage}\n{StackTrace}" : ErrorMessage;

        public static bool operator ==(Error left, Error right)
        {
            if (ReferenceEquals(left, right)) return true;
            if (left is null || right is null) return false;
            return Equals(left.ErrorMessage, right.ErrorMessage);
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

    public class Error<T> : Error
    {
        [JsonIgnore] public T Value { get; private set; }

        public Error()
        {
        }

        public Error(T value, params string[] errorMessages) : base(errorMessages)
        {
            Value = value;
        }

        public Error(T value, Exception exception, params string[] additionalMessages) : base(exception, additionalMessages)
        {
            Value = value;
        }

        public Error(T value, Issue issue, params string[] additionalMessages) : base(issue, additionalMessages)
        {
            Value = value;
        }
    }
}