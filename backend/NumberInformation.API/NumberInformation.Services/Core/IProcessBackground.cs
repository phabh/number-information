using System;

namespace NumberInformation.Services.Core
{
    public interface IProcessBackground<T>
    {
        string ExecuteInBackground(Func<T> backgroundMethod);
        T GetResultBackgroundTask(string taskId);
    }
}