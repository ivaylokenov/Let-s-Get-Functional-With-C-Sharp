namespace Concepts
{
    using Concepts.ImmutableTypes;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Program
    {
        public static void Main()
        {
            // Immutable types
            var mutableCat = new Cat("Sharo", 5);
            mutableCat.Grow(1);

            var imutableCat = new ImmutableCat("Sharo", 5);
            imutableCat = imutableCat.Grow(1);

            var catRecord = new CatRecord("Sharo", 5);
            var anotherRecordCat = catRecord with { Age = catRecord.Age + 1 };

            var (_, age) = anotherRecordCat;

            Console.WriteLine(age);

            // Method Chaining
            var result = new StringBuilder()
                .Append("Code ")
                .Append("It Up ")
                .ToString()
                .TrimEnd()
                .ToUpper();

            // Declarative instead of imperative
            var collection = new List<int> { 1, 2, 3, 4, 5 };

            // Imperative - verbose
            var evenImperative = new List<int>();

            foreach (var number in collection)
            {
                if (number % 2 == 0)
                {
                    evenImperative.Add(number);
                }
            }

            // Declarative - beautiful
            var evenDeclarative = collection
                .Where(num => num % 2 == 0)
                .ToList();

            // Higher order functions

            Func<int, int> timesThree = x => x * 3;
            int higherResult = EvalWithFiveAndAddTwo(timesThree); // 17

            var addOne = Adder(1);
            var addTwo = Adder(2);
            var firstResult = addOne(5); // 6
            var secondResult = addTwo(7); // 9
        }

        public static int EvalWithFiveAndAddTwo(Func<int, int> eval)
        {
            return eval(5) + 2;
        }

        public static Func<int, int> Adder(int number)
        {
            return x => x + number;
        }
    }
}
