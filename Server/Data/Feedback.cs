using Google.Cloud.Firestore;

namespace DevTools.Server.Data;

[FirestoreData]
public class Feedback : IPersistentObject
{
    [FirestoreDocumentId]
    public string Id { get; set; }
    
    [FirestoreProperty]
    public Timestamp TimeStamp { get; set; }

    public string Message { get; set; } = 
        string.Empty;
    
    public string CollectionName() 
        => "feedback";
}