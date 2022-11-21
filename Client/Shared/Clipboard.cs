using Microsoft.JSInterop;

namespace DevTools.Client.Shared;

public interface IClipboard
{
    ValueTask CopyText(string text);
}

public class Clipboard : IClipboard
{
    private readonly IJSRuntime _jsRuntime;

    public Clipboard(IJSRuntime jsRuntime) => _jsRuntime = jsRuntime;

    public async ValueTask CopyText(string text) 
        => await _jsRuntime.InvokeVoidAsync("clipboardCopy.copyText", text);
}