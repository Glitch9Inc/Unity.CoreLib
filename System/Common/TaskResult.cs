using System;

namespace Glitch9
{
    public class TaskResult
    {
        public static TaskResult Success => new() { IsSuccess = true };
        public static TaskResult Fail => new() { IsSuccess = false };
        
        public bool IsSuccess { get; set; }
        public Error Error { get; set; }
 

        public static TaskResult CreateError(string errorMessage, string errorResponseFromApi = null)
        {
            return new TaskResult
            {
                IsSuccess = false,
                Error = new Error(errorMessage, errorResponseFromApi)
            };
        }

        public static TaskResult CreateError(Exception exception, string errorResponseFromApi = null)
        {
            return new TaskResult
            {
                IsSuccess = false,
                Error = new Error(exception, errorResponseFromApi),
            };
        }

        public static TaskResult CreateError(Issue issue, string errorResponseFromApi = null)
        {
            return new TaskResult
            {
                IsSuccess = false,
                Error = new Error(issue, errorResponseFromApi),
            };
        }
    }
}