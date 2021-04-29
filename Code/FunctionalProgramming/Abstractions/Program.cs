namespace Abstractions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Extensions;
    using Functors;
    using Models;

    using static Functional.Techniques;

    public class Program
    {
        public static void Main()
        {
            Functors();
            Maybe();
            MapDisadvantages();
            Monads();
            FromImperativeToDeclarative();
        }

        public static void Functors()
        {
            var box = new[] { 42 };
            int? container = 42;

            var boxResult = box.Map(v => v + 10);
            var containerResult = container.Map(v => v + 10);
        }

        public static void Maybe()
        {
            var maybeNumber = new Maybe<Cat>(new Cat("Sharo", 5));

            var maybeCat = GetCat()
                .Map(c => new Cat(c.Name, c.Age + 1));

            Console.WriteLine($"Cat age: {maybeCat.Value.Age}");
        }

        public static void MapDisadvantages()
        {
            IEnumerable<IEnumerable<Cat>> catsFromOwners
                   = GetOwnersList()
                       .Map(owner => owner.Cats);

            Maybe<IEnumerable<IEnumerable<Cat>>> maybeCatsFromMaybeOwners
                = GetOwners()
                    .Map(maybe => maybe
                        .Map(owner => owner.Cats));

            Maybe<Maybe<string>> maybeAddressesFromMaybeOwners
                = GetOwner()
                    .Map(owner => owner.Address);
        }

        public static void Monads()
        {
            var box = new[] { 42 };
            var maybeNumber = new Maybe<int>(42);

            Func<int, IEnumerable<int>> halfFuncion
                   = func((int number)
                       => number % 2 == 0
                           ? new[] { number / 2 }
                           : Array.Empty<int>());

            Func<int, Maybe<int>> maybeHalfFunction
                = func((int number)
                    => number % 2 == 0
                        ? number / 2
                        : Maybe<int>.None);

            IEnumerable<int> halfBox = box.FlatMap(halfFuncion);
            Maybe<int> halfMaybe = maybeNumber.FlatMap(maybeHalfFunction);

            Maybe<int> maybeChaining = new Maybe<int>(64)
                .FlatMap(maybeHalfFunction) // 32
                .FlatMap(maybeHalfFunction) // 16
                .FlatMap(maybeHalfFunction) // 8
                .FlatMap(maybeHalfFunction); // 4

            Console.WriteLine($"Half array: {halfBox.First()}");
            Console.WriteLine($"Half number: {halfMaybe.Value}");
            Console.WriteLine($"Half number chained: {maybeChaining.Value}");
        }

        public static void FromImperativeToDeclarative()
        {
            // Single loop to Map/Select

            var list = new List<int> { 42, 100, 200 };

            var resultImperative = new List<int>();

            foreach (var number in list)
            {
                resultImperative.Add(number + 10);
            }

            var resultDeclarative = list.Select(n => n + 10);

            // Multiple loops to FlatMap/Bind/SelectMany

            var secondList = new List<int> { 42, 1000, 2000 };

            resultImperative = new List<int>();

            foreach (var firstNumber in list)
            {
                foreach (var secondNumber in secondList)
                {
                    resultImperative.Add(firstNumber + secondNumber);
                }
            }

            resultDeclarative = from firstNumber in list
                                from secondNumber in secondList
                                select firstNumber + secondNumber;

            resultDeclarative = list
                .SelectMany(
                    fn => secondList, // For each number in the first list select everything in the second list.
                    (fn, sn) => fn + sn);

            // Conditional statements to Maybe

            var firstName = "John";
            var lastName = "Mnogovitch";

            string result = null;

            if (firstName != null && lastName != null)
            {
                result = firstName + " " + lastName;
            }
            else if (firstName != null)
            {
                result = firstName;
            }
            else
            {
                result = string.Empty;
            }

            Maybe<string> firstNameMaybe = "John";
            Maybe<string> lastNameMaybe = "Mnogovitch";

            result = firstNameMaybe
                .Map(fn => fn +
                    lastNameMaybe.Match(
                        Some: ln => " " + ln))
                .Match(
                    Some: r => r,
                    None: string.Empty);
        }

        private static Maybe<Cat> GetCat()
            => new Cat("Sharo", 5);

        private static Maybe<Owner> GetOwner()
            => new Owner("John") { Address = "Italy" };

        private static List<Owner> GetOwnersList()
            => new()
            {
                new Owner("John") { Cats = new List<Cat> { new Cat("Sharo", 5), new Cat("Lady", 13) } },
                new Owner("George") { Cats = new List<Cat> { new Cat("Silvestar", 2), new Cat("Maca", 12), new Cat("Meggy", 7) } }
            };

        private static Maybe<List<Owner>> GetOwners() => GetOwnersList();
    }
}
