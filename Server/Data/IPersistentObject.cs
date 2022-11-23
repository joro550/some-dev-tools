namespace DevTools.Server.Data;

public interface IPersistentObject
{
    public string Id { get; set; }

    string CollectionName();
    
}