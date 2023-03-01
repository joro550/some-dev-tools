using Markdig;
using DevTools.Shared;
using Microsoft.AspNetCore.Mvc;
using DevTools.Server.Extensions;

namespace DevTools.Server.Controllers;

[ApiController, Route("api/blog")]
public class BlogController : ControllerBase
{
    private readonly MarkdownPipeline _pipeline;
    
    public BlogController() 
        => _pipeline = new MarkdownPipelineBuilder()
            .UseYamlFrontMatter()
            .Build();

    [HttpGet]
    public async Task<IActionResult> GetPosts() 
        => Ok(await GetFiles());
    
    [HttpGet("{slug}")]
    public async Task<IActionResult> GetPost([FromRoute]string slug) 
        => Ok(await GetFile(slug));
    
    private async Task<Post> GetFile(string slug)
    {
        var currentDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Posts");
        var file = Directory.GetFiles(currentDirectory, $"{slug}.md")
            .FirstOrDefault();

        var content = await System.IO.File.ReadAllTextAsync(file ?? string.Empty);
        return PostExtensions.FromContent(file, content, _pipeline);
    }

    private static async Task<List<Post>> GetFiles()
    {
        var currentDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Posts");

        var posts = new List<Post>();
        foreach (var file in Directory.GetFiles(currentDirectory))
        {
            var content = await System.IO.File.ReadAllTextAsync(file);
            posts.Add(PostExtensions.FromContent(file, content));
        }
        return posts;
    }
}