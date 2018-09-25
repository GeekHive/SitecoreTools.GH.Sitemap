using System;
using Sitecore.Data.Items;

namespace SitemapGenerator.PipelineArgs
{
    public class SitemapGetModifiedDateArgs : Sitecore.Pipelines.PipelineArgs
    {
        public Item Item { get; set; }

        public DateTime LastModified { get; set; }

        
        public SitemapGetModifiedDateArgs(Item item, DateTime lastModified)
        {
            Item = item;
            LastModified = lastModified;
        }
    }
}