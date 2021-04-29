namespace Blog.Data.Extensions
{
    using System;
    using System.Threading.Tasks;
    using CSharpFunctionalExtensions;
    using Microsoft.EntityFrameworkCore;

    public static class DbContextExtensions
    {
        public static async Task<Maybe<TModel>> Get<TModel>(
            this DbContext data,
            object id)
            where TModel : class
            => Maybe<TModel>
                .From(await data.FindAsync<TModel>(id));

        public static async Task<TResult> Create<TModel, TResult>(
            this DbContext data, 
            TModel value, 
            Func<TModel, TResult> keySelector)
            where TModel : class
        {
            data.Add(value);

            await data.SaveChangesAsync();

            return keySelector(value);
        }

        public static Task Update<TModel>(
            this DbContext data,
            object id,
            Action<TModel> updateAction)
            where TModel : class
            => data
                .Get<TModel>(id)
                .Tap(updateAction)
                .Execute(m => data.SaveChangesAsync());

        public static Task Remove<TModel>(
            this DbContext data,
            object id)
            where TModel : class
            => data
                .Get<TModel>(id)
                .Tap(m => data.Remove(m))
                .Execute(m => data.SaveChangesAsync());
    }
}
