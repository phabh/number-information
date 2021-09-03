using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberInformation.Services.Core
{
    public class MemoryProcessBackground<T> : IProcessBackground<T>
    {
        public static Dictionary<string, T> _memoryStorage = new Dictionary<string, T>();
        public static object _lock = new object();

        public string ExecuteInBackground(Func<T> backgroundMethod)
        {
            var taskId = Guid.NewGuid().ToString();

            lock (_lock)
            {
                while (_memoryStorage.ContainsKey(taskId))
                {
                    taskId = Guid.NewGuid().ToString();
                }

            }

            Task.Run(() =>
            {
                var insideTaskId = taskId.ToString();

                lock (_lock)
                {
                    _memoryStorage.Add(insideTaskId, backgroundMethod());
                }

            });

            return taskId;
        }

        public T GetResultBackgroundTask(string taskId)
        {
            lock (_lock)
            {
                if (_memoryStorage.ContainsKey(taskId))
                {
                    var result = _memoryStorage[taskId];
                    _memoryStorage.Remove(taskId);

                    return result;
                }
            }

            return default;
        }

    }
}
