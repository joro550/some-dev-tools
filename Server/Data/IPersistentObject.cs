using Google.Cloud.Firestore;

namespace DevTools.Server.Data;

public interface IPersistentObject
{
    public string Id { get; set; }
    public Timestamp TimeStamp { get; set; }

    string CollectionName();
    bool IsBeingDeleted();
}