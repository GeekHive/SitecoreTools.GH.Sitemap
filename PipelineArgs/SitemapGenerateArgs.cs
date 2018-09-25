using System.Collections.Generic;
using Sitecore.Data.Items;
using SitemapGenerator.Objects.Sitemap;

namespace SitemapGenerator.PipelineArgs
{
    public class SitemapGenerateArgs : Sitecore.Pipelines.PipelineArgs
    {
        public string SiteName { get; set; }
        public string SiteGroupName { get; set; }

        public bool IsSiteGroup => !string.IsNullOrWhiteSpace(SiteGroupName);
        public string SitemapName => IsSiteGroup ? SiteGroupName : SiteName;
        public string ProposedSitemapName => ItemUtil.ProposeValidItemName(SitemapName);

        public string GlobalDirectoryAbsolutePath { get; set; }
        public string SiteDirectoryAbsolutePath { get; set; }

        public List<SitemapItem> SitemapItems { get; set; }
        
        public List<SitemapSiteArgs> SitemapSites { get; set; }
        
        public SitemapGenerateArgs(string siteName, string siteGroupName)
        {
            SiteName = siteName;
            SiteGroupName = siteGroupName;
            SitemapSites = new List<SitemapSiteArgs>();
            SitemapItems = new List<SitemapItem>();
        }
    }    
}