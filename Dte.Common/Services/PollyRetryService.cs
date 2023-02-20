using System;
using System.Threading.Tasks;
using Dte.Common.Contracts;
using Polly;

namespace Dte.Common.Services
{
    public class PollyRetryService : IPollyRetryService
    {
        public async Task<TResult> WaitAndRetryAsync<TResult>(int retries, Func<int, TimeSpan> sleepDuration, Action<int> retryAction, Func<TResult, bool> handleResultCondition, Func<Task<TResult>> executeFunction, bool doInitialSleep)
        {
            if (doInitialSleep)
            {
                await Task.Delay(sleepDuration(0));
            }

            var retryPolicy = Policy
                .HandleResult(handleResultCondition)
                .WaitAndRetryAsync(retries, sleepDuration, (ex, ts, index, context) => { retryAction?.Invoke(index); });

            try
            {
                var result = await retryPolicy.ExecuteAsync(executeFunction);

                return result;
            }
            catch
            {
                return default;
            }
        }

        public async Task<TResult> WaitAndRetryAsync<TException, TResult>(int retries, Func<int, TimeSpan> sleepDuration, Action<int> retryAction, Func<Task<TResult>> executeFunction) 
            where TException : Exception
        {
            var retryPolicy = Policy
                .Handle<TException>()
                .WaitAndRetryAsync(retries, sleepDuration, (ex, ts, index, context) => { retryAction?.Invoke(index); });

            return await retryPolicy.ExecuteAsync(executeFunction);
        }

        public async Task<TResult> WaitAndRetryAsync<TException, TResult>(int retries, Func<int, TimeSpan> sleepDuration, Action<int> retryAction, Func<TResult, bool> handleResultCondition, Func<Task<TResult>> executeFunction, bool doInitialSleep) 
            where TException : Exception
        {
            if (doInitialSleep)
            {
                await Task.Delay(sleepDuration(0));
            }

            var retryPolicy = Policy
                .Handle<TException>()
                .OrResult(handleResultCondition)
                .WaitAndRetryAsync(retries, sleepDuration, (ex, ts, index, context) => { retryAction?.Invoke(index); });

            try
            {
                var result = await retryPolicy.ExecuteAsync(executeFunction);

                return result;
            }
            catch
            {
                return default;
            }
        }

        public async Task<TResult> WaitAndRetryAsync<TException1, TException2, TResult>(int retries, Func<int, TimeSpan> sleepDuration, Action<int> retryAction, Func<TResult, bool> handleResultCondition, Func<Task<TResult>> executeFunction, bool doInitialSleep) 
            where TException1 : Exception
            where TException2 : Exception
        {
            if (doInitialSleep)
            {
                await Task.Delay(sleepDuration(0));
            }

            var retryPolicy = Policy
                .Handle<TException1>()
                .Or<TException2>()
                .OrResult(handleResultCondition)
                .WaitAndRetryAsync(retries, sleepDuration, (ex, ts, index, context) => { retryAction?.Invoke(index); });

            try
            {
                var result = await retryPolicy.ExecuteAsync(executeFunction);

                return result;
            }
            catch
            {
                return default;
            }
        }
    }
}