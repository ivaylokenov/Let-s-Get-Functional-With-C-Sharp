namespace Abstractions.Extensions
{
    using System;
    using Functors;

    public static class MaybeExtensions
    {
        public static Maybe<TResult> Map<TInput, TResult>(
            this Maybe<TInput> maybe, 
            Func<TInput, TResult> mapping)
        {
            if (!maybe.HasValue)
            {
                return Maybe<TResult>.None;
            }

            return mapping(maybe.Value);
        }

        public static Maybe<TResult> FlatMap<TInput, TResult>(
            this Maybe<TInput> maybe,
            Func<TInput, Maybe<TResult>> mapping)
        {
            if (!maybe.HasValue)
            {
                return Maybe<TResult>.None;
            }

            return mapping(maybe.Value);
        }

        public static Maybe<TResult> Bind<TInput, TResult>(
            this Maybe<TInput> maybe,
            Func<TInput, Maybe<TResult>> mapping)
            => maybe.FlatMap(mapping);

        public static TResult Match<TInput, TResult>(
            this Maybe<TInput> maybe,
            Func<TInput, TResult> Some,
            Func<TResult> None)
            => maybe.HasValue
                ? Some(maybe.Value)
                : None();

        public static TResult Match<TInput, TResult>(
            this Maybe<TInput> maybe,
            Func<TInput, TResult> Some,
            TResult None)
            => maybe.HasValue
                ? Some(maybe.Value)
                : None;

        public static TResult Match<TInput, TResult>(
            this Maybe<TInput> maybe,
            Func<TInput, TResult> Some)
            => maybe.HasValue
                ? Some(maybe.Value)
                : default;
    }
}
