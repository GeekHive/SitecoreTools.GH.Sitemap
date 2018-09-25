using Sitecore.Data.Items;

namespace SitemapGenerator.PipelineArgs
{
    public class SitemapValidateVersionItemArgs : Sitecore.Pipelines.PipelineArgs
    {
        public Item Item { get; set; }
        
        public bool Valid{ get; set; }
        
        public SitemapValidateVersionItemArgs(Item item)
        {
            Item = item;
        }
    }
}