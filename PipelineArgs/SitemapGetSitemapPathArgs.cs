using Sitecore.Sites;

namespace SitemapGenerator.PipelineArgs
{
    public class SitemapGetSitemapPathArgs : Sitecore.Pipelines.PipelineArgs
    {
        public SiteContext SiteContext { get; set; }
        public string AbsolutePath { get; set; }
        
        public SitemapGetSitemapPathArgs(SiteContext siteContext)
        {
            SiteContext = siteContext;
        }
    }
}