namespace Blog.Data.Extensions
{
    using System;
    using System.Threading.Tasks;
    using CSharpFunctionalExtensions;

    public static class MaybeExtensions
    {
        public static async Task Execute<T>(this Task<Maybe<T>> maybeTask, Func<T, Task> action)
        {
            var maybe = await maybeTask;

            if (maybe.HasNoValue)
            {
                return;
            }

            await action(maybe.Value);
        }

        public static async Task<Maybe<T>> Tap<T>(this Task<Maybe<T>> maybeTask, Action<T> action)
        {
            var maybe = await maybeTask;

            if (maybe.HasValue)
            {
                action(maybe.Value);
            }

            return maybe;
        }

        public static async Task<TResult> Match<T, TResult>(this Task<Maybe<T>> maybeTask, Func<T, TResult> Some, Func<TResult> None)
        {
            var maybe = await maybeTask;

            return maybe.HasValue
                ? Some(maybe.Value)
                : None();
        }
    }
}
