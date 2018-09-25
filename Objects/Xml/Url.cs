using System.Collections.Generic;
using System.Xml.Serialization;

namespace SitemapGenerator.Objects.Xml
{
    [XmlRoot(ElementName = "url", Namespace = "http://www.sitemaps.org/schemas/sitemap/0.9")]
    public class Url
    {
        [XmlElement(ElementName = "loc", Namespace = "http://www.sitemaps.org/schemas/sitemap/0.9")]
        public string Loc { get; set; }
        [XmlElement(ElementName = "lastmod", Namespace = "http://www.sitemaps.org/schemas/sitemap/0.9")]
        public string Lastmod { get; set; }
        [XmlElement(ElementName = "link", Namespace = "http://www.w3.org/1999/xhtml")]
        public List<Link> Link { get; set; }

        public Url()
        {
            Link = new List<Link>();
        }
    }
}