using Bogus.DataSets;

namespace DevTools.Shared;

public enum DataType
{
    Guid,
    LoremWord
}

public sealed class CsvGeneratorModel
{
    public CsvGeneratorModel(string name, DataType type)
    {
        Name = name;
        Type = type;
    }
        
    public string Name { get; set; }
    public DataType Type { get; set; }

    public string GetData()
    {
        var lorem = new Lorem();
            
        return Type switch 
        {
            DataType.Guid => Guid.NewGuid().ToString(),
            DataType.LoremWord => lorem.Word(),
            _ => string.Empty
        };
    }
}