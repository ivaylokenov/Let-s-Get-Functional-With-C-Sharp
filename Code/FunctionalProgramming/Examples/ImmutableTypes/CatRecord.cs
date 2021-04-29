namespace Concepts.ImmutableTypes
{
    public record CatRecord(string Name, int Age);
       
    // public record CatRecord
    // {
    //     public CatRecord(string name, int age)
    //     {
    //         this.Name = name;
    //         this.Age = age;
    //     }
       
    //     public string Name { get; init; }
       
    //     public int Age { get; init; }
       
    //     public void Deconstruct(out string name, out int age)
    //     {
    //         name = this.Name;
    //         age = this.Age;
    //     }
    // }
}
