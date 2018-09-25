using System.IO;
using Sitecore.Diagnostics;
using SitemapGenerator.PipelineArgs;

namespace SitemapGenerator.Pipelines.WriteFile
{
    public class WriteSitemapXmlFile
    {
        public void Process(SitemapWriteFileArgs args)
        {
            Assert.ArgumentNotNull(args, nameof(args));

            File.WriteAllText(args.AbsolutePath, args.Text);
            Logger.Info($"Wrote XML for site: {args.SiteContext.Name}, to path: {args.AbsolutePath}");
        }
    }
}