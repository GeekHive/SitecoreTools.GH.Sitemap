using System;
using System.Linq;
using Sitecore.Diagnostics;
using Sitecore.Sites;
using Sitecore.Web;
using SitemapGenerator.PipelineArgs;

namespace SitemapGenerator.Pipelines.SitemapGenerate
{
    public class GetSites
    {
        public void Process(SitemapGenerateArgs args)
        {
            Assert.ArgumentNotNull(args, nameof(args));

            if (!string.IsNullOrWhiteSpace(args.SiteGroupName))
            {
                var sites = SiteContextFactory.Sites.Where(i => string.Equals(i.Properties["sitemapGroup"], args.SiteGroupName,
                    StringComparison.InvariantCultureIgnoreCase));

                foreach (var site in sites)
                {
                    args.SitemapSites.Add(
                        new SitemapSiteArgs(
                            new SiteContext(site), args.SiteDirectoryAbsolutePath));
                }
                return;
            }

            if (!string.IsNullOrWhiteSpace(args.SiteName))
            {
                var site = SiteManager.GetSite(args.SiteName);

                if (site == null)
                {
                    Logger.Error($"Unable to resolve site with SiteManager: {args.SiteName}");
                    args.AbortPipeline();
                    return;
                }

                args.SitemapSites.Add(
                    new SitemapSiteArgs(
                        new SiteContext(
                            new SiteInfo(site.Properties)), args.SiteDirectoryAbsolutePath));
                return;
            }

            Logger.Error($"No SiteName or SiteGroupName found, aborting.");
            args.AbortPipeline();
        }
    }
}