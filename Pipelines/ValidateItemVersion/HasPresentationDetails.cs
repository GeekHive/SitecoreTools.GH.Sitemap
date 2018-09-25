using Sitecore.Diagnostics;
using SitemapGenerator.PipelineArgs;

namespace SitemapGenerator.Pipelines.ValidateItemVersion
{
    public class HasPresentationDetails
    {
        public void Process(SitemapValidateVersionItemArgs args)
        {
            Assert.ArgumentNotNull(args, nameof(args));

            if (args.Item == null)
            {
                args.Valid = false;
                return;
            }

            args.Valid = 
                (
                    args.Item.Fields[Sitecore.FieldIDs.LayoutField] != null && !string.IsNullOrEmpty(args.Item.Fields[Sitecore.FieldIDs.LayoutField].Value)
                )
                ||
                (
                    args.Item.Fields[Sitecore.FieldIDs.FinalLayoutField] != null && !string.IsNullOrEmpty(args.Item.Fields[Sitecore.FieldIDs.FinalLayoutField].Value)
                );
        }
    }
}