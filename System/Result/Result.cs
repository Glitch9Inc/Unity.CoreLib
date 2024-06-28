using Newtonsoft.Json;
using System;

namespace Glitch9
{
    /// <summary>
    /// Represents the base class for result objects, providing success, failure, and error states.
    /// </summary>
    public class Result : IResult
    {
        /// <summary>
        /// Gets a value indicating whether the result is successful.
        /// </summary>
        [JsonIgnore] public bool IsSuccess { get; protected set; }

        /// <summary>
        /// Gets a value indicating whether the result is a failure.
        /// </summary>
        [JsonIgnore] public bool IsFailure => !IsSuccess;

        /// <summary>
        /// Gets a value indicating whether the result is an error.
        /// </summary>
        [JsonIgnore] public bool IsError => this is Error;

        /// <summary>
        /// Gets or sets the message associated with the result.
        /// </summary>
        [JsonIgnore] public virtual string TextOutput { get; set; }

        /// <summary>
        /// Gets the reason for the failure, if applicable.
        /// </summary>
        [JsonIgnore] public string FailReason { get; protected set; }

        /// <summary>
        /// Gets or sets a value indicating whether the result has been saved to the server.
        /// </summary>
        [JsonIgnore] public bool IsUpdated { get; set; }

        /// <summary>
        /// Returns true if it's a result of SSE event and the event is done.
        /// </summary>
        [JsonIgnore] public bool IsDone { get; set; } = false;

        /// <summary>
        /// Gets or sets the exception associated with the result.
        /// </summary>
        [JsonIgnore] public Exception Exception { get; set; }

        /// <summary>
        /// Creates a successful result.
        /// </summary>
        /// <returns>A successful <see cref="IResult"/>.</returns>
        public static IResult Success() => new Result { IsSuccess = true };

        /// <summary>
        /// Creates a successful result with a specified message.
        /// </summary>
        /// <param name="outputMessage">The message to associate with the result.</param>
        /// <returns>A successful <see cref="IResult"/> with a message.</returns>
        public static IResult Success(string outputMessage) => new Result { IsSuccess = true, TextOutput = outputMessage };

        /// <summary>
        /// Creates a failed result with an optional failure reason.
        /// </summary>
        /// <param name="failReason">The reason for the failure.</param>
        /// <returns>A failed <see cref="IResult"/>.</returns>
        public static IResult Fail(string failReason = null) => new Result { IsSuccess = false, FailReason = failReason };

        /// <summary>
        /// Creates an error result with a specified issue and additional messages.
        /// </summary>
        /// <param name="issue">The issue causing the error.</param>
        /// <param name="additionalMessages">Additional messages to associate with the error.</param>
        /// <returns>An error <see cref="IResult"/>.</returns>
        public static IResult Error(Issue issue, params string[] additionalMessages) => new Error(issue, additionalMessages);

        /// <summary>
        /// Creates an error result with a specified exception and additional messages.
        /// </summary>
        /// <param name="ex">The exception causing the error.</param>
        /// <param name="additionalMessages">Additional messages to associate with the error.</param>
        /// <returns>An error <see cref="IResult"/>.</returns>
        public static IResult Error(Exception ex, params string[] additionalMessages) => new Error(ex, additionalMessages);

        /// <summary>
        /// Creates a successful and saved result.
        /// </summary>
        /// <returns>A successful and saved <see cref="IResult"/>.</returns>
        public static IResult Updated() => new Result { IsSuccess = true, IsUpdated = true };
    }

    /// <summary>
    /// Represents a result object that includes a value, providing success, failure, and error states.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    public class Result<T> : Result
    {
        /// <summary>
        /// Gets the value associated with the result.
        /// </summary>
        [JsonIgnore] public T Value { get; private set; }

        /// <summary>
        /// Creates a successful result with a specified value.
        /// </summary>
        /// <param name="value">The value to associate with the result.</param>
        /// <returns>A successful <see cref="IResult"/> with a value.</returns>
        public static IResult Success(T value) => new Result<T> { IsSuccess = true, Value = value };

        /// <summary>
        /// Creates a failed result with a specified value and an optional failure reason.
        /// </summary>
        /// <param name="value">The value to associate with the result.</param>
        /// <param name="failReason">The reason for the failure.</param>
        /// <returns>A failed <see cref="IResult"/> with a value.</returns>
        public static IResult Fail(T value, string failReason = null) => new Result<T> { IsSuccess = false, FailReason = failReason, Value = value };

        /// <summary>
        /// Creates an error result with a specified value, issue, and additional messages.
        /// </summary>
        /// <param name="value">The value to associate with the result.</param>
        /// <param name="issue">The issue causing the error.</param>
        /// <param name="additionalMessages">Additional messages to associate with the error.</param>
        /// <returns>An error <see cref="IResult"/> with a value.</returns>
        public static IResult Error(T value, Issue issue, params string[] additionalMessages) => new Error<T>(value, issue, additionalMessages);

        /// <summary>
        /// Creates an error result with a specified value, exception, and additional messages.
        /// </summary>
        /// <param name="value">The value to associate with the result.</param>
        /// <param name="ex">The exception causing the error.</param>
        /// <param name="additionalMessages">Additional messages to associate with the error.</param>
        /// <returns>An error <see cref="IResult"/> with a value.</returns>
        public static IResult Error(T value, Exception ex, params string[] additionalMessages) => new Error<T>(value, ex, additionalMessages);
    }
}
