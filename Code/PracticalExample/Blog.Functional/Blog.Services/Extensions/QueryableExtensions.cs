namespace Blog.Services.Extensions
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    public static class QueryableExtensions
    {
        public static IQueryable<T> FilterOn<T>(
            this IQueryable<T> queryable,
            bool condition,
            Expression<Func<T, bool>> filter)
            => condition 
                ? queryable.Where(filter) 
                : queryable;
    }
}
