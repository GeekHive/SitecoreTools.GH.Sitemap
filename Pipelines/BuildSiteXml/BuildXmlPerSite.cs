using System;
using System.Collections.Generic;
using System.Linq;
using Sitecore;
using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Data.Managers;
using Sitecore.Diagnostics;
using Sitecore.ExperienceEditor.Utils;
using Sitecore.Pipelines;
using Sitecore.Sites;
using Sitecore.Web;
using SitemapGenerator.Objects.Sitemap;
using SitemapGenerator.Objects.Xml;
using SitemapGenerator.PipelineArgs;

namespace SitemapGenerator.Pipelines.BuildSiteXml
{
    public class BuildXmlPerSite
    {
        public void Process(SitemapBuildSitemapXmlArgs args)
        {
            Assert.ArgumentNotNull(args, nameof(args));

            var urlSet = new Urlset();

            var filteredSitemapItems = args.SitemapItems;

            if (args.IsGroup)
            {
                filteredSitemapItems = filteredSitemapItems.Where(sitemapItem =>
                    sitemapItem.SiteContext.Name == args.SitemapSiteArgs.SiteContext.Name).ToList();
            }

            foreach (var sitemapItem in filteredSitemapItems)
            {
                var path = sitemapItem.Path;

                // TODO: optimize this loop
                var alternates = args.SitemapItems.Where(item => item.Path == path);

                // Cycle through once to get all links for itemAlternates
                //
                // e.g.
                //
                // <xhtml:link rel="alternate" hreflang="en" href="http://www.site.com/"  />
                // <xhtml:link rel="alternate" hreflang="fr-ca" href="http://www.site.com/fr" />
                //
                var links = BuildAlternates(alternates, args.SitemapSiteArgs);

                // output individual loc and lastmod time per https://support.google.com/webmasters/answer/2620865?hl=en
                //
                // e.g.
                //
                //<url>
                //  <loc>http://www.site.com/</loc>
                //  <lastmod>2018-01-16</lastmod>
                //  <xhtml:link rel="alternate" hreflang="en" href="http://www.site.com/" />
                //  <xhtml:link rel="alternate" hreflang="fr-ca" href="http://www.site.com/fr" />
                //</url>
                //<url>
                //  <loc>http://www.site.com/fr</loc>
                //  <lastmod>2018-03-26</lastmod >
                //  <xhtml:link rel="alternate" hreflang="en" href="http://www.site.com" />
                //  <xhtml:link rel="alternate" hreflang="fr-ca" href="http://www.site.com/fr" />
                //</url>
                //
                var url = BuildUrl(links, sitemapItem);

                urlSet.Url.Add(url);
            }

            args.SitemapSiteArgs.XmlObjects = urlSet;
        }

        private static Url BuildUrl(List<Link> links, SitemapItem sitemapItem)
        {
            var matchedItem = links.FirstOrDefault(link => link.Item == sitemapItem.Item);

            if (matchedItem == null)
            {
                Logger.Warn(
                    $"Unable to retrieve item from alternate links: {sitemapItem.Item.Paths.FullPath}, language: {sitemapItem.Item.Language.Name}");
                return null;
            }

            var loc = matchedItem.Href;

            var sitemapGetModifiedDateArgs = new SitemapGetModifiedDateArgs(sitemapItem.Item, DateTime.MinValue);
            CorePipeline.Run("sitemap.GetModifiedDate", sitemapGetModifiedDateArgs);

            var lastmod = sitemapGetModifiedDateArgs.LastModified;

            if (lastmod == DateTime.MinValue)
            {
                Logger.Warn(
                    $"No modified date found for item: {sitemapItem.Item.Paths.FullPath}, language: {sitemapItem.Item.Language.Name}");
                return null;
            }

            var url = new Url
            {
                Loc = loc,
                Lastmod = lastmod.ToString("yyyy-MM-dd"),
                Link = links
            };

            return url;
        }

        private static List<Link> BuildAlternates(IEnumerable<SitemapItem> alternates, SitemapSiteArgs sitemapSiteArgs)
        {
            var links = new List<Link>();

            foreach (var alternate in alternates)
            {
                var sitemapItemLinkArgs = new SitemapItemLinkArgs(alternate);

                CorePipeline.Run("sitemap.GetItemLink", sitemapItemLinkArgs);

                if (string.IsNullOrWhiteSpace(sitemapItemLinkArgs.Link))
                {
                    Logger.Warn(
                        $"Unable to determine link for item: {alternate.Item.Paths.Path} and site: {sitemapSiteArgs.SiteContext.Name}");
                    continue;
                }

                var customLang = sitemapSiteArgs.SitemapLanguages.FirstOrDefault(i =>
                    string.Equals(i.Language.Name, alternate.Item.Language.Name,
                        StringComparison.InvariantCultureIgnoreCase));

                if (customLang == null)
                {
                    Logger.Warn(
                        $"Unable to find CustomLanguage for item: {alternate.Item.Paths.Path}, language: {alternate.Item.Language.Name}, site: {sitemapSiteArgs.SiteContext.Name}");
                    continue;
                }

                links.Add(new Link()
                {
                    Href = sitemapItemLinkArgs.Link,
                    Hreflang = customLang.HrefLang,
                    Rel = "alternate",
                    Item = alternate.Item
                });
            }

            return links;
        }
    }

}