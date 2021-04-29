namespace Abstractions.Extensions
{
    using System;
    using System.Threading.Tasks;

    public static class TaskExtensions
    {
        // Map tasks one after another.

        public async static Task<TResult> Map<TInput, TResult>(
            this Task<TInput> task,
            Func<TInput, TResult> mapping) 
            => mapping(await task);
    }
}
