using System.Collections.Generic;
using System.Xml.Serialization;

namespace SitemapGenerator.Objects.Xml
{
    [XmlRoot(ElementName = "sitemapindex", Namespace = "http://www.sitemaps.org/schemas/sitemap/0.9")]
    public class Sitemapindex
    {
        [XmlElement(ElementName = "sitemap", Namespace = "http://www.sitemaps.org/schemas/sitemap/0.9")]
        public List<Sitemap> Sitemaps { get; set; }
        [XmlAttribute(AttributeName = "xsd", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Xsd { get; set; }
        [XmlAttribute(AttributeName = "xsi", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Xsi { get; set; }
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }

        public Sitemapindex()
        {
            Sitemaps = new List<Sitemap>();
        }
    }
}