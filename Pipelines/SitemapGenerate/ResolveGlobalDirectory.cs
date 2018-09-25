using Sitecore.Diagnostics;
using SitemapGenerator.PipelineArgs;

namespace SitemapGenerator.Pipelines.SitemapGenerate
{
    public class ResolveGlobalDirectory
    {
        private static readonly string DirectoryFromSettings = Sitecore.Configuration.Settings.GetSetting("Sitemap.StoreLocation");

        public void Process(SitemapGenerateArgs args)
        {
            Assert.ArgumentNotNull(args, nameof(args));

            args.GlobalDirectoryAbsolutePath = GlobalDirectory();
        }

        public static string GlobalDirectory()
        {
            return Sitecore.MainUtil.MapPath(DirectoryFromSettings);
        }
    }
}