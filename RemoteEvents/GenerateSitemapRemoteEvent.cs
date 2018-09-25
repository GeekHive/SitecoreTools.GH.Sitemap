using System.Runtime.Serialization;
using Sitecore.Eventing;

namespace SitemapGenerator.RemoteEvents
{
    [DataContract]
    public class GenerateSitemapRemoteEvent : IHasEventName
    {
        public GenerateSitemapRemoteEvent(string eventName, string siteName, string siteGroupname)
        {
            SiteName = siteName;
            EventName = eventName;
            SiteGroupName = siteGroupname;
        }
        
        [DataMember]
        public string EventName { get; protected set; }

        [DataMember]
        public string SiteName { get; protected set; }

        [DataMember]
        public string SiteGroupName { get; protected set; }
    }
}