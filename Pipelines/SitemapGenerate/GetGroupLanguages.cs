using System.Collections.Generic;
using System.Linq;
using Sitecore.Diagnostics;
using SitemapGenerator.Extensions;
using SitemapGenerator.Objects.Sitemap;
using SitemapGenerator.PipelineArgs;

namespace SitemapGenerator.Pipelines.SitemapGenerate
{
    public class GetGroupLanguages
    {
        private readonly bool _matchPaths = Sitecore.Configuration.Settings.GetBoolSetting("Sitemap.Groups.MatchPaths", false);

        public void Process(SitemapGenerateArgs args)
        {
            Assert.ArgumentNotNull(args, nameof(args));

            if (!_matchPaths)
            {
                Logger.Info("Site Group languages not included due to Sitemap.Groups.MatchPaths being set to false.");
                return;
            }

            var allLanguages = new List<SitemapLanguage>();

            foreach (var sitemapSite in args.SitemapSites)
            {
                allLanguages.AddRange(sitemapSite.SitemapLanguages);
            }

            allLanguages = allLanguages.DistinctBy(lang => lang.Language.Name).ToList();

            foreach(var sitemapSite in args.SitemapSites)
            {
                sitemapSite.SitemapLanguages = allLanguages;
            }
        }
    }
}