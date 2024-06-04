namespace Glitch9.Internal.Git
{
    public enum GitOutputStatus
    {
        Unknown,
        Info,
        Hint,
        Success,
        Warning,
        Error,
        Fatal,

        /// <summary>
        /// Custom status for when the git command is successfully completed
        /// </summary>
        Completed,
    }
}