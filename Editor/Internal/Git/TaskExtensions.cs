using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Glitch9.Internal.Git
{
    public static class TaskExtensions
    {
        public static async Task WaitForExitAsync(this Process process, CancellationToken cancellationToken = default)
        {
            TaskCompletionSource<object> tcs = new();

            void ProcessExited(object sender, EventArgs e) => tcs.TrySetResult(null);

            process.EnableRaisingEvents = true;
            process.Exited += ProcessExited;

            using (cancellationToken.Register(() =>
                   {
                       process.Exited -= ProcessExited;
                       tcs.TrySetCanceled();
                   }))
            {
                await tcs.Task;
            }
        }
    }
}