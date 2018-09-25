using System;
using System.Linq;
using Sitecore.Configuration;
using Sitecore.Data.Items;
using Sitecore.Data.Managers;
using Sitecore.Diagnostics;
using Sitecore.ExperienceEditor.Utils;
using Sitecore.Sites;
using SitemapGenerator.PipelineArgs;

namespace SitemapGenerator.Pipelines.BuildSiteXml
{
    public class GetModifiedDate
    {
        public void Process(SitemapGetModifiedDateArgs args)
        {
            Assert.ArgumentNotNull(args, nameof(args));
            
            if (args.Item.Versions.Count > 0)
            {
                using (new ContextItemSwitcher(args.Item))
                using(new SiteContextSwitcher(Factory.GetSite("shell")))
                {
                    var defaultDevice = args.Item.Database.Resources.Devices.GetAll()
                        .First(d => d.Name.ToLower() == "default");
                    var dependentItems =
                        ItemUtility.GetItemsFromLayoutDefinedDatasources(args.Item, defaultDevice, args.Item.Language);
                    if (!dependentItems.Any())
                    {
                        args.LastModified = args.Item.Statistics.Updated;
                        return;
                    }

                    var dependentUpdatedDate = dependentItems.Max(x => x.Statistics.Updated);
                    args.LastModified = args.Item.Statistics.Updated.CompareTo(dependentUpdatedDate) > 0
                        ? args.Item.Statistics.Updated
                        : dependentUpdatedDate;
                    return;
                }
            }

            var fallbackLanguage = LanguageFallbackManager.GetFallbackLanguage(args.Item.Language, args.Item.Database);

            if (fallbackLanguage == null)
                args.LastModified = DateTime.MinValue;
            if (fallbackLanguage == null || args.LastModified != null)
                return;

            args.Item = args.Item.Database.GetItem(args.Item.ID, fallbackLanguage);
            Process(args);
        }
    }
}