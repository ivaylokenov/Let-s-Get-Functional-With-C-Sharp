namespace Concepts.ImmutableTypes
{
    public class Cat
    {
        public Cat(string name, int age)
        {
            this.Name = name;
            this.Age = age;
        }

        public string Name { get; private set; }

        public int Age { get; private set; }

        public void Grow(int years)
            => this.Age += years;
    }
}
