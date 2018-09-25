using System.Collections.Generic;
using Sitecore.Data.Items;
using Sitecore.Sites;
using SitemapGenerator.Objects.Sitemap;
using SitemapGenerator.Objects.Xml;

namespace SitemapGenerator.PipelineArgs
{
    public class SitemapSiteArgs :  Sitecore.Pipelines.PipelineArgs
    {
        public SiteContext SiteContext { get; set; }
        public string AbsolutePath { get; set; }

        public List<SitemapLanguage> SitemapLanguages { get; set; }

        public Urlset XmlObjects { get; set; }
        public string XmlString { get; set; }
        

        public SitemapSiteArgs(SiteContext siteContext, string sitePath)
        {
            SiteContext = siteContext;
            SitemapLanguages = new List<SitemapLanguage>();
            AbsolutePath = sitePath + $"\\{ItemUtil.ProposeValidItemName(siteContext.Name)}.xml";
        }
    }
}