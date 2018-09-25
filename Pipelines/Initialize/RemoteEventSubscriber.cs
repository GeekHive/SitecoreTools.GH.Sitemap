using Sitecore.Data.Events;
using Sitecore.Eventing;
using Sitecore.Events;
using SitemapGenerator.RemoteEvents;

namespace SitemapGenerator.Pipelines.Initialize
{
    public class RemoteEventSubscriber
    {
        public void Initialize(Sitecore.Pipelines.PipelineArgs args)
        {
            EventManager.Subscribe<GenerateSitemapRemoteEvent>(OnGenericRemoteEvent);
            Logger.Info("Initializing Generate Sitemap Remote event.");
        }

        private static void OnGenericRemoteEvent<TEvent>(TEvent @event) where TEvent : IHasEventName
        {
            Logger.Info("Generate Sitemap Remote event raised.");
            var remoteEventArgs = new RemoteEventArgs<TEvent>(@event);
            Event.RaiseEvent(@event.EventName, remoteEventArgs);
        }
    }
}