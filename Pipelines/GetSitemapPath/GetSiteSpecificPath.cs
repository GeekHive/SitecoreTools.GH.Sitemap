using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using SitemapGenerator.PipelineArgs;
using SitemapGenerator.Pipelines.SitemapGenerate;

namespace SitemapGenerator.Pipelines.GetSitemapPath
{
    public class GetSiteSpecificPath
    {
        public void Process(SitemapGetSitemapPathArgs args)
        {
            Assert.ArgumentNotNull(args, nameof(args));

            var globalDirectory = ResolveGlobalDirectory.GlobalDirectory();

            var sitemapDirectory = ResolveRelativeSitemapDirectory.SiteRelativeDirectory(globalDirectory, ItemUtil.ProposeValidItemName(args.SiteContext.Name));

            args.AbsolutePath = sitemapDirectory + "\\sitemap.xml";
        }
    }
}