using System.Linq;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Pipelines;
using SitemapGenerator.PipelineArgs;

namespace SitemapGenerator.Pipelines.GetItems
{
    public class TraverseTree
    {
        public void Process(SitemapGetItemsArgs args)
        {
            Assert.ArgumentNotNull(args, nameof(args));
            
            var homeItemPath = args.SitemapSiteArgs.SiteContext.StartPath;

            var homeItem = args.SitemapSiteArgs.SiteContext.Database.GetItem(homeItemPath);
            
            Recurse(homeItem, args);
        }

        private static void Recurse(Item item, SitemapGetItemsArgs args)
        {
            var sitemapGetItemArgs = new SitemapGetItemArgs(item, args.SitemapSiteArgs);

            CorePipeline.Run("sitemap.ProcessItem", sitemapGetItemArgs);

            args.SitemapItems.AddRange(sitemapGetItemArgs.SitemapItems);

            foreach (var child in item.GetChildren().Select(i => i))
            {
                Recurse(child, args);
            }
        }
    }
}