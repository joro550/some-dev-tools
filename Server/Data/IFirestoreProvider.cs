using Google.Cloud.Firestore;
using LanguageExt;
using Query = Google.Cloud.Firestore.Query;

namespace DevTools.Server.Data;

public interface IFirestoreProvider
{
    Task<Unit> AddOrUpdate<T>(T entity, CancellationToken ct = default) where T : IPersistentObject;
    Task<Option<T>> Get<T>(string id, CancellationToken ct = default) where T : IPersistentObject;
    Task<IReadOnlyCollection<T>> GetAll<T>(CancellationToken ct = default) where T : IPersistentObject;
    Task<IReadOnlyCollection<T>> WhereEqualTo<T>(string fieldPath, object value, CancellationToken ct = default) where T : IPersistentObject;
}

public class FirestoreProvider : IFirestoreProvider
{
    private readonly FirestoreDb _fireStoreDb;
    public FirestoreProvider(FirestoreDb fireStoreDb) => _fireStoreDb = fireStoreDb;

    public async Task<Unit> AddOrUpdate<T>(T entity, CancellationToken ct) where T : IPersistentObject
    {
        var document = _fireStoreDb.Collection(typeof(T).Name).Document(entity.Id);
        await document.SetAsync(entity, cancellationToken: ct);
        return Unit.Default;
    }

    public async Task<Option<T>> Get<T>(string id, CancellationToken ct) where T : IPersistentObject
    {
        var document = _fireStoreDb.Collection(typeof(T).Name).Document(id);
        var snapshot = await document.GetSnapshotAsync(ct);
        return snapshot.ConvertTo<T>();
    }

    public async Task<IReadOnlyCollection<T>> GetAll<T>(CancellationToken ct) where T : IPersistentObject
    {
        var collection = _fireStoreDb.Collection(typeof(T).Name);
        var snapshot = await collection.GetSnapshotAsync(ct);
        return snapshot.Documents.Select(x => x.ConvertTo<T>()).ToList();
    }

    public async Task<IReadOnlyCollection<T>> WhereEqualTo<T>(string fieldPath, object value, CancellationToken ct) where T : IPersistentObject
    {
        var whereEqualTo = _fireStoreDb.Collection(typeof(T).Name)
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