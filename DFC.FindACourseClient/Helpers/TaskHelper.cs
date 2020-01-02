using System;
using System.Threading.Tasks;

namespace DFC.FindACourseClient
{
    internal static class TaskHelper
    {
        private static readonly Action<Task> SwallowError = t =>
        {
            try
            {
                t.Wait();
            }
            catch
            {
                //swallow error
            }
        };

        public static void ExecuteNoWait(Action action, Action<Exception> handler = null)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            var task = Task.Run(action);
            var taskContinuationOptions = TaskContinuationOptions.ExecuteSynchronously | TaskContinuationOptions.OnlyOnFaulted;
            if (handler is null)
            {
                task.ContinueWith(SwallowError, taskContinuationOptions);
            }
            else
            {
                task.ContinueWith(t => handler(t.Exception.GetBaseException()), taskContinuationOptions);
            }
        }
    }
}
