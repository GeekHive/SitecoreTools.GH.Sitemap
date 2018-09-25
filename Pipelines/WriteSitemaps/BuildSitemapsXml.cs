using System.Linq;
using Sitecore.Diagnostics;
using Sitecore.Pipelines;
using SitemapGenerator.PipelineArgs;

namespace SitemapGenerator.Pipelines.WriteSitemaps
{
    public class BuildSitemapsXml
    {
        public void Process(SitemapGenerateArgs args)
        {
            Assert.ArgumentNotNull(args, nameof(args));

            if (args.SitemapSites.Count == 1)
            {
                var sitemapBuildSitemapXmlArgs = new SitemapBuildSitemapXmlArgs(args.SitemapSites.FirstOrDefault(), args.SitemapItems);

                CorePipeline.Run("sitemap.BuildSiteXml", sitemapBuildSitemapXmlArgs);
                return;
            }

            foreach (var sitemapSite in args.SitemapSites)
            {
                var sitemapBuildSitemapXmlArgs = new SitemapBuildSitemapXmlArgs(sitemapSite, args.SitemapItems, true);

                CorePipeline.Run("sitemap.BuildSiteXml", sitemapBuildSitemapXmlArgs);
            }
        }
    }
}