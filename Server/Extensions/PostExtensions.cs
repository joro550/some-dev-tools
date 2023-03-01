using DevTools.Client.Pages.Blog;
using DevTools.Shared;
using Markdig;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace DevTools.Server.Extensions;

public static class PostExtensions
{
    public static Post FromContent(string fileName, string content, MarkdownPipeline pipeline)
    {
        var frontMatter = GetYaml(content);
        return new Post
        {
            Slug = fileName.Replace(".md", ""),
            Html = Markdown.ToHtml(content, pipeline),
            Title = frontMatter.Title,
            Image = frontMatter.Image,
            ImageAltText = frontMatter.ImageAltText
        };
    }
    
    public static Post FromContent(string fileName, string content)
    {
        var frontMatter = GetYaml(content);
        return new Post
        {
            Slug = fileName.Replace(".md", ""),
            Title = frontMatter.Title,
            Image = frontMatter.Image,
            ImageAltText = frontMatter.ImageAltText
        };
    }
    
    private static Post GetYaml(string content)
    {
        var yamlDeserializer = new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build();

        using var input = new StringReader(content);
        var parser = new Parser(input);
        parser.Consume<StreamStart>();
        parser.Consume<DocumentStart>();
        var post = yamlDeserializer.Deserialize<Post>(parser);
        parser.Consume<DocumentEnd>();
        return post;
    }
}