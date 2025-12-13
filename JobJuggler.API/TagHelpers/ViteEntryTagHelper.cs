using JobJuggler.API.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace JobJuggler.API.TagHelpers;

[HtmlTargetElement("vite-entry", TagStructure = TagStructure.WithoutEndTag)]
public class ViteEntryTagHelper : TagHelper
{
    private readonly IWebHostEnvironment _env;
    
    [HtmlAttributeName("src")]
    public string Src { get; set; } = string.Empty;

    [HtmlAttributeName("app")] public string App { get; set; } = "svelte";

    [ViewContext] public ViewContext ViewContext { get; set; } = null!;
    
    public ViteEntryTagHelper(IWebHostEnvironment env)
    {
        _env = env;
    }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = null;

        var (port, distDir) = App.ToLower() switch
        {
            _ => (5173, "dist")
        };

        if (_env.IsDevelopment() || !_env.IsProduction())
        {
            var script = new TagBuilder("script");
            script.Attributes["type"] = "module";
            script.Attributes["src"] = $"http://localhost:{port}/@vite/client";
            output.Content.AppendHtml(script);
            
            var entryScript = new TagBuilder("script");
            entryScript.Attributes["type"] = "module";
            entryScript.Attributes["src"] = $"http://localhost:{port}/{Src}";
            output.Content.AppendHtml(entryScript);
        }
        else
        {
            var entry = ViteManifest.GetEntry(Src, distDir);
            if (entry == null) return;
            
            if(entry.Css != null)
            {
                foreach (var cssFile in entry.Css)
                {
                    var link = new TagBuilder("link");
                    link.Attributes["rel"] = "stylesheet";
                    link.Attributes["href"] = $"/{distDir}/{cssFile}";
                    output.Content.AppendHtml(link);
                }
            }
            
            var script = new TagBuilder("script");
            script.Attributes["type"] = "module";
            script.Attributes["src"] = $"/{distDir}/{entry.File}";
            output.Content.AppendHtml(script);
        }
    }
}