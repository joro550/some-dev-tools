using DevTools.Shared;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using Microsoft.AspNetCore.Mvc;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace DevTools.Server.Controllers;

[ApiController, Route("api/blog")]
public class BlogController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetPosts() 
        => Ok(await GetFiles());

    private static async Task<List<Post>> GetFiles()
    {
        var currentDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Posts");
        var files = Directory.GetFiles(currentDirectory);
        
        var posts = new List<Post>();
        foreach (var file in files)
        {
            var content = await System.IO.File.ReadAllTextAsync(file);
            posts.Add(GetYaml(content));
        }
        return posts;
    }

    private static Post GetYaml(string content)
    {
        var yamlDeserializer = new DeserializerBuilder()
            .WithNamingConvention(new CamelCaseNamingConvention())
            .Build();

        using var input = new StringReader(content);
        var parser = new Parser(input);
        parser.Expect<StreamStart>();
        parser.Expect<DocumentStart>();
        var post = yamlDeserializer.Deserialize<Post>(parser);
        parser.Expect<DocumentEnd>();
        return post;
    }
}