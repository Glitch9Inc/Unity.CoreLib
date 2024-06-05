namespace Glitch9
{
    public interface ILogger
    {
        void Info(string message);
        void Warning(string message);
        void Error(string message);

        void Info(string tag, string message);
        void Warning(string tag, string message);
        void Error(string tag, string message);
    }
}