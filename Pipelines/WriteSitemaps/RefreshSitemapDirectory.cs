using System.IO;
using Sitecore.Diagnostics;
using SitemapGenerator.PipelineArgs;

namespace SitemapGenerator.Pipelines.WriteSitemaps
{
    public class RefreshSitemapDirectory
    {
        public void Process(SitemapGenerateArgs args)
        {
            Assert.ArgumentNotNull(args, nameof(args));

            if (Directory.Exists(args.SiteDirectoryAbsolutePath))
            {
                DeleteDirectory(args.SiteDirectoryAbsolutePath);
                Logger.Info($"Deleted Sitemap site directory: {args.SiteDirectoryAbsolutePath}.");
            }

            Directory.CreateDirectory(args.SiteDirectoryAbsolutePath);
            Logger.Info($"Created Sitemap directory: {args.SiteDirectoryAbsolutePath}");
        }

        private static void DeleteDirectory(string targetDir)
        {
            var files = Directory.GetFiles(targetDir);
            var dirs = Directory.GetDirectories(targetDir);

            foreach (var file in files)
            {
                File.Delete(file);
            }

            foreach (var dir in dirs)
            {
                DeleteDirectory(dir);
            }

            Directory.Delete(targetDir, false);
        }
    }
}