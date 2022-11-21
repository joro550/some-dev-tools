using Blazored.LocalStorage;

namespace DevTools.Client.Data;

public class EncodingService<T>
{
    private readonly ILocalStorageService _storageService;
    public EncodingService(ILocalStorageService storageService) => _storageService = storageService;

    public async Task Add(T encoding)
    {
        var items = await _storageService.GetItemAsync<List<T>>(typeof(T).Name);
        if (items == null)
            items = new List<T> { encoding };
        else
            items.Insert(0, encoding);

        await _storageService.SetItemAsync(typeof(T).Name, items);
    }

    public async Task<List<T>> GetAsync()
    {
        var items = await _storageService.GetItemAsync<List<T>>(typeof(T).Name);
        return items ?? new List<T>();
    }
}