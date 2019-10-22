using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using Feedpipes.Syndication.Extensions.MediaRss.Entities;
using Feedpipes.Syndication.TimeSpans.MinutesSeconds;
using Feedpipes.Syndication.TimeSpans.Rfc2326Npt;
using Feedpipes.Syndication.Utils.Xml;

namespace Feedpipes.Syndication.Extensions.MediaRss
{
    public static class MediaRssExtensionFormatter
    {
        private static readonly XNamespace _media = MediaRssExtensionConstants.Namespace;

        public static bool TryFormatMediaRssExtension(MediaRssExtension extensionToFormat, XNamespaceAliasSet namespaceAliases, ExtensionManifestDirectory extensionManifestDirectory, out IList<XElement> elements)
        {
            elements = default;

            if (extensionToFormat == null)
                return false;

            elements = new List<XElement>();

            // groups
            foreach (var groupToFormat in extensionToFormat.Groups ?? Enumerable.Empty<MediaRssGroup>())
            {
                if (TryFormatMediaRssGroup(groupToFormat, namespaceAliases, extensionManifestDirectory, out var groupElement))
                {
                    elements.Add(groupElement);
                }
            }

            // contents
            foreach (var contentToFormat in extensionToFormat.Contents ?? Enumerable.Empty<MediaRssContent>())
            {
                if (TryFormatMediaRssContent(contentToFormat, namespaceAliases, extensionManifestDirectory, out var contentElement))
                {
                    elements.Add(contentElement);
                }
            }

            // container
            if (TryFormatMediaRssContainer(extensionToFormat, namespaceAliases, extensionManifestDirectory, out var containerChildElements))
            {
                foreach (var containerChildElement in containerChildElements)
                {
                    elements.Add(containerChildElement);
                }
            }

            if (!elements.Any())
                return false;

            namespaceAliases.EnsureNamespaceAlias(MediaRssExtensionConstants.NamespaceAlias, _media);
            return true;
        }

        private static bool TryFormatMediaRssContent(MediaRssContent contentToFormat, XNamespaceAliasSet namespaceAliases, ExtensionManifestDirectory extensionManifestDirectory, out XElement contentElement)
        {
            contentElement = default;

            if (contentToFormat == null)
                return false;

            var childElements = new List<XElement>();
            var attributes = new List<XAttribute>();

            // content fields
            if (!string.IsNullOrEmpty(contentToFormat.Url))
            {
                attributes.Add(new XAttribute("url", contentToFormat.Url));
            }

            if (contentToFormat.FileSize != null)
            {
                attributes.Add(new XAttribute("fileSize", contentToFormat.FileSize.Value.ToString(CultureInfo.InvariantCulture)));
            }

            if (!string.IsNullOrEmpty(contentToFormat.Type))
            {
                attributes.Add(new XAttribute("type", contentToFormat.Type));
            }

            if (contentToFormat.Medium != null)
            {
                attributes.Add(new XAttribute("medium", contentToFormat.Medium.Value.ToString().ToLowerInvariant()));
            }

            if (contentToFormat.IsDefault != null)
            {
                attributes.Add(new XAttribute("isDefault", contentToFormat.IsDefault.Value ? "true" : "false"));
            }

            if (contentToFormat.Expression != MediaRssExpression.Full)
            {
                attributes.Add(new XAttribute("expression", contentToFormat.Expression.ToString().ToLowerInvariant()));
            }

            if (contentToFormat.BitRate != null)
            {
                attributes.Add(new XAttribute("bitrate", contentToFormat.BitRate.Value.ToString("0.####", CultureInfo.InvariantCulture)));
            }

            if (contentToFormat.FrameRate != null)
            {
                attributes.Add(new XAttribute("framerate", contentToFormat.FrameRate.Value.ToString("0.####", CultureInfo.InvariantCulture)));
            }

            if (contentToFormat.SamplingRate != null)
            {
                attributes.Add(new XAttribute("samplingrate", contentToFormat.SamplingRate.Value.ToString("0.####", CultureInfo.InvariantCulture)));
            }

            if (contentToFormat.Channels != null)
            {
                attributes.Add(new XAttribute("channels", contentToFormat.Channels.Value.ToString(CultureInfo.InvariantCulture)));
            }

            if (contentToFormat.Duration != null)
            {
                attributes.Add(new XAttribute("duration", contentToFormat.Duration.Value.TotalSeconds.ToString("0.####", CultureInfo.InvariantCulture)));
            }

            if (contentToFormat.Height != null)
            {
                attributes.Add(new XAttribute("height", contentToFormat.Height.Value.ToString(CultureInfo.InvariantCulture)));
            }

            if (contentToFormat.Width != null)
            {
                attributes.Add(new XAttribute("width", contentToFormat.Width.Value.ToString(CultureInfo.InvariantCulture)));
            }

            if (!string.IsNullOrEmpty(contentToFormat.Lang))
            {
                attributes.Add(new XAttribute("lang", contentToFormat.Lang));
            }

            // container
            if (TryFormatMediaRssContainer(contentToFormat, namespaceAliases, extensionManifestDirectory, out var containerChildElements))
            {
                foreach (var containerChildElement in containerChildElements)
                {
                    childElements.Add(containerChildElement);
                }
            }

            // extensions
            if (ExtensibleEntityFormatter.TryFormatXElementExtensions(contentToFormat, namespaceAliases, extensionManifestDirectory, out var extensionElements))
            {
                childElements.AddRange(extensionElements);
            }

            if (!childElements.Any() && !attributes.Any())
                return false;

            contentElement = new XElement(_media + "content", attributes);
            contentElement.AddRange(childElements);

            return true;
        }

        private static bool TryFormatMediaRssGroup(MediaRssGroup groupToFormat, XNamespaceAliasSet namespaceAliases, ExtensionManifestDirectory extensionManifestDirectory, out XElement groupElement)
        {
            groupElement = default;

            if (groupToFormat == null)
                return false;

            var childElements = new List<XElement>();

            // contents
            foreach (var contentToFormat in groupToFormat.Contents ?? Enumerable.Empty<MediaRssContent>())
            {
                if (TryFormatMediaRssContent(contentToFormat, namespaceAliases, extensionManifestDirectory, out var contentElement))
                {
                    childElements.Add(contentElement);
                }
            }

            // container
            if (TryFormatMediaRssContainer(groupToFormat, namespaceAliases, extensionManifestDirectory, out var containerChildElements))
            {
                foreach (var containerChildElement in containerChildElements)
                {
                    childElements.Add(containerChildElement);
                }
            }

            // extensions
            if (ExtensibleEntityFormatter.TryFormatXElementExtensions(groupToFormat, namespaceAliases, extensionManifestDirectory, out var extensionElements))
            {
                childElements.AddRange(extensionElements);
            }

            if (!childElements.Any())
                return false;

            groupElement = new XElement(_media + "group", childElements);
            return true;
        }

        private static bool TryFormatMediaRssContainer(MediaRssContainer containerToFormat, XNamespaceAliasSet namespaceAliases, ExtensionManifestDirectory extensionManifestDirectory, out IList<XElement> elements)
        {
            elements = default;

            if (containerToFormat == null)
                return false;

            elements = new List<XElement>();

            // optional elements

            foreach (var ratingToFormat in containerToFormat.Ratings ?? Enumerable.Empty<MediaRssRating>())
            {
                if (TryFormatMediaRssRating(ratingToFormat, out var ratingElement))
                {
                    elements.Add(ratingElement);
                }
            }

            if (TryFormatMediaRssTypedText(containerToFormat.Title, "title", out var titleElement))
            {
                elements.Add(titleElement);
            }

            if (TryFormatMediaRssTypedText(containerToFormat.Description, "description", out var descriptionElement))
            {
                elements.Add(descriptionElement);
            }

            if (TryFormatMediaRssKeywords(containerToFormat.Keywords, out var keywordsElement))
            {
                elements.Add(keywordsElement);
            }

            foreach (var thumbnailToFormat in containerToFormat.Thumbnails ?? Enumerable.Empty<MediaRssThumbnail>())
            {
                if (TryFormatMediaRssThumbnail(thumbnailToFormat, out var thumbnailElement))
                {
                    elements.Add(thumbnailElement);
                }
            }

            foreach (var categoryToFormat in containerToFormat.Categories ?? Enumerable.Empty<MediaRssCategory>())
            {
                if (TryFormatMediaRssCategory(categoryToFormat, out var categoryElement))
                {
                    elements.Add(categoryElement);
                }
            }

            foreach (var hashToFormat in containerToFormat.Hashes ?? Enumerable.Empty<MediaRssHash>())
            {
                if (TryFormatMediaRssHash(hashToFormat, out var hashElement))
                {
                    elements.Add(hashElement);
                }
            }

            if (TryFormatMediaRssPlayer(containerToFormat.Player, out var playerElement))
            {
                elements.Add(playerElement);
            }

            foreach (var creditToFormat in containerToFormat.Credits ?? Enumerable.Empty<MediaRssCredit>())
            {
                if (TryFormatMediaRssCredit(creditToFormat, out var creditElement))
                {
                    elements.Add(creditElement);
                }
            }

            if (TryFormatMediaRssCopyright(containerToFormat.Copyright, out var copyrightElement))
            {
                elements.Add(copyrightElement);
            }

            foreach (var textToFormat in containerToFormat.Texts ?? Enumerable.Empty<MediaRssText>())
            {
                if (TryFormatMediaRssText(textToFormat, out var textElement))
                {
                    elements.Add(textElement);
                }
            }

            foreach (var restrictionToFormat in containerToFormat.Restrictions ?? Enumerable.Empty<MediaRssRestriction>())
            {
                if (TryFormatMediaRssRestriction(restrictionToFormat, out var restrictionElement))
                {
                    elements.Add(restrictionElement);
                }
            }

            if (TryFormatMediaRssCommunity(containerToFormat.Community, out var communityElement))
            {
                elements.Add(communityElement);
            }

            if (TryFormatMediaRssStringCollectionElements(containerToFormat.Comments, "comments", "comment", out var commentsElement))
            {
                elements.Add(commentsElement);
            }

            foreach (var embedToFormat in containerToFormat.Embeds ?? Enumerable.Empty<MediaRssEmbed>())
            {
                if (TryFormatMediaRssEmbed(embedToFormat, out var embedElement))
                {
                    elements.Add(embedElement);
                }
            }

            if (TryFormatMediaRssStringCollectionElements(containerToFormat.Responses, "responses", "response", out var responsesElement))
            {
                elements.Add(responsesElement);
            }

            if (TryFormatMediaRssStringCollectionElements(containerToFormat.BackLinks, "backLinks", "backLink", out var backLinksElement))
            {
                elements.Add(backLinksElement);
            }

            if (TryFormatMediaRssStatus(containerToFormat.Status, out var statusElement))
            {
                elements.Add(statusElement);
            }

            foreach (var priceToFormat in containerToFormat.Prices ?? Enumerable.Empty<MediaRssPrice>())
            {
                if (TryFormatMediaRssPrice(priceToFormat, out var priceElement))
                {
                    elements.Add(priceElement);
                }
            }

            if (TryFormatMediaRssLink(containerToFormat.License, "license", out var licenseElement))
            {
                elements.Add(licenseElement);
            }

            foreach (var subTitleToFormat in containerToFormat.SubTitles ?? Enumerable.Empty<MediaRssLink>())
            {
                if (TryFormatMediaRssLink(subTitleToFormat, "subTitle", out var subTitleElement))
                {
                    elements.Add(subTitleElement);
                }
            }

            foreach (var peerLinkToFormat in containerToFormat.PeerLinks ?? Enumerable.Empty<MediaRssLink>())
            {
                if (TryFormatMediaRssLink(peerLinkToFormat, "peerLink", out var peerLinkElement))
                {
                    elements.Add(peerLinkElement);
                }
            }

            foreach (var locationToFormat in containerToFormat.Locations ?? Enumerable.Empty<MediaRssLocation>())
            {
                if (TryFormatMediaRssLocation(locationToFormat, namespaceAliases, extensionManifestDirectory, out var locationElement))
                {
                    elements.Add(locationElement);
                }
            }

            if (TryFormatMediaRssRights(containerToFormat.Rights, out var rightsElement))
            {
                elements.Add(rightsElement);
            }

            if (TryFormatMediaRssScenes(containerToFormat.Scenes, out var scenesElement))
            {
                elements.Add(scenesElement);
            }

            if (!elements.Any())
                return false;

            return true;
        }

        private static bool TryFormatMediaRssScenes(IList<MediaRssScene> scenesToFormat, out XElement scenesElement)
        {
            scenesElement = default;

            if (scenesToFormat == null)
                return false;

            var childElements = new List<XElement>();

            foreach (var sceneToFormat in scenesToFormat)
            {
                if (TryFormatMediaRssScene(sceneToFormat, out var sceneElement))
                {
                    childElements.Add(sceneElement);
                }
            }

            if (!childElements.Any())
                return false;

            scenesElement = new XElement(_media + "scenes", childElements);
            return true;
        }

        private static bool TryFormatMediaRssScene(MediaRssScene sceneToFormat, out XElement sceneElement)
        {
            sceneElement = default;

            if (sceneToFormat == null)
                return false;

            if (!string.IsNullOrEmpty(sceneToFormat.Title))
            {
                // ReSharper disable once ConstantNullCoalescingCondition
                sceneElement = sceneElement ?? new XElement(_media + "scene");
                sceneElement.Add(new XElement("sceneTitle", sceneToFormat.Title));
            }

            if (!string.IsNullOrEmpty(sceneToFormat.Description))
            {
                sceneElement = sceneElement ?? new XElement(_media + "scene");
                sceneElement.Add(new XElement("sceneDescription", sceneToFormat.Description));
            }

            if (MinutesSecondsTimeSpanFormatter.TryFormatTimeAsString(sceneToFormat.StartTime, out var startTimeFormatted))
            {
                sceneElement = sceneElement ?? new XElement(_media + "scene");
                sceneElement.Add(new XElement("sceneStartTime", startTimeFormatted));
            }

            if (MinutesSecondsTimeSpanFormatter.TryFormatTimeAsString(sceneToFormat.EndTime, out var endTimeFormatted))
            {
                sceneElement = sceneElement ?? new XElement(_media + "scene");
                sceneElement.Add(new XElement("sceneEndTime", endTimeFormatted));
            }

            if (sceneElement == null)
                return false;

            return true;
        }

        private static bool TryFormatMediaRssRights(MediaRssRightsStatus? rightsStatusToFormat, out XElement rightsElement)
        {
            rightsElement = default;

            if (rightsStatusToFormat == null)
                return false;

            rightsElement = new XElement(_media + "rights", new XAttribute("status", rightsStatusToFormat.Value.ToString().ToLowerInvariant()));
            return true;
        }

        private static bool TryFormatMediaRssLocation(MediaRssLocation locationToFormat, XNamespaceAliasSet namespaceAliases, ExtensionManifestDirectory extensionManifestDirectory, out XElement locationElement)
        {
            locationElement = default;

            if (locationToFormat == null)
                return false;

            var childObjects = new List<object>();

            if (MinutesSecondsTimeSpanFormatter.TryFormatTimeAsString(locationToFormat.Start, out var startFormatted))
            {
                childObjects.Add(new XAttribute("start", startFormatted));
            }

            if (MinutesSecondsTimeSpanFormatter.TryFormatTimeAsString(locationToFormat.End, out var endFormatted))
            {
                childObjects.Add(new XAttribute("end", endFormatted));
            }

            if (!string.IsNullOrEmpty(locationToFormat.Description))
            {
                childObjects.Add(new XAttribute("description", locationToFormat.Description));
            }

            // extensions
            if (ExtensibleEntityFormatter.TryFormatXElementExtensions(locationToFormat, namespaceAliases, extensionManifestDirectory, out var extensionElements))
            {
                childObjects.AddRange(extensionElements);
            }

            if (!childObjects.Any())
                return false;

            locationElement = new XElement(_media + "location", childObjects);
            return true;
        }

        private static bool TryFormatMediaRssLink(MediaRssLink linkToFormat, string elementName, out XElement linkElement)
        {
            linkElement = default;

            if (string.IsNullOrEmpty(linkToFormat?.Href))
                return false;

            linkElement = new XElement(_media + elementName, new XAttribute("href", linkToFormat.Href));

            if (!string.IsNullOrEmpty(linkToFormat.Type))
            {
                linkElement.Add(new XAttribute("type", linkToFormat.Type));
            }

            if (!string.IsNullOrEmpty(linkToFormat.Lang))
            {
                linkElement.Add(new XAttribute("lang", linkToFormat.Lang));
            }

            return true;
        }

        private static bool TryFormatMediaRssPrice(MediaRssPrice priceToFormat, out XElement priceElement)
        {
            priceElement = default;

            if (priceToFormat == null)
                return false;

            var childAttributes = new List<XAttribute>();

            if (priceToFormat.Type != null)
            {
                childAttributes.Add(new XAttribute("type", priceToFormat.Type.Value.ToString().ToLowerInvariant()));
            }

            if (!string.IsNullOrEmpty(priceToFormat.Info))
            {
                childAttributes.Add(new XAttribute("info", priceToFormat.Info));
            }

            if (priceToFormat.Price != null)
            {
                childAttributes.Add(new XAttribute("price", priceToFormat.Price.Value.ToString("0.####", CultureInfo.InvariantCulture)));
            }

            if (!string.IsNullOrEmpty(priceToFormat.Currency))
            {
                childAttributes.Add(new XAttribute("currency", priceToFormat.Currency));
            }

            if (!childAttributes.Any())
                return false;

            priceElement = new XElement(_media + "price", childAttributes);

            return true;
        }

        private static bool TryFormatMediaRssStatus(MediaRssStatus statusToFormat, out XElement statusElement)
        {
            statusElement = default;

            if (statusToFormat == null)
                return false;

            var childObjects = new List<object>();

            if (statusToFormat.State != null)
            {
                childObjects.Add(new XAttribute("state", statusToFormat.State.Value.ToString().ToLowerInvariant()));
            }

            if (!string.IsNullOrEmpty(statusToFormat.Reason))
            {
                childObjects.Add(new XAttribute("reason", statusToFormat.Reason));
            }

            if (!childObjects.Any())
                return false;

            statusElement = new XElement(_media + "status", childObjects);
            return true;
        }

        private static bool TryFormatMediaRssEmbed(MediaRssEmbed embedToFormat, out XElement embedElement)
        {
            embedElement = default;

            if (string.IsNullOrEmpty(embedToFormat?.Url))
                return false;

            embedElement = new XElement(_media + "embed", new XAttribute("url", embedToFormat.Url));

            if (embedToFormat.Height != null)
            {
                embedElement.Add(new XAttribute("height", embedToFormat.Height.Value.ToString(CultureInfo.InvariantCulture)));
            }

            if (embedToFormat.Width != null)
            {
                embedElement.Add(new XAttribute("width", embedToFormat.Width.Value.ToString(CultureInfo.InvariantCulture)));
            }

            foreach (var paramToFormat in embedToFormat.Params ?? Enumerable.Empty<MediaRssEmbedParam>())
            {
                if (string.IsNullOrEmpty(paramToFormat?.Name) && string.IsNullOrEmpty(paramToFormat?.Value))
                    continue;

                var paramElement = new XElement(_media + "param", paramToFormat.Value ?? string.Empty);
                paramElement.Add(new XAttribute("name", paramToFormat.Name ?? string.Empty));

                embedElement.Add(paramElement);
            }

            return true;
        }

        private static bool TryFormatMediaRssStringCollectionElements(IList<string> stringsToFormat, string collectionElementName, string itemElementName, out XElement collectionElement)
        {
            collectionElement = default;

            if (stringsToFormat == null)
                return false;

            if (stringsToFormat.All(string.IsNullOrEmpty))
                return false;

            collectionElement = new XElement(_media + collectionElementName);

            foreach (var stringToFormat in stringsToFormat)
            {
                if (string.IsNullOrEmpty(stringToFormat))
                    continue;

                collectionElement.Add(new XElement(_media + itemElementName, stringToFormat));
            }

            return true;
        }

        private static bool TryFormatMediaRssCommunity(MediaRssCommunity communityToFormat, out XElement communityElement)
        {
            communityElement = default;

            if (communityToFormat == null)
                return false;

            if (TryFormatMediaRssCommunityStarRating(communityToFormat.StarRating, out var starRatingElement))
            {
                // ReSharper disable once ConstantNullCoalescingCondition
                communityElement = communityElement ?? new XElement(_media + "community");
                communityElement.Add(starRatingElement);
            }

            if (TryFormatMediaRssCommunityStatistics(communityToFormat.Statistics, out var statisticsElement))
            {
                communityElement = communityElement ?? new XElement(_media + "community");
                communityElement.Add(statisticsElement);
            }

            if (TryFormatMediaRssCommunityTags(communityToFormat.Tags, out var tagsElement))
            {
                communityElement = communityElement ?? new XElement(_media + "community");
                communityElement.Add(tagsElement);
            }

            if (communityElement == null)
                return false;

            return true;
        }

        private static bool TryFormatMediaRssCommunityTags(IList<MediaRssCommunityTag> tagsToFormat, out XElement tagsElement)
        {
            tagsElement = default;

            if (tagsToFormat == null)
                return false;

            var tagsValueFormatted = string.Join(", ", tagsToFormat
                .Where(x => !string.IsNullOrEmpty(x?.Tag))
                // ReSharper disable once CompareOfFloatsByEqualityOperator
                .Select(x => x.Weight == null || x.Weight == 1
                    ? x.Tag.Trim()
                    : x.Tag.Trim() + ":" + x.Weight.Value.ToString("0.####", CultureInfo.InvariantCulture)));

            if (string.IsNullOrEmpty(tagsValueFormatted))
                return false;

            tagsElement = new XElement(_media + "tags", tagsValueFormatted);
            return true;
        }

        private static bool TryFormatMediaRssCommunityStatistics(MediaRssCommunityStatistics statisticsToFormat, out XElement statisticsElement)
        {
            statisticsElement = default;

            if (statisticsToFormat == null)
                return false;

            var childAttributes = new List<XAttribute>();

            if (statisticsToFormat.Views != null)
            {
                childAttributes.Add(new XAttribute("views", statisticsToFormat.Views.Value.ToString(CultureInfo.InvariantCulture)));
            }

            if (statisticsToFormat.Favorites != null)
            {
                childAttributes.Add(new XAttribute("favorites", statisticsToFormat.Favorites.Value.ToString(CultureInfo.InvariantCulture)));
            }

            if (!childAttributes.Any())
                return false;

            statisticsElement = new XElement(_media + "statistics", childAttributes);

            return true;
        }

        private static bool TryFormatMediaRssCommunityStarRating(MediaRssCommunityStarRating starRatingToFormat, out XElement starRatingElement)
        {
            starRatingElement = default;

            if (starRatingToFormat == null)
                return false;

            var childAttributes = new List<XAttribute>();

            if (starRatingToFormat.Average != null)
            {
                childAttributes.Add(new XAttribute("average", starRatingToFormat.Average.Value.ToString("0.####", CultureInfo.InvariantCulture)));
            }

            if (starRatingToFormat.Count != null)
            {
                childAttributes.Add(new XAttribute("count", starRatingToFormat.Count.Value.ToString(CultureInfo.InvariantCulture)));
            }

            if (starRatingToFormat.Min != null)
            {
                childAttributes.Add(new XAttribute("min", starRatingToFormat.Min.Value.ToString("0.####", CultureInfo.InvariantCulture)));
            }

            if (starRatingToFormat.Max != null)
            {
                childAttributes.Add(new XAttribute("max", starRatingToFormat.Max.Value.ToString("0.####", CultureInfo.InvariantCulture)));
            }

            if (!childAttributes.Any())
                return false;

            starRatingElement = new XElement(_media + "starRating", childAttributes);

            return true;
        }

        private static bool TryFormatMediaRssRestriction(MediaRssRestriction restrictionToFormat, out XElement restrictionElement)
        {
            restrictionElement = default;

            if (restrictionToFormat == null)
                return false;
            
            restrictionElement = new XElement(_media + "restriction");

            var valuesFormatted = string.Join(" ", restrictionToFormat.Values
                .Where(x => !string.IsNullOrEmpty(x))
                .Select(x => x.Trim()));

            if (!string.IsNullOrEmpty(valuesFormatted))
            {
                restrictionElement.Add(valuesFormatted);
            }

            if (restrictionToFormat.Relationship != null)
            {
                restrictionElement.Add(new XAttribute("relationship", restrictionToFormat.Relationship.Value.ToString().ToLowerInvariant()));
            }

            if (restrictionToFormat.Type != null)
            {
                restrictionElement.Add(new XAttribute("type", restrictionToFormat.Type.Value.ToString().ToLowerInvariant()));
            }

            return true;
        }

        private static bool TryFormatMediaRssText(MediaRssText textToFormat, out XElement textElement)
        {
            textElement = default;

            if (string.IsNullOrEmpty(textToFormat?.Value))
                return false;

            if (!TryFormatMediaRssTypedText(textToFormat, "text", out textElement))
                return false;

            if (!string.IsNullOrEmpty(textToFormat.Lang))
            {
                textElement.Add(new XAttribute("lang", textToFormat.Lang));
            }

            if (Rfc2326NptTimeSpanFormatter.TryFormatTimeAsString(textToFormat.Start, out var startFormatted))
            {
                textElement.Add(new XAttribute("start", startFormatted));
            }

            if (Rfc2326NptTimeSpanFormatter.TryFormatTimeAsString(textToFormat.End, out var endFormatted))
            {
                textElement.Add(new XAttribute("end", endFormatted));
            }

            return true;
        }

        private static bool TryFormatMediaRssCopyright(MediaRssCopyright copyrightToFormat, out XElement copyrightElement)
        {
            copyrightElement = default;

            if (string.IsNullOrEmpty(copyrightToFormat?.Value))
                return false;

            copyrightElement = new XElement(_media + "copyright", copyrightToFormat.Value);

            if (!string.IsNullOrEmpty(copyrightToFormat.Url))
            {
                copyrightElement.Add(new XAttribute("url", copyrightToFormat.Url));
            }

            return true;
        }

        private static bool TryFormatMediaRssCredit(MediaRssCredit creditToFormat, out XElement creditElement)
        {
            creditElement = default;

            if (string.IsNullOrEmpty(creditToFormat?.Value))
                return false;

            creditElement = new XElement(_media + "credit", creditToFormat.Value);

            if (!string.IsNullOrEmpty(creditToFormat.Role))
            {
                creditElement.Add(new XAttribute("role", creditToFormat.Role));
            }

            if (!string.IsNullOrEmpty(creditToFormat.Scheme) && creditToFormat.Scheme != "urn:ebu")
            {
                creditElement.Add(new XAttribute("scheme", creditToFormat.Scheme));
            }

            return true;
        }

        private static bool TryFormatMediaRssPlayer(MediaRssPlayer playerToFormat, out XElement playerElement)
        {
            playerElement = default;

            if (string.IsNullOrEmpty(playerToFormat?.Url))
                return false;

            playerElement = new XElement(_media + "player", new XAttribute("url", playerToFormat.Url));

            if (playerToFormat.Height != null)
            {
                playerElement.Add(new XAttribute("height", playerToFormat.Height.Value.ToString(CultureInfo.InvariantCulture)));
            }

            if (playerToFormat.Width != null)
            {
                playerElement.Add(new XAttribute("width", playerToFormat.Width.Value.ToString(CultureInfo.InvariantCulture)));
            }

            return true;
        }

        private static bool TryFormatMediaRssHash(MediaRssHash hashToFormat, out XElement hashElement)
        {
            hashElement = default;

            if (string.IsNullOrEmpty(hashToFormat?.Value))
                return false;

            hashElement = new XElement(_media + "hash", hashToFormat.Value);

            if (hashToFormat.Algo != MediaRssHashAlgo.Md5)
            {
                hashElement.Add(new XAttribute("algo", hashToFormat.Algo.ToString().ToLowerInvariant()));
            }

            return true;
        }

        private static bool TryFormatMediaRssCategory(MediaRssCategory categoryToFormat, out XElement categoryElement)
        {
            categoryElement = default;

            if (string.IsNullOrEmpty(categoryToFormat?.Value))
                return false;

            categoryElement = new XElement(_media + "category", categoryToFormat.Value);

            if (!string.IsNullOrEmpty(categoryToFormat.Label))
            {
                categoryElement.Add(new XAttribute("label", categoryToFormat.Label));
            }

            if (!string.IsNullOrEmpty(categoryToFormat.Scheme) && categoryToFormat.Scheme != "http://search.yahoo.com/mrss/category_schema")
            {
                categoryElement.Add(new XAttribute("scheme", categoryToFormat.Scheme));
            }

            return true;
        }

        private static bool TryFormatMediaRssThumbnail(MediaRssThumbnail thumbnailToFormat, out XElement thumbnailElement)
        {
            thumbnailElement = default;

            if (string.IsNullOrEmpty(thumbnailToFormat?.Url))
                return false;

            thumbnailElement = new XElement(_media + "thumbnail", new XAttribute("url", thumbnailToFormat.Url));

            if (thumbnailToFormat.Height != null)
            {
                thumbnailElement.Add(new XAttribute("height", thumbnailToFormat.Height.Value.ToString(CultureInfo.InvariantCulture)));
            }

            if (thumbnailToFormat.Width != null)
            {
                thumbnailElement.Add(new XAttribute("width", thumbnailToFormat.Width.Value.ToString(CultureInfo.InvariantCulture)));
            }

            if (Rfc2326NptTimeSpanFormatter.TryFormatTimeAsString(thumbnailToFormat.Time, out var timeFormatted))
            {
                thumbnailElement.Add(new XAttribute("time", timeFormatted));
            }

            return true;
        }

        private static bool TryFormatMediaRssKeywords(IList<string> keywordsToFormat, out XElement keywordsElement)
        {
            keywordsElement = default;

            var keywordsValue = string.Join(", ", (keywordsToFormat ?? Enumerable.Empty<string>())
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(x => x.Trim()));

            if (string.IsNullOrEmpty(keywordsValue))
                return false;

            keywordsElement = new XElement(_media + "keywords", keywordsValue);

            return true;
        }

        private static bool TryFormatMediaRssTypedText(MediaRssTypedText typedTextToFormat, string elementName, out XElement typedTextElement)
        {
            typedTextElement = default;

            if (string.IsNullOrEmpty(typedTextToFormat?.Value))
                return false;

            typedTextElement = new XElement(_media + elementName, typedTextToFormat.Value);

            if (!string.IsNullOrEmpty(typedTextToFormat.Type) && typedTextToFormat.Type != "plain")
            {
                typedTextElement.Add(new XAttribute("type", typedTextToFormat.Type));
            }

            return true;
        }

        private static bool TryFormatMediaRssRating(MediaRssRating ratingToFormat, out XElement ratingElement)
        {
            ratingElement = default;

            if (string.IsNullOrEmpty(ratingToFormat?.Value))
                return false;

            ratingElement = new XElement(_media + "rating", ratingToFormat.Value);

            if (!string.IsNullOrEmpty(ratingToFormat.Scheme) && ratingToFormat.Scheme != "urn:simple")
            {
                ratingElement.Add(new XAttribute("scheme", ratingToFormat.Scheme));
            }

            return true;
        }
    }
}