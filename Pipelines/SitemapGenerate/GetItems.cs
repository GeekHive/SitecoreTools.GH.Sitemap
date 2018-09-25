using Sitecore.Diagnostics;
using Sitecore.Pipelines;
using SitemapGenerator.PipelineArgs;

namespace SitemapGenerator.Pipelines.SitemapGenerate
{
    public class GetItems
    {
        public void Process(SitemapGenerateArgs args)
        {
            Assert.ArgumentNotNull(args, nameof(args));

            foreach (var sitemapSite in args.SitemapSites)
            {
                var sitemapGetItemsArgs = new SitemapGetItemsArgs(sitemapSite);

                CorePipeline.Run("sitemap.GetItems", sitemapGetItemsArgs);

                args.SitemapItems.AddRange(sitemapGetItemsArgs.SitemapItems);
            }
        }
    }
}