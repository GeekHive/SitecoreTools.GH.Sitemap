using System.Collections.Generic;
using SitemapGenerator.Objects.Sitemap;

namespace SitemapGenerator.PipelineArgs
{
    public class SitemapBuildSitemapXmlArgs : Sitecore.Pipelines.PipelineArgs
    {
        public SitemapSiteArgs SitemapSiteArgs { get; set; }
        public List<SitemapItem> SitemapItems { get; set; }
        public bool IsGroup { get; set; }
        
        public SitemapBuildSitemapXmlArgs(SitemapSiteArgs sitemapSiteArgs, List<SitemapItem> sitemapItems, bool isGroup = false)
        {
            SitemapSiteArgs = sitemapSiteArgs;
            SitemapItems = sitemapItems;
            IsGroup = isGroup;
        }
    }
}