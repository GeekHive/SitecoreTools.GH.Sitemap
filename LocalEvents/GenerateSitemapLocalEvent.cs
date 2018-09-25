using System;
using Sitecore.Data.Events;
using Sitecore.Pipelines;
using Sitecore.StringExtensions;
using SitemapGenerator.PipelineArgs;
using SitemapGenerator.RemoteEvents;

namespace SitemapGenerator.LocalEvents
{
    public class GenerateSitemapLocalEvent
    {
        private readonly bool _shouldGenerate = Sitecore.Configuration.Settings.GetBoolSetting("Sitemap.ShouldGenerate", false);

        public void GenerateSitemap(object sender, EventArgs args)
        {
            Logger.Info("Generate Sitemap local event raised.");

            if (!_shouldGenerate)
            {
                Logger.Info("Sitemap generation disabled on this server due to Sitemap.ShouldGenerate setting set to false.");
                return;
            }

            if (!(args is RemoteEventArgs<GenerateSitemapRemoteEvent> generateSitemapArgs))
                throw new InvalidOperationException("Unexpected event args: {0}".FormatWith((object)args.GetType().FullName));

            if (!string.IsNullOrWhiteSpace(generateSitemapArgs.Event.SiteName))
            {
                Logger.Info($"Starting generation of site: {generateSitemapArgs.Event.SiteName}.");
            }

            if (!string.IsNullOrWhiteSpace(generateSitemapArgs.Event.SiteGroupName))
            {
                Logger.Info($"Starting generation of site group: {generateSitemapArgs.Event.SiteGroupName}.");
            }

            var sitemapGenerateArgs = new SitemapGenerateArgs(generateSitemapArgs.Event.SiteName, generateSitemapArgs.Event.SiteGroupName);

            CorePipeline.Run("sitemap.Generate", sitemapGenerateArgs);
        }
    }
}