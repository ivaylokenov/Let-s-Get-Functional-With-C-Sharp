namespace Abstractions.Models
{
    using System.Collections.Generic;
    using Functors;

    public record Owner(string Name)
    {
        public Maybe<string> Address { get; init; }

        public IEnumerable<Cat> Cats { get; init; } = new List<Cat>();
    }
}
