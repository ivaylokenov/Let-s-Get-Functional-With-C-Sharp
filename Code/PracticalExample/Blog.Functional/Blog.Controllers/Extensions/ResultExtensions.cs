namespace Blog.Controllers.Extensions
{
    using System;
    using System.Threading.Tasks;
    using CSharpFunctionalExtensions;
    using Microsoft.AspNetCore.Mvc;

    public static class ResultExtensions
    {
        public static Result<T> ToResult<T>(this T value)
            => value != null
                ? Result.Success(value)
                : Result.Failure<T>("Value cannot be null.");

        public static async Task<IActionResult> Match<T>(
            this Task<Result<T>> resultTask,
            Func<T, IActionResult> Some,
            Func<IActionResult> None)
        {
            var maybe = await resultTask;

            return maybe.IsSuccess
                ? Some(maybe.Value)
                : None();
        }
    }
}
