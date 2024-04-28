using System;

namespace Glitch9
{
    public struct Error
    {
        public static explicit operator string(Error error) => error.Message;
        public static explicit operator Error(Exception exception) => new(exception);
        public static explicit operator Error(Issue issue) => new(issue);
        public static explicit operator Error(string errorMessage) => new(errorMessage);

        public string Message { get; private set; }
        public string ApiResponse { get; private set; }

        public Error(string value, string apiResponse = null)
        {
            this.Message = value;
            this.ApiResponse = apiResponse;
            GNLog.Error(value);
        }

        public Error(Exception exception, string apiResponse = null)
        {
            Message = $"{exception.Message}\n{exception.StackTrace}";
            ApiResponse = apiResponse;
            GNLog.Error(Message);
        }

        public Error(Issue issue, string apiResponse = null)
        {
            Message = issue.ToString();
            ApiResponse = apiResponse;
            GNLog.Error(Message);
        }
     
        public override string ToString() => Message;
        public static bool operator ==(Error left, Error right) => left.Message == right.Message;
        public static bool operator !=(Error left, Error right) => left.Message != right.Message;
        public override bool Equals(object obj) => obj is Error message && Message == message.Message;
        public override int GetHashCode() => Message.GetHashCode();
    }
}