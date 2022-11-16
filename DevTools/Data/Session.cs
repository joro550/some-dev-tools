namespace DevTools.Data;

public class Session
{
    private readonly string _id;

    public Session() 
        => _id = Guid.NewGuid().ToString("N");

    public string GetSessionId() => _id;
}