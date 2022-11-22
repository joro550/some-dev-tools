namespace DevTools.Server.Data;

public interface IPersistentObject
{
    public string Id { get; set; }
    
    static abstract string CollectionName { get; }
}