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
        [IssueCode("An unknown error occurred.")]
        UnknownError = 0,

        // System - General Errors
        [IssueCode("Invalid operation attempted.")]
        InvalidOperation,
        [IssueCode("Invalid argument provided.")]
        InvalidArgument,
        [IssueCode("Invalid format.")]
        InvalidFormat,

        // System - File and IO Errors
        [IssueCode("File not found.")]
        FileNotFound = 200,
        [IssueCode("File format is not supported.")]
        FileFormatUnsupported,
        [IssueCode("File size is too large.")]
        FileSizeExceeded,

        // System - Resource Errors
        [IssueCode("Resource not found.")]
        ResourceNotFound = 300,
        [IssueCode("Resource already exists.")]
        ResourceAlreadyExists = 301,
        [IssueCode("Resource is locked.")]
        ResourceLocked = 302,
        [IssueCode("Resource state conflict.")]
        ResourceStateConflict = 303,

        // Network - General Errors / HTTP (API)
        [IssueCode("[400] Invalid request. Please check your input.")]
        InvalidRequest = 400,
        [IssueCode("[401] You don't have permission to execute this operation.")]
        PermissionDenied,
        [IssueCode("[402] Server is not available. Please try again later.")]
        ServiceUnavailable,
        [IssueCode("[403] Server is forbidden. Please check your access rights.")]
        Forbidden,
        [IssueCode("[404] Target endpoint is not available. Please check the URL.")]
        InvalidEndpoint,
        [IssueCode("Request Failed.")]
        ProtocolError,
        [IssueCode("Request timeout.")]
        RequestTimeout,
        [IssueCode("Please enter all required fields to send this request.")]
        MissingRequiredField,
        [IssueCode("Access has been denied.")]
        UnauthorizedAccess,
        [IssueCode("No internet connection. Please try again later.")]
        NoInternet,
        [IssueCode("Network error occurred. Please try again later.")]
        NetworkError, // Generic
        [IssueCode("Too many requests. Please try again later.")]
        TooManyRequests,
        [IssueCode("Batch operation Failed.")]
        InvalidBatchOperation,
        [IssueCode("There was an error while sending the request.")]
        SendFailed,
        [IssueCode("There was an error while receiving the response.")]
        ReceiveFailed,
        [IssueCode("Request is not allowed by the server.")]
        RequestProhibitedByCachePolicy,
        [IssueCode("Request is not allowed by the server.")]
        RequestProhibitedByProxy,
        [IssueCode("A custom API error object was returned.")]
        ErrorObjectReturned,

        // Network - Validation and Format Errors
        [IssueCode("Server error occurred. Please try again later.")]
        InternalServerError = 500,
        [IssueCode("Validation error occurred.")]
        ValidationError,
        [IssueCode("Response is empty.")]
        EmptyResponse,
        [IssueCode("Response is malformed.")]
        MalformedResponse,
        [IssueCode("Data is out of range.")]
        DataOutOfRange,
        [IssueCode("Data is duplicated.")]
        DuplicateData,

        // Network - Authentication and Authorization Errors
        [IssueCode("Authentication Failed.")]
        AuthenticationFailed = 600,
        [IssueCode("Invalid email address.")]
        InvalidEmail,
        [IssueCode("Invalid password.")]
        InvalidPassword,
        [IssueCode("Invalid email or password.")]
        InvalidEmailOrPassword,
        [IssueCode("Token is Expired.")]
        TokenExpired,
        [IssueCode("Invalid token.")]
        TokenInvalid,
        [IssueCode("Session is Expired.")]
        SessionExpired,

        // Conversion, Parsing and Serialization Errors
        [IssueCode("Conversion Failed.")]
        ConversionFailed = 700,
        [IssueCode("Parsing Failed.")]
        ParsingFailed,
        [IssueCode("Serialization Failed.")]
        SerializationFailed,

        // Game Errors
        [IssueCode("Content is not ready.")]
        ContentNotReady = 800,
        [IssueCode("Invalid amount.")]
        InvalidAmount,
        [IssueCode("Your level is not high enough.")]
        NotEnoughLevel,
        [IssueCode("Invalid item.")]
        InvalidItem,
        [IssueCode("Item is not found.")]
        MissingItem,
        [IssueCode("Invalid content.")]
        InvalidContent,
        [IssueCode("Content is not found.")]
        MissingContent,
        [IssueCode("Invalid character.")]
        InvalidCharacter,
        [IssueCode("Character is not found.")]
        MissingCharacter,
        [IssueCode("Not enough energy.")]
        InsufficientEnergy,
        [IssueCode("Not enough stamina.")]
        InsufficientStamina,
        [IssueCode("You don't have required materials.")]
        InsufficientMaterials,
        [IssueCode("You don't have enough tokens.")]
        InsufficientTokens,
        [IssueCode("You don't have enough currency.")]
        InsufficientCurrency,
        [IssueCode("You don't have season pass.")]
        NoSeasonPass,

#if GLITCH9_INTERNAL

        [IssueCode("You don't have enough Glitch9 credits.")]
        InsufficientWallet,

#endif
    }

    public static class ErrorCodeExtensions
    {
        public static Issue Convert(this Exception e)
        {
            return e switch
            {
                GNException restEx => restEx.Issue,
                HttpRequestException _ => Issue.NetworkError,
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
                WebExceptionStatus.ProtocolError => Issue.ProtocolError,
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