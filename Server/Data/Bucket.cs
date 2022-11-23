using AutoMapper;
using DevTools.Shared;
using Google.Cloud.Firestore;

namespace DevTools.Server.Data;

[FirestoreData]
public class Bucket : IPersistentObject
{
    [FirestoreDocumentId]
    public string Id { get; set; }= string.Empty;

    string IPersistentObject.CollectionName() => "buckets";

    [FirestoreProperty]
    public Timestamp TimeStamp { get; set; }
        = Timestamp.FromDateTime(DateTime.UtcNow.AddHours(2));

    [FirestoreProperty]
    public string ResponseTemplate { get; set; } = string.Empty;

    [FirestoreProperty]
    public List<CustomHttpRequestEntity> Requests { get; set; } 
        = new();

}

[FirestoreData]
[AutoMap(typeof(CustomHttpRequest), ReverseMap = true)]
public class CustomHttpRequestEntity
{
    
    [FirestoreProperty]
    public string Method { get; set; }= string.Empty;
    
    [FirestoreProperty]
    public string Body { get; set; }= string.Empty;
    
    [FirestoreProperty]
    public string? ContentType { get; set; }

    [FirestoreProperty]
    public Dictionary<string, string> Cookies { get; set; }
        = new();
    
    [FirestoreProperty]
    public Dictionary<string, List<string?>> Query { get; set; } 
        = new();
    
    [FirestoreProperty]
    public Dictionary<string, List<string?>> FormCollection { get; set; } 
        = new();
    
    [FirestoreProperty]
    public Dictionary<string, object?> RouteValues { get; set; }
        = new();
    
    [FirestoreProperty]
    public Dictionary<string,List<string?>> Headers { get; set; }
        = new();
}