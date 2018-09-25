using System;
using System.Xml.Serialization;

namespace SitemapGenerator.Objects.Xml
{
    [XmlRoot(ElementName = "sitemap", Namespace = "http://www.sitemaps.org/schemas/sitemap/0.9")]
    public class Sitemap
    {
        [XmlElement(ElementName = "loc", Namespace = "http://www.sitemaps.org/schemas/sitemap/0.9")]
        public string Loc { get; set; }
        [XmlElement(ElementName = "lastmod", Namespace = "http://www.sitemaps.org/schemas/sitemap/0.9")]
        public string Lastmod { get; set; }
        
        public Sitemap(string loc, DateTime lastMod)
        {
            Loc = loc;
            Lastmod = lastMod.ToString("yyyy-MM-dd");
        }

        [Obsolete("Needed for serialization only", true)]
        public Sitemap()
        {
        }
    }
}