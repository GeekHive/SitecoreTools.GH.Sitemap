# Sitemap Generator for Sitecore

A Sitecore utility that enables automatic generation of sitemaps.

Provides hooks to automatically generate sitemaps for sites in a Sitecore instance. Supports individual sitemaps as well as groups for generating sitemap indexes.

### Get Started

To get set up, first include this project in your Sitecore solution. Then copy the provided `Sitemap.example.config` into your `App_Config` directory and modify as desired (See the [Options](#options) below).

Next, add the following handlers to your `Web.config`:
```
<add verb="*" name="SitemapXml" preCondition="integratedMode" path="sitemap.ashx" type="SitemapGenerator.Handlers.SitemapRequestHandler, SitemapGenerator" />
<add verb="*" name="GroupedSiteSitemapXml" preCondition="integratedMode" path="groupedSiteSitemap.ashx" type="SitemapGenerator.Handlers.GroupedSiteSitemapRequestHandler, SitemapGenerator" />
```

Now you can add hooks to Sitecore in order to trigger a manual build via the `sitemap:generate` command, or trigger sitemap generation programmatically via `SitemapUtils.Generate()` (for example, via a Cron job). Both methods take parameters `siteName` and `siteGroupName`, one of which must be provided.

The generator will automatically traverse the site's home item and include in the sitemap any items which have defined presentation details.

Once generated, the sitemap for any particular site can be accessed via `[site hostname]/sitemap.xml`.

### Options

The generator has a few settings, specified in your custom Sitecore configuration file:

| Key | Description | Default | Example |
| --- | ----------- | ------- | ------- |
| Sitemap.ShouldGenerate | Enables / disables Sitemap generation. Must be set to `true` for sitemaps to be generated. | `false` | `<setting name="Sitemap.ShouldGenerate" value="true" />` |
| Sitemap.StoreLocation | A file path, relative to the webroot of the Sitecore instance, where sitemap XML files should be saved. | (required) | ` <setting name="Sitemap.StoreLocation" value="/App_Data/Sitemaps" />` |
| Sitemap.Commands.GenerateLocally | Whether the sitemap rebuilding event should be triggered locally, in addition to remotely. | `false` | `<setting name="Sitemap.Commands.GenerateLocally" value="true" />` |
| Sitemap.IdentifyWildcardItems | (currently unused) Enables / disables support for wildcard items. | `false` | `<setting name="Sitemap.IdentifyWildcardItems" value="true"/>` |
| Sitemap.Groups.MatchPaths | Whether to list alternates for items of sites in sitemap groups, based on the paths to those items being the same. | `false` | `<setting name="Sitemap.Groups.MatchPaths" value="true" />` |
| Sitemap.ManualGeneration.Delay.Seconds | A minimum delay, in seconds, to require between manual sitemap generation via the `sitemap:generate` command hook. Set to `0` for no delay. | `600` | `<setting name="Sitemap.ManualGeneration.Delay.Seconds" value="600" />` |

### Sitemap Groups

In order to support sitemap groups, the `sitemapGroup` attribute must be specified within the properties of any grouped `<site>` items in your Sitecore configuration. This case-insensitive string will be used to determine which sites should be grouped together into one sitemap index. e.g.:
```
<site name="mysiteus"
      [...]
      sitemapGroup="mysite" />
<site name="mysiteuk"
      [...]
      sitemapGroup="mysite" />
<site name="mysitejp"
      [...]
      sitemapGroup="mysite" />
```

In addition, a custom handler must be defined in your Sitecore configuration for each sitemap in the group, with each handler's `trigger` attribute having the format `sitemaps/[sitename].xml`, e.g.:
```
<customHandlers>
  <handler trigger="sitemaps/mysiteus.xml" handler="groupedSiteSitemap.ashx" />
  <handler trigger="sitemaps/mysiteuk.xml" handler="groupedSiteSitemap.ashx" />
  <handler trigger="sitemaps/mysitejp.xml" handler="groupedSiteSitemap.ashx" />
</customHandlers>
```

### TODO:
* Add automatic indexing for sitemaps with more than 50,000 URLs or which exceed 50MB in size.
* Support custom paths for group sitemaps (other than `sitemaps/[sitename].xml`).