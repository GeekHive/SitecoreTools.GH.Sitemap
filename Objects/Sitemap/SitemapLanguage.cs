using Sitecore.Globalization;

namespace SitemapGenerator.Objects.Sitemap
{
    public class SitemapLanguage
    {
        public Language Language { get; set; }
        
        private string _hrefLang;

        public string HrefLang
        {
            get => _hrefLang ?? Language.CultureInfo.TwoLetterISOLanguageName;
            set => _hrefLang = value;
        }
    }
}