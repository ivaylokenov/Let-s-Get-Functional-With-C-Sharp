namespace Abstractions.Extensions
{
    using System;

    public static class LazyExtensions
    {
        // Chain lazy computation until you 
        // force evaluation with the Value property.

        public static Lazy<TResult> Map<TInput, TResult>(
            this Lazy<TInput> lazy,
            Func<TInput, TResult> mapping) 
            => new(() => mapping(lazy.Value));
    }
}
