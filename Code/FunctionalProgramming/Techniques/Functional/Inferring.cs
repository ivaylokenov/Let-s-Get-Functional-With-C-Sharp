namespace Functional
{
    using System;

    public static partial class Techniques
    {
        public static Func<A, R> func<A, R>(Func<A, R> func) => func;

        public static Func<A, R> Func<A, R>(Func<A, R> func) => func;

        public static Func<A, B, R> func<A, B, R>(Func<A, B, R> func) => func;

        public static Func<A, B, R> Func<A, B, R>(Func<A, B, R> func) => func;

        public static Func<A, B, C, R> func<A, B, C, R>(Func<A, B, C, R> func) => func;

        public static Func<A, B, C, R> Func<A, B, C, R>(Func<A, B, C, R> func) => func;
    }
}
