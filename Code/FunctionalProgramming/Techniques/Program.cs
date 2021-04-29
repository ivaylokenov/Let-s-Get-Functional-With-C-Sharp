namespace Techniques
{
    using System;

    using static Functional.Techniques;

    public class Program
    {
        // Currying example with lambda
        public static Func<int, Func<int, int>> sum = x => y => x + y;

        // Currying example with full method
        public static Func<int, int> sumFull(int x)
        {
            int partial(int y)
            {
                return x + y;
            }

            return partial;
        }

        public static void Main()
        {
            // Currying

            // Func<int, int, int, int> func = (x, y, z) => x + y + z;

            var function = func((int x, int y, int z) => x + y + z);

            var curriedFunction = curry(function);

            var curriedOne = curriedFunction(1);
            var curriedTwo = curriedOne(2);
            var curryResult = curriedTwo(3);

            Console.WriteLine(curryResult);

            // Partial application

            var partialFunction = partial(function, 2, 3);

            var partialResult = partialFunction(4);

            Console.WriteLine(partialResult);

            // Composition

            var firstFunction = func((double number) => (int)Math.Round(number));
            var secondFunction = func((int integer) => integer % 2 == 0);

            var composedFunction = compose(firstFunction, secondFunction);

            var composedResult = composedFunction(1.2);

            Console.WriteLine(composedResult);
        }
    }
}
