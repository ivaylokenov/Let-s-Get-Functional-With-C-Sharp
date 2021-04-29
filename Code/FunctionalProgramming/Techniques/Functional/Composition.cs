namespace Functional
{
    using System;

    public static partial class Techniques
    {
        public static Func<A, C> compose<A, B, C>(Func<A, B> a, Func<B, C> b) 
            => x => b(a(x));

        public static Func<A, C> Compose<A, B, C>(Func<A, B> a, Func<B, C> b)
            => x => b(a(x));

        public static Func<A, D> compose<A, B, C, D>(Func<A, B> a, Func<B, C> b, Func<C, D> c)
            => x => c(b(a(x)));

        public static Func<A, D> Compose<A, B, C, D>(Func<A, B> a, Func<B, C> b, Func<C, D> c)
            => x => c(b(a(x)));
    }
}
