using System.Collections.Generic;
using Sitecore.Data.Items;
using Sitecore.Sites;

namespace SitemapGenerator.Objects.Sitemap
{
    public class SitemapItem
    {
        public string Path { get; set; }
        public Item Item { get; set; }
        public SiteContext SiteContext { get; set; }
        public SitemapLanguage SitemapLanguage { get; set; }
		public Dictionary<object, object> CustomData { get; set; }

        public SitemapItem(Item item, SiteContext siteContext, SitemapLanguage sitemapLanguage)
        {
            Item = item;
            SiteContext = siteContext;
            Path = GetRelativePath(siteContext.RootPath.ToLower(), item);
            SitemapLanguage = sitemapLanguage;
        }

	    public SitemapItem(Item item, SiteContext siteContext, SitemapLanguage sitemapLanguage,
		    Dictionary<object, object> customData)
	    {
		    Item = item;
		    SiteContext = siteContext;
		    Path = GetRelativePath(siteContext.RootPath.ToLower(), item);
		    SitemapLanguage = sitemapLanguage;
		    CustomData = customData;
	    }


		private static string GetRelativePath(string rootPath, Item item)
        {
            return item.Paths.FullPath.ToLower().Replace(rootPath, "");
        }
    }
}