using Newtonsoft.Json;
using System;

namespace Glitch9
{
    public class Result : IResult
    {
        [JsonIgnore] public bool IsSuccess { get; protected set; }
        [JsonIgnore] public bool IsFailure => !IsSuccess;
        [JsonIgnore] public bool IsError => this is Error;
        [JsonIgnore] public string Message { get; set; }
        [JsonIgnore] public string FailReason { get; protected set; }
        [JsonIgnore] public bool IsSaved { get; set; }

        public static IResult Success() => new Result { IsSuccess = true };
        public static IResult Success(string outputMessage) => new Result { IsSuccess = true, Message = outputMessage };
        public static IResult Fail(string failReason = null) => new Result { IsSuccess = false, FailReason = failReason };
        public static IResult Error(Issue issue, params string[] additionalMessages) => new Error(issue, additionalMessages);
        public static IResult Error(Exception ex, params string[] additionalMessages) => new Error(ex, additionalMessages);
        public static IResult Saved() => new Result { IsSuccess = true, IsSaved = true };
    }

    public class Result<T> : Result
    {
        [JsonIgnore] public T Value { get; private set; }
        public static IResult Success(T value) => new Result<T> { IsSuccess = true, Value = value };
        public static IResult Fail(T value, string failReason = null) => new Result<T> { IsSuccess = false, FailReason = failReason, Value = value };
        public static IResult Error(T value, Issue issue, params string[] additionalMessages) => new Error<T>(value, issue, additionalMessages);
        public static IResult Error(T value, Exception ex, params string[] additionalMessages) => new Error<T>(value, ex, additionalMessages);
    }
}