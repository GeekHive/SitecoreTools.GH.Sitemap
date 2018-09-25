using Sitecore.Diagnostics;
using SitemapGenerator.PipelineArgs;

namespace SitemapGenerator.Pipelines.SitemapGenerate
{
    public class ResolveRelativeSitemapDirectory
    {
        public void Process(SitemapGenerateArgs args)
        {
            Assert.ArgumentNotNull(args, nameof(args));

            args.SiteDirectoryAbsolutePath = SiteRelativeDirectory(args.GlobalDirectoryAbsolutePath, $"\\{args.ProposedSitemapName}");
        }

        public static string SiteRelativeDirectory(string globalAbsolutePath, string siteName)
        {
            return globalAbsolutePath + "\\" + siteName;
        }
    }
}