using System.Linq;
using Sitecore.Diagnostics;
using SitemapGenerator.PipelineArgs;

namespace SitemapGenerator.Pipelines.SitemapGenerate
{
    public class OrderByPath
    {
        public void Process(SitemapGenerateArgs args)
        {
            Assert.ArgumentNotNull(args, nameof(args));

            args.SitemapItems = args.SitemapItems.OrderBy(i => i.Item.Paths.FullPath.Length).ToList();
        }
    }
}