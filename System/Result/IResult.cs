namespace Glitch9
{
    public interface IResult
    {
        bool IsSuccess { get; }
        bool IsFailure => !IsSuccess;
        string FailReason { get; }
    }
}