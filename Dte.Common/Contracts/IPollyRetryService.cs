using System;
using System.Threading.Tasks;

namespace Dte.Common.Contracts
{
    public interface IPollyRetryService
    {
        Task<TResult> WaitAndRetryAsync<TResult>(int retries, Func<int, TimeSpan> sleepDuration, Action<int> retryAction, Func<TResult, bool> handleResultCondition, Func<Task<TResult>> executeFunction, bool doInitialSleep);

        Task<TResult> WaitAndRetryAsync<TException, TResult>(int retries, Func<int, TimeSpan> sleepDuration,
            Action<int> retryAction, Func<Task<TResult>> executeFunction) where TException : Exception;

        Task<TResult> WaitAndRetryAsync<TException, TResult>(int retries, Func<int, TimeSpan> sleepDuration,
            Action<int> retryAction, Func<TResult, bool> handleResultCondition,
            Func<Task<TResult>> executeFunction, bool doInitialSleep) where TException : Exception;

        Task<TResult> WaitAndRetryAsync<TException1, TException2, TResult>(int retries,
            Func<int, TimeSpan> sleepDuration,
            Action<int> retryAction,
            Func<TResult, bool> handleResultCondition,
            Func<Task<TResult>> executeFunction,
            bool doInitialSleep) where TException1 : Exception
            where TException2 : Exception;
    }
}