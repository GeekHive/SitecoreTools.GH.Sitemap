using System;
using System.IO;
using Sitecore.Diagnostics;
using SitemapGenerator.PipelineArgs;

namespace SitemapGenerator.Pipelines.SitemapGenerate
{
    public class VerifyGlobalDirectoryAndCreate
    {
        public void Process(SitemapGenerateArgs args)
        {
            Assert.ArgumentNotNull(args, nameof(args));

            if (Directory.Exists(args.GlobalDirectoryAbsolutePath))
            {
                Logger.Info($"Sitemap global directory already exists: {args.GlobalDirectoryAbsolutePath}.");
                return;
            }

            try
            {
                Directory.CreateDirectory(args.GlobalDirectoryAbsolutePath);
                Logger.Info($"Sitemap global directory created: {args.GlobalDirectoryAbsolutePath}.");
            }
            catch (Exception ex)
            {
                Logger.Error($"Error creating directory: {args.GlobalDirectoryAbsolutePath}", ex);
            }
        }
    }
}