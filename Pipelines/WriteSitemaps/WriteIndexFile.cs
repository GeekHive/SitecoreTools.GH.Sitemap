using System;
using System.IO;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using SitemapGenerator.Extensions;
using SitemapGenerator.Objects.Xml;
using SitemapGenerator.PipelineArgs;

namespace SitemapGenerator.Pipelines.WriteSitemaps
{
    public class WriteIndexFile
    {
        public void Process(SitemapGenerateArgs args)
        {
            Assert.ArgumentNotNull(args, nameof(args));

            if (!args.IsSiteGroup || args.SitemapSites.Count <= 1)
            {
                return;
            }

            var sitemapindex = new Sitemapindex();

            foreach (var sitemapSite in args.SitemapSites)
            {
                var scheme = sitemapSite.SiteContext.Properties["scheme"] ?? "http";

                var groupUrl = $"{scheme}://{sitemapSite.SiteContext.TargetHostName}";

                var loc = $"{groupUrl}/sitemaps/{ItemUtil.ProposeValidItemName(sitemapSite.SiteContext.Name)}.xml";

                var sitemap = new Sitemap(loc, DateTime.Now);

                sitemapindex.Sitemaps.Add(sitemap);
            }

            Logger.Info($"Generated SiteIndex object for site group: {args.SiteGroupName}");

            var xmlString = sitemapindex.SitemapToXml();

            var indexPath = $"{args.SiteDirectoryAbsolutePath}\\index.xml";

            File.WriteAllText(indexPath, xmlString);
            Logger.Info($"Wrote SiteIndex XML for site group: {args.SiteGroupName}");
        }
    }
}