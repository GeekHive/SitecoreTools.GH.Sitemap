using SitemapGenerator.Objects.Sitemap;

namespace SitemapGenerator.PipelineArgs
{
    public class SitemapItemLinkArgs : Sitecore.Pipelines.PipelineArgs
    {
        public SitemapItem CustomItem { get; set; }
        public string Link { get; set; }
        
        public SitemapItemLinkArgs(SitemapItem item)
        {
            CustomItem = item;
        }
    }
}