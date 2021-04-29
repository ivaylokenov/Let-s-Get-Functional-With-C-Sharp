namespace Functional
{
    using System;

    public static partial class Techniques
    {
        public static Func<B, C, R> partial<A, B, C, R>(Func<A, B, C, R> func, A a)
            => (B b, C c) => func(a, b, c);

        public static Func<B, C, R> Partial<A, B, C, R>(Func<A, B, C, R> func, A a)
            => (B b, C c) => func(a, b, c);

        public static Func<C, R> partial<A, B, C, R>(Func<A, B, C, R> func, A a, B b)
            => (C c) => func(a, b, c);

        public static Func<C, R> Partial<A, B, C, R>(Func<A, B, C, R> func, A a, B b)
            => (C c) => func(a, b, c);
    }
}
