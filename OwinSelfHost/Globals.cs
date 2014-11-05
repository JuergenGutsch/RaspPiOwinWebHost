using System.Threading.Tasks;

namespace Webserver
{
    internal class Globals
    {
        public static readonly int BufferSize = 20 * 1024;
        internal static readonly Task CompletedTask = CreateCompletedTask();

        private static Task CreateCompletedTask()
        {
            var tcs = new TaskCompletionSource<object>();
            tcs.SetResult(null);
            return tcs.Task;
        }
    }
}