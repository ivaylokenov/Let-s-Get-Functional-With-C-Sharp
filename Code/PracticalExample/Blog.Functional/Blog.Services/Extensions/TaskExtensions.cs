namespace Blog.Services.Extensions
{
    using System.Threading.Tasks;
    using CSharpFunctionalExtensions;

    public static class TaskExtensions
    {
        public static async Task<Maybe<T>> ToMaybe<T>(this Task<T> task)
            => Maybe<T>.From(await task);
    }
}
