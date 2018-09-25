using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Configuration;
using SitemapGenerator.RemoteEvents;

namespace SitemapGenerator
{
	public class SitemapUtils
	{
		private static readonly bool _generateLocally = Settings.GetBoolSetting("Sitemap.Commands.GenerateLocally", false);

		public static void Generate(string siteName, string siteGroupName)
		{
			var siteRemoteEvent = new GenerateSitemapRemoteEvent("sitemap:generate", siteName, siteGroupName);

			Sitecore.Eventing.EventManager.QueueEvent(siteRemoteEvent, true, _generateLocally);

		    Logger.Info($"Sitemap generation queued for {siteName}");
		}
	}
}
