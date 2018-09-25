using Sitecore.Diagnostics;
using Sitecore.Pipelines;
using SitemapGenerator.PipelineArgs;

namespace SitemapGenerator.Pipelines.WriteSitemaps
{
    public class WriteFiles
    {
        public void Process(SitemapGenerateArgs args)
        {
            Assert.ArgumentNotNull(args, nameof(args));
            
            foreach (var sitemapSite in args.SitemapSites)
            {
                var sitemapWriteFileArgs = new SitemapWriteFileArgs(sitemapSite.SiteContext, sitemapSite.AbsolutePath, sitemapSite.XmlString);

                CorePipeline.Run("sitemap.WriteFile", sitemapWriteFileArgs);
            }
        }
    }
}