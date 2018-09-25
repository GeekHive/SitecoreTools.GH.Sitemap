using System.Xml.Serialization;
using Sitecore.Data.Items;

namespace SitemapGenerator.Objects.Xml
{
    [XmlRoot(ElementName = "link", Namespace = "http://www.w3.org/1999/xhtml")]
    public class Link
    {
        [XmlAttribute(AttributeName = "rel")]
        public string Rel { get; set; }
        [XmlAttribute(AttributeName = "hreflang")]
        public string Hreflang { get; set; }
        [XmlAttribute(AttributeName = "href")]
        public string Href { get; set; }

        [XmlIgnore]
        public Item Item { get; set; }
    }
}