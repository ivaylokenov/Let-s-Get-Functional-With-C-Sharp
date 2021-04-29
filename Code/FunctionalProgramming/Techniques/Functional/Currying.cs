namespace Functional
{
    using System;

    public static partial class Techniques
    {
        public static Func<A, Func<B, R>> curry<A, B, R>(Func<A, B, R> func)
            => (A a) => (B b) => func(a, b);

        public static Func<A, Func<B, R>> Curry<A, B, R>(Func<A, B, R> func)
            => (A a) => (B b) => func(a, b);

        public static Func<A, Func<B, Func<C, R>>> curry<A, B, C, R>(Func<A, B, C, R> func)
            => (A a) => (B b) => (C c) => func(a, b, c);

        public static Func<A, Func<B, Func<C, R>>> Curry<A, B, C, R>(Func<A, B, C, R> func)
            => (A a) => (B b) => (C c) => func(a, b, c);
    }
}
