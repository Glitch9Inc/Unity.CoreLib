namespace Glitch9
{
    public class GNLogger : ILogger
    {
        protected string Tag;
        public GNLogger(string tag) => Tag = tag;

        public virtual void Info(string message)
        {
            Info(message, Tag);
        }

        public virtual void Warning(string message)
        {
            Warning(message, Tag);
        }

        public virtual void Error(string message)
        {
            Error(message, Tag);
        }

        public virtual void Info(object sender, string message)
        {
            GNLog.Info(message, sender);
        }

        public virtual void Warning(object sender, string message)
        {
            GNLog.Warning(message, sender);
        }

        public virtual void Error(object sender, string message)
        {
            GNLog.Error(message, sender);
        }
    }
}