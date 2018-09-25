using Sitecore.Diagnostics;
using Sitecore.Globalization;
using Sitecore.Links;
using Sitecore.Sites;
using SitemapGenerator.PipelineArgs;

namespace SitemapGenerator.Pipelines.GetItemLink
{
    public class ResolveUrl
    {
        public void Process(SitemapItemLinkArgs args)
        {
            Assert.ArgumentNotNull(args, nameof(args));
            
            using (new SiteContextSwitcher(args.CustomItem.SiteContext))
            using (new LanguageSwitcher(args.CustomItem.Item.Language))
            {
                var urlOptions = LinkManager.GetDefaultUrlOptions();

                urlOptions.AlwaysIncludeServerUrl = true;
                
                args.Link = LinkManager.GetItemUrl(args.CustomItem.Item, urlOptions);
            }
        }
    }
}