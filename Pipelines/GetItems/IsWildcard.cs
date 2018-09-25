using Sitecore.Configuration;
using Sitecore.Diagnostics;
using SitemapGenerator.PipelineArgs;

namespace SitemapGenerator.Pipelines.GetItems
{
	public class IsWildcard
	{
		private readonly bool _identifyWildcardItems = Settings.GetBoolSetting("Sitemap.IdentifyWildcardItems", false);
		public void Process(SitemapValidateVersionItemArgs args)
		{
			Assert.ArgumentNotNull(args, nameof(args));

			if (args.Item == null)
			{
				args.Valid = false;
				return;
			}

			if (!_identifyWildcardItems) return;
			var isWildcard = args.Item.Name.Equals("*");
			if (isWildcard)
			{
				args.Valid = true;
			}
		}
	}
}
