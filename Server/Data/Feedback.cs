using Google.Cloud.Firestore;

namespace DevTools.Server.Data;

[FirestoreData]
public class Feedback : IPersistentObject
{
    [FirestoreDocumentId]
    public string Id { get; set; }
     = string.Empty;

    [FirestoreProperty]
    public Timestamp TimeStamp { get; set; }

    [FirestoreProperty]
    public string Message { get; set; } =
        string.Empty;

    public string CollectionName()
        => "feedback";

    public bool IsBeingDeleted()
    {
        return false;
    }
}