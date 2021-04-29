namespace Abstractions.Functors
{
    using System;
    using System.Threading.Tasks;

    public class Future<T>
    {
        private readonly Task<T> instance;

        public Future(T instance) 
            => this.instance = Task.FromResult(instance);

        private Future(Task<T> instance) 
            => this.instance = instance;

        public static Future<TValue> From<TValue>(TValue value) => new(value);

        public Future<U> FlatMap<U>(Func<T, Future<U>> func)
        {
            var a = this.instance
                .ContinueWith(async t => func(await t).instance)
                .Unwrap();

            return new Future<U>(a.Unwrap());
        }

        public async Task OnComplete(Action<T> action) 
            => await this.instance.ContinueWith(async t => action(await t));
    }
}
