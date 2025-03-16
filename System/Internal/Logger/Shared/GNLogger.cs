namespace Glitch9
{
    public class GNLogger : ILogger
    {
        protected string Tag;
        public GNLogger(string tag) => Tag = tag;
        private LogListener _listener;

        public virtual void Info(string message)
        {
            Info(Tag, message);
        }

        public virtual void Warning(string message)
        {
            Warning(Tag, message);
        }

        public virtual void Error(string message)
        {
            Error(Tag, message);
        }

        public virtual void Info(object sender, string message)
        {
            GNLog.Info(message, sender);
            _listener?.OnInfo?.Invoke(message);
        }

        public virtual void Warning(object sender, string message)
        {
            GNLog.Warning(message, sender);
            _listener?.OnWarning?.Invoke(message);
        }

        public virtual void Error(object sender, string message)
        {
            GNLog.Error(message, sender);
            _listener?.OnError?.Invoke(message);
        }

        public void AddListener(LogListener listener)
        {
            _listener = listener;
        }
    }
}