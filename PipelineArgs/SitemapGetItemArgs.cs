using System.Collections.Generic;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Sites;
using SitemapGenerator.Objects.Sitemap;

namespace SitemapGenerator.PipelineArgs
{
    public class SitemapGetItemArgs : Sitecore.Pipelines.PipelineArgs
    {
        public Item Item { get; set; }
        public List<SitemapLanguage> SitemapLanguages { get; set; }
        
        public List<SitemapItem> SitemapItems { get; set; }
        //public SiteContext SiteContext { get; set; }
        public SitemapSiteArgs SitemapSiteArgs { get; set; }

        //public List<Item> ItemLanguageVersions { get; set; }
        
        public SitemapGetItemArgs(Item item, SitemapSiteArgs sitemapSiteArgs)
        {
            Item = item;
            SitemapSiteArgs = sitemapSiteArgs;
            SitemapLanguages = sitemapSiteArgs.SitemapLanguages;
            //ItemLanguageVersions = new List<Item>();
            SitemapItems = new List<SitemapItem>();
        }
    }
}