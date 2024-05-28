namespace Glitch9
{
    public class OneShotLog
    {
        private string _cachedLog;

        public void Info(string log)
        {
            if (log == _cachedLog) return;
            _cachedLog = log;
            GNLog.Info(log);
        }
        public void Warning(string log)
        {
            if (log == _cachedLog) return;
            _cachedLog = log;
            GNLog.Warning(log);
        }
        public void Error(string log)
        {
            if (log == _cachedLog) return;
            _cachedLog = log;
            GNLog.Error(log);
        }

        public override string ToString() => _cachedLog;
    }
}