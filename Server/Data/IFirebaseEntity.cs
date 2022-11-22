namespace DevTools.Server.Data;

public interface IFirebaseEntity
{
    public string Id { get; set; }
    
    string CollectionName { get; }
}