using Sitecore.Diagnostics;
using Sitecore.Pipelines;
using SitemapGenerator.Objects.Sitemap;
using SitemapGenerator.PipelineArgs;

namespace SitemapGenerator.Pipelines.ProcessItem
{
    public class AddItemVersions
    {
        public void Process(SitemapGetItemArgs args)
        {
            Assert.ArgumentNotNull(args, nameof(args));

            foreach (var sitemapLanguage in args.SitemapLanguages)
            {
                var itemLanguageVersion = args.Item.Versions.GetLatestVersion(sitemapLanguage.Language);

                var sitemapValidateVersionItemArgs = new SitemapValidateVersionItemArgs(itemLanguageVersion);

                CorePipeline.Run("sitemap.ValidateItemVersion", sitemapValidateVersionItemArgs);

                if (sitemapValidateVersionItemArgs.Valid)
                {
                    args.SitemapItems.Add(
                        new SitemapItem(itemLanguageVersion, args.SitemapSiteArgs.SiteContext, sitemapLanguage)
                    );
                }
            }
        }
    }
}