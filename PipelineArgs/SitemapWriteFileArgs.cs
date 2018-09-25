using Sitecore.Sites;

namespace SitemapGenerator.PipelineArgs
{
    public class SitemapWriteFileArgs : Sitecore.Pipelines.PipelineArgs
    {
        public SiteContext SiteContext { get; set; }
        public string AbsolutePath { get; set; }
        public string Text { get; set; }

        public SitemapWriteFileArgs(SiteContext siteContext, string absolutePath, string text)
        {
            SiteContext = siteContext;
            AbsolutePath = absolutePath;
            Text = text;
        }
    }
}