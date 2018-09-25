using Sitecore.Diagnostics;
using Sitecore.Pipelines;
using SitemapGenerator.PipelineArgs;

namespace SitemapGenerator.Pipelines.SitemapGenerate
{
    public class GetLanguages
    {
        public void Process(SitemapGenerateArgs args)
        {
            Assert.ArgumentNotNull(args, nameof(args));

            foreach (var sitemapSite in args.SitemapSites)
            {
                CorePipeline.Run("sitemap.GetLanguages", sitemapSite);
            }
        }
    }
}