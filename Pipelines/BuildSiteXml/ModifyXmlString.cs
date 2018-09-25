using System;
using Sitecore.Diagnostics;
using SitemapGenerator.PipelineArgs;

namespace SitemapGenerator.Pipelines.BuildSiteXml
{
    public class ModifyXmlString
    {
        public void Process(SitemapBuildSitemapXmlArgs args)
        {
            Assert.ArgumentNotNull(args, nameof(args));

            var xmlnsIndex = args.SitemapSiteArgs.XmlString.IndexOf("xmlns=", StringComparison.InvariantCultureIgnoreCase);

            args.SitemapSiteArgs.XmlString = args.SitemapSiteArgs.XmlString.Insert(xmlnsIndex + "xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\"".Length, " xmlns:xhtml=\"http://www.w3.org/1999/xhtml\"");

            args.SitemapSiteArgs.XmlString = args.SitemapSiteArgs.XmlString.Replace("xmlns=\"http://www.w3.org/1999/xhtml\"", "");

            args.SitemapSiteArgs.XmlString = args.SitemapSiteArgs.XmlString.Replace("<link", "<xhtml:link");

            args.SitemapSiteArgs.XmlString = args.SitemapSiteArgs.XmlString.Replace("xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"", "");

            args.SitemapSiteArgs.XmlString = args.SitemapSiteArgs.XmlString.Replace("xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"", "");

            args.SitemapSiteArgs.XmlString = args.SitemapSiteArgs.XmlString.Replace("\"utf-16\"", "\"utf-8\"");
        }
    }
}