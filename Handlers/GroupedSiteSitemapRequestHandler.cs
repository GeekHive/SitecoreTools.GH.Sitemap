using System.IO;
using System.Web;
using System.Xml;
using Sitecore;
using Sitecore.Sites;
using SitemapGenerator.Pipelines.SitemapGenerate;

namespace SitemapGenerator.Handlers
{
    public class GroupedSiteSitemapRequestHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            if (Context.Site == null)
                return;

            var filePath = context.Request.FilePath;

            var requestedSitemapFile = filePath.Replace("/sitemaps", "");
            
            var sitemapFilePath = GetSitemapFilePath(Context.Site, requestedSitemapFile);

            var sitemapData = File.ReadAllText(sitemapFilePath);
            
            var doc = new XmlDocument();
            
            doc.LoadXml(sitemapData);
            
            context.Response.ContentType = "text/xml";
            context.Response.ContentEncoding = System.Text.Encoding.UTF8;
            context.Response.Expires = -1;
            context.Response.Cache.SetAllowResponseInBrowserHistory(true);

            doc.Save(context.Response.Output);
        }

        private static string GetSitemapFilePath(SiteContext siteContext, string sitemapFile)
        {
            var group = siteContext.Properties["sitemapGroup"];

            var globalDirectory = ResolveGlobalDirectory.GlobalDirectory();
            
            var siteDirectory = ResolveRelativeSitemapDirectory.SiteRelativeDirectory(globalDirectory, group);

            return $"{siteDirectory}\\{sitemapFile}";
        }
        
        public bool IsReusable => false;
    }
}