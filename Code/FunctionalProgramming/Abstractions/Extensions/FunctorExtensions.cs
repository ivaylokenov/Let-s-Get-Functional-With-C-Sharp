namespace Abstractions.Extensions
{
    using System;
    using System.Collections.Generic;

    public static class FunctorExtensions
    {
        public static int[] map(int[] array, Func<int, int> mapping)
            => array.Map(mapping);

        public static int[] Map(this int[] array, Func<int, int> mapping)
        {
            var result = new int[array.Length];

            for (int i = 0; i < array.Length; i++)
            {
                result[i] = mapping(array[i]);
            }

            return result;
        }

        public static int? map(int? number, Func<int, int> mapping)
            => number.Map(mapping);

        public static int? Map(this int? number, Func<int, int> mapping)
        {
            if (number.HasValue)
            {
                return mapping(number.Value);
            }

            return null;
        }

        public static IEnumerable<TResult> Map<TInput, TResult>(
            this IEnumerable<TInput> input,
            Func<TInput, TResult> mapping)
        {
            foreach (var item in input)
            {
                yield return mapping(item);
            }
        }

        public static IEnumerable<TResult> FlatMap<TInput, TResult>(
            this IEnumerable<TInput> input,
            Func<TInput, IEnumerable<TResult>> mapping)
        {
            foreach (var item in input)
            {
                foreach (var mappedItem in mapping(item))
                {
                    yield return mappedItem;
                }
            }
        }

        public static IEnumerable<TResult> Bind<TInput, TResult>(
            this IEnumerable<TInput> input,
            Func<TInput, IEnumerable<TResult>> mapping)
            => input.FlatMap(mapping);
    }
}
