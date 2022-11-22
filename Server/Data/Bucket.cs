using AutoMapper;
using DevTools.Shared;
using Google.Cloud.Firestore;

namespace DevTools.Server.Data;

public class Bucket : IPersistentObject
{
    [FirestoreDocumentId]
    public string Id { get; set; }= string.Empty;

    [FirestoreProperty] 
    public string Prefix { get; set; } = string.Empty;

    [FirestoreProperty]
    public Timestamp TimeStamp { get; set; }

    [FirestoreProperty]
    public string ResponseTemplate { get; set; }= string.Empty;

    public List<CustomHttpRequestEntity> Requests { get; set; } 
        = new();

    public static string CollectionName => "buckets";
}

[AutoMap(typeof(CustomHttpRequest), ReverseMap = true)]
public class CustomHttpRequestEntity
{
    
    [FirestoreProperty]
    public string Method { get; set; }
    
    [FirestoreProperty]
    public string Body { get; set; }
    
    [FirestoreProperty]
    public string? ContentType { get; set; }
    
    [FirestoreProperty]
    public IEnumerable<KeyValuePair<string, string>> Cookies { get; set; }
    
    [FirestoreProperty]
    public Dictionary<string, List<string?>> Query { get; set; } 
    
    [FirestoreProperty]
    public Dictionary<string, List<string?>> FormCollection { get; set; } 
    
    [FirestoreProperty]
    public Dictionary<string, object?> RouteValues { get; set; }
    
    [FirestoreProperty]
    public Dictionary<string,List<string?>> Headers { get; set; }
}