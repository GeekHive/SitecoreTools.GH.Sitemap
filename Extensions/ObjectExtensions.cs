using System.IO;
using System.Xml.Serialization;

namespace SitemapGenerator.Extensions
{
    public static class ObjectExtensions
    {
        public static string SitemapToXml<T>(this T data)
        {
            if (data == null)
                return null;

            var xmlSerializer = new XmlSerializer(typeof(T));

            using (var textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, data);
                var str = textWriter.ToString();
                return str;
            }
        }
    }
}