using System;
using Sitecore.Data.Items;

namespace SitemapGenerator.PipelineArgs
{
    public class SitemapGetModifiedDateArgs : Sitecore.Pipelines.PipelineArgs
    {
        public Item Item { get; set; }

        public DateTime LastModified { get; set; }

        public bool LanguageFallbackEnabled { get; set; }

        public SitemapGetModifiedDateArgs(Item item, DateTime lastModified) : this(item, lastModified, true)
        {
        }

        public SitemapGetModifiedDateArgs(Item item, DateTime lastModified, bool languageFallbackEnabled)
        {
            Item = item;
            LastModified = lastModified;
            LanguageFallbackEnabled = languageFallbackEnabled;
        }
    }
}