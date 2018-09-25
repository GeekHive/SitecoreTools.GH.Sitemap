using Sitecore.Diagnostics;
using SitemapGenerator.Extensions;
using SitemapGenerator.PipelineArgs;

namespace SitemapGenerator.Pipelines.BuildSiteXml
{
    public class ConvertToXmlString
    {
        public void Process(SitemapBuildSitemapXmlArgs args)
        {
            Assert.ArgumentNotNull(args, nameof(args));

            args.SitemapSiteArgs.XmlString = args.SitemapSiteArgs.XmlObjects.SitemapToXml();
        }
    }
}