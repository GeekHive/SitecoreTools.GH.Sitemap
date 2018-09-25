using System.Collections.Generic;
using SitemapGenerator.Objects.Sitemap;

namespace SitemapGenerator.PipelineArgs
{
    public class SitemapGetItemsArgs : Sitecore.Pipelines.PipelineArgs
    {
        public SitemapSiteArgs SitemapSiteArgs { get; set; }
        public List<SitemapItem> SitemapItems { get; set; }

        public SitemapGetItemsArgs(SitemapSiteArgs sitemapSiteArgs)
        {
            SitemapSiteArgs = sitemapSiteArgs;
            SitemapItems = new List<SitemapItem>();
        }
    }
}