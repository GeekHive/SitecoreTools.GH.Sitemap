using System.IO;
using System.Web;
using System.Xml;
using Sitecore;
using Sitecore.Data.Items;
using Sitecore.Sites;
using SitemapGenerator.Pipelines.SitemapGenerate;

namespace SitemapGenerator.Handlers
{
    public class SitemapRequestHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            if (Context.Site == null)
                return;

            var sitemapFilePath = GetSitemapFilePath(Context.Site);

            var sitemapData = File.ReadAllText(sitemapFilePath);
            
            var doc = new XmlDocument();
            
            doc.LoadXml(sitemapData);
            
            context.Response.ContentType = "text/xml";
            context.Response.ContentEncoding = System.Text.Encoding.UTF8;
            context.Response.Expires = -1;
            context.Response.Cache.SetAllowResponseInBrowserHistory(true);

            doc.Save(context.Response.Output);
        }

        private static string GetSitemapFilePath(SiteContext siteContext)
        {
            var group = siteContext.Properties["sitemapGroup"];

            var globalDirectory = ResolveGlobalDirectory.GlobalDirectory();

            var siteName = !string.IsNullOrWhiteSpace(group) ? group : ItemUtil.ProposeValidItemName(siteContext.Name);

            var siteDirectory = ResolveRelativeSitemapDirectory.SiteRelativeDirectory(globalDirectory, siteName);

            if (!string.IsNullOrWhiteSpace(group))
            {
                return $"{siteDirectory}\\index.xml";
            }

            return $"{siteDirectory}\\{ItemUtil.ProposeValidItemName(siteContext.Name)}.xml";
        }
        
        public bool IsReusable => false;
    }
}