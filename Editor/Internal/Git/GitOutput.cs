using System;
using System.Collections.Generic;

namespace Glitch9.Internal.Git
{
    public struct GitOutput : IEqualityComparer<GitOutput>
    {
        public string Message;
        public GitOutputStatus Status;

        public GitOutput(string message, GitOutputStatus status = 0)
        {
            Message = message;
            Status = status;
        }

        public bool Equals(GitOutput x, GitOutput y)
        {
            // 시간차가 0.5초 이내이면 같은 메시지로 판단
            return x.Message == y.Message && x.Status == y.Status;
        }

        public int GetHashCode(GitOutput obj)
        {
            return HashCode.Combine(obj.Message, (int)obj.Status);
        }
    }
}