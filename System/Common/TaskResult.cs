using System;

namespace Glitch9
{
    public class TaskResult
    {
        public static TaskResult Success => new() { IsSuccess = true };
        public static TaskResult Fail => new() { IsSuccess = false };
        
        public bool IsSuccess { get; set; }
        public ErrorMessage ErrorMessage { get; set; }
        public string ErrorText { get; set; }

        public static TaskResult Error(string message)
        {
            return new TaskResult
            {
                IsSuccess = false,
                ErrorMessage = new ErrorMessage(message)
            };
        }

        public static TaskResult Error(Exception exception, string errorResponseFromApi = null)
        {
            return new TaskResult
            {
                IsSuccess = false,
                ErrorMessage = new ErrorMessage(exception),
                ErrorText = errorResponseFromApi
            };
        }

        public static TaskResult Error(Issue issue, string errorResponseFromApi = null)
        {
            return new TaskResult
            {
                IsSuccess = false,
                ErrorMessage = new ErrorMessage(issue),
                ErrorText = errorResponseFromApi
            };
        }
    }
}