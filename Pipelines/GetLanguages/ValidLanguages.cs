using System.Collections.Generic;
using Sitecore.Data.Managers;
using Sitecore.Diagnostics;
using SitemapGenerator.Objects.Sitemap;
using SitemapGenerator.PipelineArgs;

namespace SitemapGenerator.Pipelines.GetLanguages
{
    public class ValidLanguages
    {
        public void Process(SitemapSiteArgs args)
        {
            Assert.ArgumentNotNull(args, nameof(args));

            var targetLanguage = args.SiteContext.Language;

            if (string.IsNullOrWhiteSpace(targetLanguage))
            {
                Logger.Error($"No language set for site: {args.SiteContext.Name}");
                args.AbortPipeline();
                return;
            }

            var language = LanguageManager.GetLanguage(targetLanguage);

            if (language == null)
            {
                Logger.Error($"Unable to resolve language for site: {args.SiteContext.Name}, given language name: {targetLanguage}");
                args.AbortPipeline();
                return;
            }

            args.SitemapLanguages = new List<SitemapLanguage>{ new SitemapLanguage{Language  = language} };
        }
    }
}