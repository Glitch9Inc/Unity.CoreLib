using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Security;
using System.Security.Authentication;

namespace Glitch9
{
    public enum Issue
    {
        None,

        // Fallback
        [Message("An unknown error occurred.")]
        UnknownError = 0,

        // System - General Errors
        [Message("Invalid operation attempted.")]
        InvalidOperation,
        [Message("Invalid argument provided.")]
        InvalidArgument,
        [Message("Invalid format.")]
        InvalidFormat,

        // System - File and IO Errors
        [Message("File not found.")]
        FileNotFound = 200,
        [Message("File format is not supported.")]
        FileFormatUnsupported,
        [Message("File size is too large.")]
        FileSizeExceeded,

        // System - Resource Errors
        [Message("Resource not found.")]
        ResourceNotFound = 300,
        [Message("Resource already exists.")]
        ResourceAlreadyExists = 301,
        [Message("Resource is locked.")]
        ResourceLocked = 302,
        [Message("Resource state conflict.")]
        ResourceStateConflict = 303,

        // Network - General Errors / HTTP (API)
        [Message("[400] Invalid request. Please check your input.")]
        InvalidRequest = 400,
        [Message("[401] You don't have permission to execute this operation.")]
        PermissionDenied,
        [Message("[402] Server is not available. Please try again later.")]
        ServiceUnavailable,
        [Message("[403] Server is forbidden. Please check your access rights.")]
        Forbidden,
        [Message("[404] Target endpoint is not available. Please check the URL.")]
        InvalidEndpoint,
        [Message("Request Failed.")]
        RequestFailed,
        [Message("Request timeout.")]
        RequestTimeout,
        [Message("Please enter all required fields to send this request.")]
        MissingRequiredField,
        [Message("Access has been denied.")]
        UnauthorizedAccess,
        [Message("No internet connection. Please try again later.")]
        NoInternet,
        [Message("Network error occurred. Please try again later.")]
        NetworkError, // Generic
        [Message("Too many requests. Please try again later.")]
        TooManyRequests,
        [Message("Batch operation Failed.")]
        InvalidBatchOperation,
        [Message("There was an error while sending the request.")]
        SendFailed,
        [Message("There was an error while receiving the response.")]
        ReceiveFailed,
        [Message("Request is not allowed by the server.")]
        RequestProhibitedByCachePolicy,
        [Message("Request is not allowed by the server.")]
        RequestProhibitedByProxy,
        [Message("A custom API error object was returned.")]
        ErrorObjectReturned,

        // Network - Validation and Format Errors
        [Message("Server error occurred. Please try again later.")]
        InternalServerError = 500,
        [Message("Validation error occurred.")]
        ValidationError,
        [Message("Response is empty.")]
        EmptyResponse,
        [Message("Response is malformed.")]
        MalformedResponse,
        [Message("Data is out of range.")]
        DataOutOfRange,
        [Message("Data is duplicated.")]
        DuplicateData,

        // Network - Authentication and Authorization Errors
        [Message("Authentication Failed.")]
        AuthenticationFailed = 600,
        [Message("Invalid email address.")]
        InvalidEmail,
        [Message("Invalid password.")]
        InvalidPassword,
        [Message("Invalid email or password.")]
        InvalidEmailOrPassword,
        [Message("Token is Expired.")]
        TokenExpired,
        [Message("Invalid token.")]
        TokenInvalid,
        [Message("Session is Expired.")]
        SessionExpired,

        // Conversion, Parsing and Serialization Errors
        [Message("Conversion Failed.")]
        ConversionFailed = 700,
        [Message("Parsing Failed.")]
        ParsingFailed,
        [Message("Serialization Failed.")]
        SerializationFailed,

        // Game Errors
        [Message("Content is not ready.")]
        ContentNotReady = 800,
        [Message("Invalid amount.")]
        InvalidAmount,
        [Message("Your level is not high enough.")]
        NotEnoughLevel,
        [Message("Invalid item.")]
        InvalidItem,
        [Message("Item is not found.")]
        MissingItem,
        [Message("Invalid content.")]
        InvalidContent,
        [Message("Content is not found.")]
        MissingContent,
        [Message("Invalid character.")]
        InvalidCharacter,
        [Message("Character is not found.")]
        MissingCharacter,
        [Message("Not enough energy.")]
        InsufficientEnergy,
        [Message("Not enough stamina.")]
        InsufficientStamina,
        [Message("You don't have required materials.")]
        InsufficientMaterials,
        [Message("You don't have enough tokens.")]
        InsufficientTokens,
        [Message("You don't have enough currency.")]
        InsufficientCurrency,
        [Message("You don't have season pass.")]
        NoSeasonPass,

#if GLITCH9_INTERNAL

        [Message("You don't have enough Glitch9 credits.")]
        InsufficientWallet,

#endif
    }

    public static class IssueConverter
    {
        public static Issue Convert(this Exception e)
        {
            return e switch
            {
                IssueException restEx => restEx.Issue,
                HttpRequestException httpEx => Issue.NetworkError,
                WebException webEx => ConvertWebException(webEx),
                TimeoutException _ => Issue.RequestTimeout,
                InvalidOperationException _ => Issue.InvalidOperation,
                AuthenticationException _ => Issue.AuthenticationFailed,
                ArgumentException _ => Issue.InvalidArgument,
                IOException _ => Issue.FileNotFound, // 파일 또는 IO 관련 에러 처리 추가
                SecurityException _ => Issue.UnauthorizedAccess, // 보안 관련 에러 처리 추가
                FormatException _ => Issue.InvalidFormat, // 형식 관련 에러 처리 추가
                UnauthorizedAccessException _ => Issue.PermissionDenied, // 권한 관련 에러 처리 추가
                NotSupportedException _ => Issue.InvalidOperation, // 지원되지 않는 작업을 시도한 경우
                // TODO: 필요에 따라 더 많은 특정 케이스를 추가하세요.

                _ => Issue.UnknownError, // 일치하는 예외가 없는 경우
            };
        }

        private static Issue ConvertWebException(WebException webEx)
        {
            Issue code = ConvertWebExceptionStatus(webEx.Status);
            if (code == Issue.UnknownError && webEx.Response is HttpWebResponse response)
            {
                code = ConvertHttpWebResponse(response);
            }
            return code;
        }

        private static Issue ConvertWebExceptionStatus(WebExceptionStatus status)
        {
            return status switch
            {
                WebExceptionStatus.ConnectFailure => Issue.ServiceUnavailable,
                WebExceptionStatus.ProtocolError => Issue.RequestFailed,
                WebExceptionStatus.NameResolutionFailure => Issue.NoInternet,
                WebExceptionStatus.Timeout => Issue.RequestTimeout,
                WebExceptionStatus.SendFailure => Issue.SendFailed,
                WebExceptionStatus.ReceiveFailure => Issue.ReceiveFailed,
                WebExceptionStatus.RequestProhibitedByCachePolicy => Issue.RequestProhibitedByCachePolicy,
                WebExceptionStatus.RequestProhibitedByProxy => Issue.RequestProhibitedByProxy,
                _ => Issue.UnknownError,
            };
        }

        private static Issue ConvertHttpWebResponse(HttpWebResponse response)
        {
            return response.StatusCode switch
            {
                HttpStatusCode.BadRequest => Issue.InvalidRequest, // 400
                HttpStatusCode.Unauthorized => Issue.PermissionDenied, // 401
                HttpStatusCode.ServiceUnavailable => Issue.ServiceUnavailable, // 402
                HttpStatusCode.Forbidden => Issue.Forbidden, // 403
                HttpStatusCode.NotFound => Issue.InvalidEndpoint, // 404
                HttpStatusCode.InternalServerError => Issue.InternalServerError, // 500
                _ => Issue.UnknownError,
            };
        }
    }
}