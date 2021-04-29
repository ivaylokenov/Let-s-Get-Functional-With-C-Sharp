namespace Abstractions.Functors
{
    using System;

    public struct Maybe<T>
    {
        private readonly T value;

        public Maybe(T value) => this.value = value;

        public static Maybe<T> None => new(default);

        public T Value => this.value;

        public bool HasValue => this.value != null;

        public Maybe<U> Bind<U>(Func<T, Maybe<U>> func) where U : class 
            => this.Value != null ? func(this.Value) : Maybe<U>.None;

        public static implicit operator Maybe<T>(T value)
        {
            if (value?.GetType() == typeof(Maybe<T>))
            {
                return (Maybe<T>)(object)value;
            }

            return new Maybe<T>(value);
        }
    }
}
