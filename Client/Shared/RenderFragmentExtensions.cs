using Microsoft.AspNetCore.Components.Rendering;

namespace DevTools.Client.Shared;

public static class RenderFragmentExtensions
{
    public static void Default(this RenderTreeBuilder builder)
    {
        builder.AddContent(0, "<div>Content was Null</div>");
    }
}