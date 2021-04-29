namespace Blog.Controllers.Extensions
{
    using System;
    using System.Threading.Tasks;
    using CSharpFunctionalExtensions;
    using Microsoft.AspNetCore.Mvc;

    public static class MaybeExtensions
    {
        public static async Task<IActionResult> Match<T>(
            this Task<Maybe<T>> maybeTask, 
            Func<T, IActionResult> Some, 
            Func<IActionResult> None)
        {
            var maybe = await maybeTask;

            return maybe.HasValue
                ? Some(maybe.Value)
                : None();
        }
    }
}
