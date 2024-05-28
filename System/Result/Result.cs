using System;
using Newtonsoft.Json;

namespace Glitch9
{
    public class Result : IResult
    {
        [JsonIgnore] public bool IsSuccess { get; protected set; }
        [JsonIgnore] public bool IsFailure => !IsSuccess;
        [JsonIgnore] public string FailReason { get; protected set; }

        public static Result Success() => new() { IsSuccess = true };
        public static Result Fail(string failReason = null) => new() { IsSuccess = false, FailReason = failReason };
        public static Result Error(Issue issue, params string[] additionalMessages) => new Error(issue, additionalMessages);
        public static Result Error(Exception ex, params string[] additionalMessages) => new Error(ex, additionalMessages);
    }

    public class Result<T> : Result
    {
        [JsonIgnore] public T Value { get; private set; }
        public static Result<T> Success(T value) => new() { IsSuccess = true, Value = value };
        public new static Result<T> Fail(string failReason = null) => new() { IsSuccess = false, FailReason = failReason };
        public new static Result<T> Fail(T value, string failReason = null) => new() { IsSuccess = false, FailReason = failReason, Value = value };
        public new static Result<T> Error(Issue issue, params string[] additionalMessages) => new Error<T>(issue, additionalMessages);
        public new static Result<T> Error(Exception ex, params string[] additionalMessages) => new Error<T>(ex, additionalMessages);
    }
}