using Bogus;
using Bogus.DataSets;

namespace DevTools.Shared;

public enum DataType
{
    Guid,
    LoremWord,
    FirstName,
    LastName, 
    Email,
    DateOfBirth,
    UserName,
    Number
}

public sealed class GeneratorModel
{
    public GeneratorModel(string name, DataType type)
    {
        Name = name;
        Type = type;
    }
        
    public string Name { get; set; }
    public DataType Type { get; set; }

    public object GetData()
    {
        var lorem = new Lorem();
        var person = new Person();
        var faker = new Faker();
        return Type switch 
        {
            DataType.Guid => Guid.NewGuid().ToString(),
            DataType.LoremWord => lorem.Word(),
            DataType.FirstName => person.FirstName,
            DataType.LastName => person.FirstName,
            DataType.Email => person.Email,
            DataType.DateOfBirth => person.DateOfBirth.ToString("d/M/yy"),
            DataType.UserName => person.UserName,
            DataType.Number => faker.Random.Int(),
            _ => string.Empty
        };
    }
}