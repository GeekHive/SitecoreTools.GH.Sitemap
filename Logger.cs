using System;
using log4net;
using Sitecore.Diagnostics;

namespace SitemapGenerator
{
    public static class Logger
    {
        private static readonly ILog _log = LogManager.GetLogger("SitemapGenerator.Logger");

        public static void Info(string message)
        {
            Assert.ArgumentNotNull(message, "message");

            _log.Info(message);
        }

        public static void Info(string message, Exception exception)
        {
            Assert.ArgumentNotNull(message, "message");
            Assert.ArgumentNotNull(exception, "exception");

            _log.Info(message, exception);
        }

        public static void Warn(string message)
        {
            Assert.ArgumentNotNull(message, "message");

            _log.Warn(message);
        }

        public static void Warn(string message, Exception exception)
        {
            Assert.ArgumentNotNull(message, "message");
            Assert.ArgumentNotNull(exception, "exception");

            _log.Warn(message, exception);
        }

        public static void Error(string message)
        {
            Assert.ArgumentNotNull(message, "message");

            _log.Error(message);
        }

        public static void Error(string message, Exception exception)
        {
            Assert.ArgumentNotNull(message, "message");
            Assert.ArgumentNotNull(exception, "exception");

            _log.Error(message, exception);
        }
    }
}