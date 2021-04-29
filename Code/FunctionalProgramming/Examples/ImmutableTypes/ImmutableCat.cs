namespace Concepts.ImmutableTypes
{
    public class ImmutableCat
    {
        public ImmutableCat(string name, int age)
        {
            this.Name = name;
            this.Age = age;
        }

        public string Name { get; init; }

        public int Age { get; init; }

        public ImmutableCat Grow(int years)
            => new(this.Name, this.Age + years);
    }
}
