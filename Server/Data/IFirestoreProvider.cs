using Google.Cloud.Firestore;
using LanguageExt;
using Query = Google.Cloud.Firestore.Query;

namespace DevTools.Server.Data;

public interface IFirestoreProvider
{
    Task<Option<T>> AddOrUpdate<T>(T entity, CancellationToken ct = default) where T : IPersistentObject, new();
    Task<Option<T>> GetAsync<T>(string id, CancellationToken ct = default) where T : IPersistentObject, new();
    Task<IReadOnlyCollection<T>> GetAll<T>(CancellationToken ct = default) where T : IPersistentObject, new();
    Task<IReadOnlyCollection<T>> WhereEqualTo<T>(string fieldPath, object value, CancellationToken ct = default) where T : IPersistentObject, new();
}

public class FirestoreProvider : IFirestoreProvider
{
    private readonly FirestoreDb _fireStoreDb;
    public FirestoreProvider(FirestoreDb fireStoreDb) => _fireStoreDb = fireStoreDb;

    public async Task<Option<T>> AddOrUpdate<T>(T entity, CancellationToken ct) where T : IPersistentObject, new()
    {
        var collection = _fireStoreDb.Collection(entity.CollectionName());

        if (string.IsNullOrEmpty(entity.Id))
        {
            var reference = await collection.AddAsync(entity, ct);
            return await GetAsync<T>(reference.Id, ct);
        }

        await collection
            .Document(entity.Id)
            .SetAsync(entity, cancellationToken: ct);
        return entity;

    }

    public async Task<Option<T>> GetAsync<T>(string id, CancellationToken ct) where T : IPersistentObject, new()
    {
        var item = new T();
        
        var document = _fireStoreDb.Collection(item.CollectionName()).Document(id);
        var snapshot = await document.GetSnapshotAsync(ct);
        return snapshot.ConvertTo<T>();
    }

    public async Task<IReadOnlyCollection<T>> GetAll<T>(CancellationToken ct) where T : IPersistentObject, new()
    {
        var item = new T();
        var collection = _fireStoreDb.Collection(item.CollectionName());
        var snapshot = await collection.GetSnapshotAsync(ct);
        return snapshot.Documents.Select(x => x.ConvertTo<T>()).ToList();
    }

    public async Task<IReadOnlyCollection<T>> WhereEqualTo<T>(string fieldPath, object value, CancellationToken ct) where T : IPersistentObject, new()
    {
        var item = new T();
        var whereEqualTo = _fireStoreDb.Collection(item.CollectionName())
            .WhereEqualTo(fieldPath, value);
        
        return await GetList<T>(whereEqualTo, ct);
    }

    // just add here any method you need here WhereGreaterThan, WhereIn etc ...

    private static async Task<IReadOnlyCollection<T>> GetList<T>(Query query, CancellationToken ct) where T : IPersistentObject
    {
        try
        {
            var snapshot = await query.GetSnapshotAsync(ct);
            return snapshot.Documents.Select(x => x.ConvertTo<T>()).ToList();
        }
        catch (Exception e)
        {
            return new List<T>();
        }
    }
}