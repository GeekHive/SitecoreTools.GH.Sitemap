using Sitecore;
using Sitecore.Configuration;
using Sitecore.Shell.Framework.Commands;
using Sitecore.Web.UI.Sheer;
using SitemapGenerator.RemoteEvents;

namespace SitemapGenerator.Commands
{
    public class GenerateSitemapCommand : Command
    {
        private static readonly double _localDelay = Settings.GetDoubleSetting("Sitemap.ManualGeneration.Delay.Seconds", 600);

        public override void Execute(CommandContext context)
        {
            var siteName = context.Parameters["siteName"];
            var siteGroupName = context.Parameters["siteGroupName"];

            if (string.IsNullOrWhiteSpace(siteName) && string.IsNullOrWhiteSpace(siteGroupName))
                return;

            var generationIndex = $"sitemap-{(string.IsNullOrWhiteSpace(siteName) ? siteGroupName : siteName)}";
            
            var coreDb = Factory.GetDatabase("core");

            var previousGeneration = coreDb.Properties[generationIndex];

            if (string.IsNullOrWhiteSpace(previousGeneration))
            {
                SitemapUtils.Generate(siteName, siteGroupName);
                coreDb.Properties[generationIndex] = DateUtil.IsoNow;
                return;
            }
            
            var previousGenerationDateTime = DateUtil.IsoDateToDateTime(previousGeneration);
            var currentDateTime = DateUtil.IsoDateToDateTime(DateUtil.IsoNow);
            var possibleRunDateTime = previousGenerationDateTime.AddSeconds(_localDelay);

            if (possibleRunDateTime > currentDateTime)
            {
                var remainingTime = possibleRunDateTime - currentDateTime;
                SheerResponse.Alert($"Unable to generate sitemap. Must wait {remainingTime.Minutes} minute{(remainingTime.Minutes == 1 ? "" : "s")} {remainingTime.Seconds} second{(remainingTime.Seconds == 1 ? "" : "s")} before attempting to manually regenerate sitemap");
                return;
            }

            SitemapUtils.Generate(siteName, siteGroupName);
            coreDb.Properties[generationIndex] = DateUtil.IsoNow;
        }

    }
}