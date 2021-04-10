using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using Feedpipes.Extensions.MediaRss.Entities;
using Feedpipes.TimeSpans.Relaxed;

namespace Feedpipes.Extensions.MediaRss
{
    public static class MediaRssExtensionParser
    {
        public static bool TryParseMediaRssExtension(XElement parentElement, ExtensionManifestDirectory extensionManifestDirectory, out MediaRssExtension extension)
        {
            extension = null;

            if (parentElement == null)
                return false;

            foreach (var ns in MediaRssExtensionConstants.RecognizedNamespaces)
            {
                if (parentElement.Name.Namespace == ns)
                    continue; // skip because we'd be processing Media RSS extension in Media RSS element

                // groups
                foreach (var groupElement in parentElement.Elements(ns + "group"))
                {
                    if (TryParseMediaRssGroup(groupElement, ns, extensionManifestDirectory, out var parsedGroup))
                    {
                        extension = extension ?? new MediaRssExtension();
                        extension.Groups.Add(parsedGroup);
                    }
                }

                // contents
                foreach (var contentElement in parentElement.Elements(ns + "content"))
                {
                    if (TryParseMediaRssContent(contentElement, ns, extensionManifestDirectory, out var parsedContent))
                    {
                        extension = extension ?? new MediaRssExtension();
                        extension.Contents.Add(parsedContent);
                    }
                }

                // container
                ParseMediaRssContainer(parentElement, ns, extensionManifestDirectory, ref extension);
            }

            return extension != null;
        }

        private static bool TryParseMediaRssContent(XElement contentElement, XNamespace ns, ExtensionManifestDirectory extensionManifestDirectory, out MediaRssContent parsedContent)
        {
            parsedContent = null;

            if (contentElement == null)
                return false;

            // content fields
            if (TryParseStringAttribute(contentElement.Attribute("url"), out var parsedUrl))
            {
                // ReSharper disable once ConstantNullCoalescingCondition
                parsedContent = parsedContent ?? new MediaRssContent();
                parsedContent.Url = parsedUrl;
            }

            if (TryParseIntegerAttribute(contentElement.Attribute("fileSize"), out var parsedFileSize))
            {
                parsedContent = parsedContent ?? new MediaRssContent();
                parsedContent.FileSize = parsedFileSize;
            }

            if (TryParseStringAttribute(contentElement.Attribute("type"), out var parsedType))
            {
                parsedContent = parsedContent ?? new MediaRssContent();
                parsedContent.Type = parsedType;
            }

            if (TryParseEnumAttribute<MediaRssMedium>(contentElement.Attribute("medium"), out var parsedMedium))
            {
                parsedContent = parsedContent ?? new MediaRssContent();
                parsedContent.Medium = parsedMedium;
            }

            if (TryParseBoolAttribute(contentElement.Attribute("isDefault"), out var parsedIsDefault))
            {
                parsedContent = parsedContent ?? new MediaRssContent();
                parsedContent.IsDefault = parsedIsDefault;
            }

            if (TryParseEnumAttribute<MediaRssExpression>(contentElement.Attribute("expression"), out var parsedExpression))
            {
                parsedContent = parsedContent ?? new MediaRssContent();
                parsedContent.Expression = parsedExpression;
            }

            if (TryParseDoubleAttribute(contentElement.Attribute("bitrate"), out var parsedBitRate))
            {
                parsedContent = parsedContent ?? new MediaRssContent();
                parsedContent.BitRate = parsedBitRate;
            }

            if (TryParseDoubleAttribute(contentElement.Attribute("framerate"), out var parsedFrameRate))
            {
                parsedContent = parsedContent ?? new MediaRssContent();
                parsedContent.FrameRate = parsedFrameRate;
            }

            if (TryParseDoubleAttribute(contentElement.Attribute("samplingrate"), out var parsedSamplingRate))
            {
                parsedContent = parsedContent ?? new MediaRssContent();
                parsedContent.SamplingRate = parsedSamplingRate;
            }

            if (TryParseIntegerAttribute(contentElement.Attribute("channels"), out var parsedChannels))
            {
                parsedContent = parsedContent ?? new MediaRssContent();
                parsedContent.Channels = parsedChannels;
            }

            if (TryParseTimeSpanAttribute(contentElement.Attribute("duration"), out var parsedDuration))
            {
                parsedContent = parsedContent ?? new MediaRssContent();
                parsedContent.Duration = parsedDuration;
            }

            if (TryParseIntegerAttribute(contentElement.Attribute("height"), out var parsedHeight))
            {
                parsedContent = parsedContent ?? new MediaRssContent();
                parsedContent.Height = parsedHeight;
            }

            if (TryParseIntegerAttribute(contentElement.Attribute("width"), out var parsedWidth))
            {
                parsedContent = parsedContent ?? new MediaRssContent();
                parsedContent.Width = parsedWidth;
            }

            if (TryParseStringAttribute(contentElement.Attribute("lang"), out var parsedLang))
            {
                parsedContent = parsedContent ?? new MediaRssContent();
                parsedContent.Lang = parsedLang;
            }

            // container
            ParseMediaRssContainer(contentElement, ns, extensionManifestDirectory, ref parsedContent);

            if (parsedContent == null)
                return false;

            // extensions
            ExtensibleEntityParser.ParseXElementExtensions(contentElement, extensionManifestDirectory, parsedContent);

            return true;
        }

        private static bool TryParseStringAttribute(XAttribute attribute, out string parsedValue)
        {
            parsedValue = default;

            if (string.IsNullOrEmpty(attribute?.Value))
                return false;

            parsedValue = attribute.Value;
            return true;
        }

        private static bool TryParseStringElement(XElement element, out string parsedValue)
        {
            parsedValue = default;

            if (string.IsNullOrEmpty(element?.Value))
                return false;

            parsedValue = element.Value.Trim();
            return true;
        }

        private static bool TryParseEnumAttribute<TEnum>(XAttribute attribute, out TEnum parsedValue)
            where TEnum : struct, Enum
        {
            parsedValue = default;

            if (string.IsNullOrEmpty(attribute?.Value))
                return false;

            var stringValue = attribute.Value.Trim();
            if (!Enum.TryParse(stringValue, ignoreCase: true, out parsedValue))
                return false;

            return true;
        }

        private static bool TryParseIntegerAttribute(XAttribute attribute, out int parsedValue)
        {
            parsedValue = default;

            if (string.IsNullOrEmpty(attribute?.Value))
                return false;

            var stringValue = attribute.Value.Trim();
            if (int.TryParse(stringValue, NumberStyles.Any, CultureInfo.InvariantCulture, out parsedValue))
                return true;

            if (double.TryParse(stringValue, NumberStyles.Any, CultureInfo.InvariantCulture, out var parsedDouble))
            {
                parsedValue = (int)parsedDouble;
                return true;
            }

            return false;
        }

        private static bool TryParseDoubleAttribute(XAttribute attribute, out double parsedValue)
        {
            parsedValue = default;

            if (string.IsNullOrEmpty(attribute?.Value))
                return false;

            var stringValue = attribute.Value.Trim();

            if (!double.TryParse(stringValue, NumberStyles.Any, CultureInfo.InvariantCulture, out parsedValue))
                return false;

            return true;
        }

        private static bool TryParseDecimalAttribute(XAttribute attribute, out decimal parsedValue)
        {
            parsedValue = default;

            if (string.IsNullOrEmpty(attribute?.Value))
                return false;

            var stringValue = attribute.Value.Trim();

            if (!decimal.TryParse(stringValue, NumberStyles.Any, CultureInfo.InvariantCulture, out parsedValue))
                return false;

            return true;
        }

        private static bool TryParseBoolAttribute(XAttribute attribute, out bool parsedValue)
        {
            parsedValue = default;

            if (string.IsNullOrEmpty(attribute?.Value))
                return false;

            var stringValue = attribute.Value.Trim().ToLowerInvariant();
            switch (stringValue)
            {
                case "true":
                    parsedValue = true;
                    return true;
                case "false":
                    parsedValue = false;
                    return true;
                default:
                    return false;
            }
        }

        private static bool TryParseTimeSpanAttribute(XAttribute attribute, out TimeSpan parsedValue)
        {
            parsedValue = default;

            if (string.IsNullOrEmpty(attribute?.Value))
                return false;

            var stringValue = attribute.Value.Trim();
            if (!RelaxedTimeSpanParser.TryParseTimeFromStringSecondsFirst(stringValue, out parsedValue))
                return false;

            return true;
        }

        private static bool TryParseTimeSpanElement(XElement element, out TimeSpan parsedValue)
        {
            parsedValue = default;

            if (string.IsNullOrEmpty(element?.Value))
                return false;

            var stringValue = element.Value.Trim();
            if (!RelaxedTimeSpanParser.TryParseTimeFromStringSecondsFirst(stringValue, out parsedValue))
                return false;

            return true;
        }

        private static bool TryParseMediaRssGroup(XElement groupElement, XNamespace ns, ExtensionManifestDirectory extensionManifestDirectory, out MediaRssGroup parsedGroup)
        {
            parsedGroup = null;

            if (groupElement == null)
                return false;

            // contents
            foreach (var contentElement in groupElement.Elements(ns + "content"))
            {
                if (TryParseMediaRssContent(contentElement, ns, extensionManifestDirectory, out var parsedContent))
                {
                    parsedGroup = parsedGroup ?? new MediaRssGroup();
                    parsedGroup.Contents.Add(parsedContent);
                }
            }

            // container
            ParseMediaRssContainer(groupElement, ns, extensionManifestDirectory, ref parsedGroup);

            if (parsedGroup == null)
                return false;

            // extensions
            ExtensibleEntityParser.ParseXElementExtensions(groupElement, extensionManifestDirectory, parsedGroup);

            return true;
        }

        private static void ParseMediaRssContainer<TContainer>(XElement parentElement, XNamespace ns, ExtensionManifestDirectory extensionManifestDirectory, ref TContainer parsedContainer)
            where TContainer : MediaRssContainer, new()
        {
            if (parentElement == null)
                return;

            // optional elements

            foreach (var ratingElement in parentElement.Elements(ns + "rating"))
            {
                if (TryParseMediaRssRating(ratingElement, out var parsedRating))
                {
                    parsedContainer = parsedContainer ?? new TContainer();
                    parsedContainer.Ratings.Add(parsedRating);
                }
            }

            if (TryParseMediaRssTypedText<MediaRssTypedText>(parentElement.Element(ns + "title"), out var parsedTitle))
            {
                parsedContainer = parsedContainer ?? new TContainer();
                parsedContainer.Title = parsedTitle;
            }

            if (TryParseMediaRssTypedText<MediaRssTypedText>(parentElement.Element(ns + "description"), out var parsedDescription))
            {
                parsedContainer = parsedContainer ?? new TContainer();
                parsedContainer.Description = parsedDescription;
            }

            if (TryParseMediaRssKeywords(parentElement.Element(ns + "keywords"), out var parsedKeywords))
            {
                parsedContainer = parsedContainer ?? new TContainer();
                parsedContainer.Keywords = parsedKeywords;
            }

            foreach (var thumbnailElement in parentElement.Elements(ns + "thumbnail"))
            {
                if (TryParseMediaRssThumbnail(thumbnailElement, out var parsedThumbnail))
                {
                    parsedContainer = parsedContainer ?? new TContainer();
                    parsedContainer.Thumbnails.Add(parsedThumbnail);
                }
            }

            foreach (var categoryElement in parentElement.Elements(ns + "category"))
            {
                if (TryParseMediaRssCategory(categoryElement, out var parsedCategory))
                {
                    parsedContainer = parsedContainer ?? new TContainer();
                    parsedContainer.Categories.Add(parsedCategory);
                }
            }

            foreach (var hashElement in parentElement.Elements(ns + "hash"))
            {
                if (TryParseMediaRssHash(hashElement, out var parsedHash))
                {
                    parsedContainer = parsedContainer ?? new TContainer();
                    parsedContainer.Hashes.Add(parsedHash);
                }
            }

            if (TryParseMediaRssPlayer(parentElement.Element(ns + "player"), out var parsedPlayer))
            {
                parsedContainer = parsedContainer ?? new TContainer();
                parsedContainer.Player = parsedPlayer;
            }

            foreach (var creditElement in parentElement.Elements(ns + "credit"))
            {
                if (TryParseMediaRssCredit(creditElement, out var parsedCredit))
                {
                    parsedContainer = parsedContainer ?? new TContainer();
                    parsedContainer.Credits.Add(parsedCredit);
                }
            }

            if (TryParseMediaRssCopyright(parentElement.Element(ns + "copyright"), out var parsedCopyright))
            {
                parsedContainer = parsedContainer ?? new TContainer();
                parsedContainer.Copyright = parsedCopyright;
            }

            foreach (var textElement in parentElement.Elements(ns + "text"))
            {
                if (TryParseMediaRssText(textElement, out var parsedText))
                {
                    parsedContainer = parsedContainer ?? new TContainer();
                    parsedContainer.Texts.Add(parsedText);
                }
            }

            foreach (var restrictionElement in parentElement.Elements(ns + "restriction"))
            {
                if (TryParseMediaRssRestriction(restrictionElement, out var parsedRestriction))
                {
                    parsedContainer = parsedContainer ?? new TContainer();
                    parsedContainer.Restrictions.Add(parsedRestriction);
                }
            }

            if (TryParseMediaRssCommunity(parentElement.Element(ns + "community"), ns, out var parsedCommunity))
            {
                parsedContainer = parsedContainer ?? new TContainer();
                parsedContainer.Community = parsedCommunity;
            }

            if (TryParseMediaRssStringCollectionElements(parentElement, ns, "comments", "comment", out var parsedComments))
            {
                parsedContainer = parsedContainer ?? new TContainer();
                parsedContainer.Comments = parsedComments;
            }

            foreach (var embedElement in parentElement.Elements(ns + "embed"))
            {
                if (TryParseMediaRssEmbed(embedElement, ns, out var parsedEmbed))
                {
                    parsedContainer = parsedContainer ?? new TContainer();
                    parsedContainer.Embeds.Add(parsedEmbed);
                }
            }

            if (TryParseMediaRssStringCollectionElements(parentElement, ns, "responses", "response", out var parsedResponses))
            {
                parsedContainer = parsedContainer ?? new TContainer();
                parsedContainer.Responses = parsedResponses;
            }

            if (TryParseMediaRssStringCollectionElements(parentElement, ns, "backLinks", "backLink", out var parsedBackLinks))
            {
                parsedContainer = parsedContainer ?? new TContainer();
                parsedContainer.BackLinks = parsedBackLinks;
            }

            if (TryParseMediaRssStatus(parentElement.Element(ns + "status"), out var parsedStatus))
            {
                parsedContainer = parsedContainer ?? new TContainer();
                parsedContainer.Status = parsedStatus;
            }

            foreach (var priceElement in parentElement.Elements(ns + "price"))
            {
                if (TryParseMediaRssPrice(priceElement, out var parsedPrice))
                {
                    parsedContainer = parsedContainer ?? new TContainer();
                    parsedContainer.Prices.Add(parsedPrice);
                }
            }

            if (TryParseMediaRssLink(parentElement.Element(ns + "license"), out var parsedLicense))
            {
                parsedContainer = parsedContainer ?? new TContainer();
                parsedContainer.License = parsedLicense;
            }

            foreach (var subTitleElement in parentElement.Elements(ns + "subTitle"))
            {
                if (TryParseMediaRssLink(subTitleElement, out var parsedSubTitle))
                {
                    parsedContainer = parsedContainer ?? new TContainer();
                    parsedContainer.SubTitles.Add(parsedSubTitle);
                }
            }

            foreach (var peerLinkElement in parentElement.Elements(ns + "peerLink"))
            {
                if (TryParseMediaRssLink(peerLinkElement, out var parsedPeerLink))
                {
                    parsedContainer = parsedContainer ?? new TContainer();
                    parsedContainer.PeerLinks.Add(parsedPeerLink);
                }
            }

            foreach (var locationElement in parentElement.Elements(ns + "location"))
            {
                if (TryParseMediaRssLocation(locationElement, extensionManifestDirectory, out var parsedLocation))
                {
                    parsedContainer = parsedContainer ?? new TContainer();
                    parsedContainer.Locations.Add(parsedLocation);
                }
            }

            if (TryParseMediaRssRights(parentElement.Element(ns + "rights"), out var parsedRights))
            {
                parsedContainer = parsedContainer ?? new TContainer();
                parsedContainer.Rights = parsedRights;
            }

            if (TryParseMediaRssScenes(parentElement.Element(ns + "scenes"), ns, out var parsedScenes))
            {
                parsedContainer = parsedContainer ?? new TContainer();
                parsedContainer.Scenes = parsedScenes;
            }
        }

        private static bool TryParseMediaRssScenes(XElement scenesElement, XNamespace ns, out IList<MediaRssScene> parsedScenes)
        {
            parsedScenes = new List<MediaRssScene>();

            var sceneElements = scenesElement?.Elements(ns + "scene") ?? Enumerable.Empty<XElement>();

            foreach (var sceneElement in sceneElements)
            {
                if (TryParseMediaRssScene(sceneElement, ns, out var parsedScene))
                {
                    parsedScenes.Add(parsedScene);
                }
            }

            if (!parsedScenes.Any())
                return false;

            return true;
        }

        private static bool TryParseMediaRssScene(XElement sceneElement, XNamespace ns, out MediaRssScene parsedScene)
        {
            parsedScene = default;

            if (TryParseStringElement(sceneElement?.Element(ns + "sceneTitle") ?? sceneElement?.Element("sceneTitle"), out var parsedTitle))
            {
                parsedScene = parsedScene ?? new MediaRssScene();
                parsedScene.Title = parsedTitle;
            }

            if (TryParseStringElement(sceneElement?.Element(ns + "sceneDescription") ?? sceneElement?.Element("sceneDescription"), out var parsedDescription))
            {
                parsedScene = parsedScene ?? new MediaRssScene();
                parsedScene.Description = parsedDescription;
            }

            if (TryParseTimeSpanElement(sceneElement?.Element(ns + "sceneStartTime") ?? sceneElement?.Element("sceneStartTime"), out var parsedStartTime))
            {
                parsedScene = parsedScene ?? new MediaRssScene();
                parsedScene.StartTime = parsedStartTime;
            }

            if (TryParseTimeSpanElement(sceneElement?.Element(ns + "sceneEndTime") ?? sceneElement?.Element("sceneEndTime"), out var parsedEndTime))
            {
                parsedScene = parsedScene ?? new MediaRssScene();
                parsedScene.EndTime = parsedEndTime;
            }

            if (parsedScene == null)
                return false;

            return true;
        }

        private static bool TryParseMediaRssRights(XElement rightsElement, out MediaRssRightsStatus parsedRights)
        {
            parsedRights = default;

            if (!TryParseEnumAttribute(rightsElement?.Attribute("status"), out parsedRights))
                return false;

            return true;
        }

        private static bool TryParseMediaRssLocation(XElement locationElement, ExtensionManifestDirectory extensionManifestDirectory, out MediaRssLocation parsedLocation)
        {
            parsedLocation = default;

            if (locationElement == null)
                return false;

            if (TryParseTimeSpanAttribute(locationElement.Attribute("start"), out var parsedStart))
            {
                // ReSharper disable once ConstantNullCoalescingCondition
                parsedLocation = parsedLocation ?? new MediaRssLocation();
                parsedLocation.Start = parsedStart;
            }

            if (TryParseTimeSpanAttribute(locationElement.Attribute("end"), out var parsedEnd))
            {
                parsedLocation = parsedLocation ?? new MediaRssLocation();
                parsedLocation.End = parsedEnd;
            }

            if (TryParseStringAttribute(locationElement.Attribute("description"), out var parsedDescription))
            {
                parsedLocation = parsedLocation ?? new MediaRssLocation();
                parsedLocation.Description = parsedDescription;
            }

            var emptyBeforeExtensions = parsedLocation == null;

            // extensions
            parsedLocation = parsedLocation ?? new MediaRssLocation();
            ExtensibleEntityParser.ParseXElementExtensions(locationElement, extensionManifestDirectory, parsedLocation);

            if (emptyBeforeExtensions && !parsedLocation.Extensions.Any())
                return false;

            return true;
        }

        private static bool TryParseMediaRssLink(XElement linkElement, out MediaRssLink parsedLink)
        {
            parsedLink = default;

            if (!TryParseStringAttribute(linkElement?.Attribute("href"), out var parsedHref))
                return false;

            parsedLink = new MediaRssLink
            {
                Href = parsedHref,
            };

            if (TryParseStringAttribute(linkElement?.Attribute("type"), out var parsedType))
            {
                parsedLink.Type = parsedType;
            }

            if (TryParseStringAttribute(linkElement?.Attribute("lang"), out var parsedLang))
            {
                parsedLink.Lang = parsedLang;
            }

            return true;
        }

        private static bool TryParseMediaRssPrice(XElement priceElement, out MediaRssPrice parsedPrice)
        {
            parsedPrice = default;

            if (TryParseEnumAttribute<MediaRssPriceType>(priceElement?.Attribute("type"), out var parsedType))
            {
                parsedPrice = parsedPrice ?? new MediaRssPrice();
                parsedPrice.Type = parsedType;
            }

            if (TryParseStringAttribute(priceElement?.Attribute("info"), out var parsedInfo))
            {
                parsedPrice = parsedPrice ?? new MediaRssPrice();
                parsedPrice.Info = parsedInfo;
            }

            if (TryParseDecimalAttribute(priceElement?.Attribute("price"), out var parsedPriceValue))
            {
                parsedPrice = parsedPrice ?? new MediaRssPrice();
                parsedPrice.Price = parsedPriceValue;
            }

            if (TryParseStringAttribute(priceElement?.Attribute("currency"), out var parsedCurrency))
            {
                parsedPrice = parsedPrice ?? new MediaRssPrice();
                parsedPrice.Currency = parsedCurrency;
            }

            if (parsedPrice == null)
                return false;

            return true;
        }

        private static bool TryParseMediaRssStatus(XElement statusElement, out MediaRssStatus parsedStatus)
        {
            parsedStatus = default;

            if (TryParseEnumAttribute<MediaRssStatusState>(statusElement?.Attribute("state"), out var parsedState))
            {
                parsedStatus = parsedStatus ?? new MediaRssStatus();
                parsedStatus.State = parsedState;
            }

            if (TryParseStringAttribute(statusElement?.Attribute("reason"), out var parsedReason))
            {
                parsedStatus = parsedStatus ?? new MediaRssStatus();
                parsedStatus.Reason = parsedReason;
            }

            if (parsedStatus == null)
                return false;

            return true;
        }

        private static bool TryParseMediaRssEmbed(XElement embedElement, XNamespace ns, out MediaRssEmbed parsedEmbed)
        {
            parsedEmbed = default;

            if (!TryParseStringAttribute(embedElement?.Attribute("url"), out var parsedUrl))
                return false;

            parsedEmbed = new MediaRssEmbed
            {
                Url = parsedUrl,
            };

            if (TryParseIntegerAttribute(embedElement?.Attribute("height"), out var parsedHeight))
            {
                parsedEmbed.Height = parsedHeight;
            }

            if (TryParseIntegerAttribute(embedElement?.Attribute("width"), out var parsedWidth))
            {
                parsedEmbed.Width = parsedWidth;
            }

            parsedEmbed.Params = (embedElement?.Elements(ns + "param") ?? Enumerable.Empty<XElement>())
                .Select(x => new MediaRssEmbedParam
                {
                    Value = x?.Value,
                    Name = x?.Attribute("name")?.Value,
                })
                .Where(x => !string.IsNullOrEmpty(x.Name) || !string.IsNullOrEmpty(x.Value))
                .ToList();

            return true;
        }

        private static bool TryParseMediaRssStringCollectionElements(XElement parentElement, XNamespace ns, string collectionElementName, string itemElementName, out IList<string> parsedStrings)
        {
            parsedStrings = (parentElement
                    ?.Element(ns + collectionElementName)
                    ?.Elements(ns + itemElementName) ?? Enumerable.Empty<XElement>())
                .Where(x => !string.IsNullOrWhiteSpace(x?.Value))
                .Select(x => x.Value.Trim())
                .ToList();

            if (!parsedStrings.Any())
                return false;

            return true;
        }

        private static bool TryParseMediaRssCommunity(XElement communityElement, XNamespace ns, out MediaRssCommunity parsedCommunity)
        {
            parsedCommunity = default;

            if (communityElement == null)
                return false;

            if (TryParseMediaRssCommunityStarRating(communityElement.Element(ns + "starRating"), out var parsedStarRating))
            {
                // ReSharper disable once ConstantNullCoalescingCondition
                parsedCommunity = parsedCommunity ?? new MediaRssCommunity();
                parsedCommunity.StarRating = parsedStarRating;
            }

            if (TryParseMediaRssCommunityStatistics(communityElement.Element(ns + "statistics"), out var parsedStatistics))
            {
                parsedCommunity = parsedCommunity ?? new MediaRssCommunity();
                parsedCommunity.Statistics = parsedStatistics;
            }

            if (TryParseMediaRssCommunityTags(communityElement.Element(ns + "tags"), out var parsedTags))
            {
                parsedCommunity = parsedCommunity ?? new MediaRssCommunity();
                parsedCommunity.Tags = parsedTags;
            }

            return true;
        }

        private static bool TryParseMediaRssCommunityTags(XElement tagsElement, out IList<MediaRssCommunityTag> parsedTags)
        {
            parsedTags = default;

            if (string.IsNullOrEmpty(tagsElement?.Value))
                return false;

            var tagPieces = tagsElement.Value
                .Trim()
                .Split(',')
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(x => x.Trim());

            parsedTags = new List<MediaRssCommunityTag>();

            foreach (var tagPiece in tagPieces)
            {
                var tagParts = tagPiece.Split(':');
                if (tagParts.Length == 2)
                {
                    var tagString = tagParts[0].Trim();
                    if (string.IsNullOrEmpty(tagString))
                        continue;

                    var weightString = tagParts[1].Trim();
                    if (string.IsNullOrEmpty(weightString))
                        continue;

                    if (double.TryParse(weightString, NumberStyles.Any, CultureInfo.InvariantCulture, out var parsedWeight))
                    {
                        parsedTags.Add(new MediaRssCommunityTag { Tag = tagString, Weight = parsedWeight });
                        continue;
                    }
                }

                parsedTags.Add(new MediaRssCommunityTag { Tag = tagPiece });
            }

            if (!parsedTags.Any())
                return false;

            return true;
        }

        private static bool TryParseMediaRssCommunityStatistics(XElement statisticsElement, out MediaRssCommunityStatistics parsedStatistics)
        {
            parsedStatistics = default;

            if (TryParseIntegerAttribute(statisticsElement?.Attribute("views"), out var parsedViews))
            {
                parsedStatistics = parsedStatistics ?? new MediaRssCommunityStatistics();
                parsedStatistics.Views = parsedViews;
            }

            if (TryParseIntegerAttribute(statisticsElement?.Attribute("favorites"), out var parsedFavorites))
            {
                parsedStatistics = parsedStatistics ?? new MediaRssCommunityStatistics();
                parsedStatistics.Favorites = parsedFavorites;
            }

            if (parsedStatistics == null)
                return false;

            return true;
        }

        private static bool TryParseMediaRssCommunityStarRating(XElement starRatingElement, out MediaRssCommunityStarRating parsedStarRating)
        {
            parsedStarRating = default;

            if (TryParseDoubleAttribute(starRatingElement?.Attribute("average"), out var parsedAverage))
            {
                parsedStarRating = parsedStarRating ?? new MediaRssCommunityStarRating();
                parsedStarRating.Average = parsedAverage;
            }

            if (TryParseIntegerAttribute(starRatingElement?.Attribute("count"), out var parsedCount))
            {
                parsedStarRating = parsedStarRating ?? new MediaRssCommunityStarRating();
                parsedStarRating.Count = parsedCount;
            }

            if (TryParseDoubleAttribute(starRatingElement?.Attribute("min"), out var parsedMin))
            {
                parsedStarRating = parsedStarRating ?? new MediaRssCommunityStarRating();
                parsedStarRating.Min = parsedMin;
            }

            if (TryParseDoubleAttribute(starRatingElement?.Attribute("max"), out var parsedMax))
            {
                parsedStarRating = parsedStarRating ?? new MediaRssCommunityStarRating();
                parsedStarRating.Max = parsedMax;
            }

            if (parsedStarRating == null)
                return false;

            return true;
        }

        private static bool TryParseMediaRssRestriction(XElement restrictionElement, out MediaRssRestriction parsedRestriction)
        {
            parsedRestriction = default;

            if (restrictionElement == null)
                return false;

            var parsedValues = restrictionElement.Value
                .Trim()
                .Split(' ')
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(x => x.Trim())
                .ToList();

            parsedRestriction = new MediaRssRestriction
            {
                Values = parsedValues,
            };

            if (TryParseEnumAttribute<MediaRssRestrictionRelationship>(restrictionElement.Attribute("relationship"), out var parsedRelationship))
            {
                parsedRestriction.Relationship = parsedRelationship;
            }

            if (TryParseEnumAttribute<MediaRssRestrictionType>(restrictionElement.Attribute("type"), out var parsedType))
            {
                parsedRestriction.Type = parsedType;
            }

            return true;
        }

        private static bool TryParseMediaRssText(XElement textElement, out MediaRssText parsedText)
        {
            if (!TryParseMediaRssTypedText(textElement, out parsedText))
                return false;

            if (TryParseStringAttribute(textElement.Attribute("lang"), out var parsedLang))
            {
                parsedText.Lang = parsedLang;
            }

            if (TryParseTimeSpanAttribute(textElement.Attribute("start"), out var parsedStart))
            {
                parsedText.Start = parsedStart;
            }

            if (TryParseTimeSpanAttribute(textElement.Attribute("end"), out var parsedEnd))
            {
                parsedText.End = parsedEnd;
            }

            return true;
        }

        private static bool TryParseMediaRssCopyright(XElement copyrightElement, out MediaRssCopyright parsedCopyright)
        {
            parsedCopyright = default;

            if (string.IsNullOrWhiteSpace(copyrightElement?.Value))
                return false;

            parsedCopyright = new MediaRssCopyright
            {
                Value = copyrightElement.Value.Trim(),
            };

            if (TryParseStringAttribute(copyrightElement.Attribute("url"), out var parsedUrl))
            {
                parsedCopyright.Url = parsedUrl;
            }

            return true;
        }

        private static bool TryParseMediaRssCredit(XElement creditElement, out MediaRssCredit parsedCredit)
        {
            parsedCredit = default;

            if (string.IsNullOrWhiteSpace(creditElement?.Value))
                return false;

            parsedCredit = new MediaRssCredit
            {
                Value = creditElement.Value.Trim(),
            };

            if (TryParseStringAttribute(creditElement.Attribute("role"), out var parsedRole))
            {
                parsedCredit.Role = parsedRole;
            }

            if (TryParseStringAttribute(creditElement.Attribute("scheme"), out var parsedScheme))
            {
                parsedCredit.Scheme = parsedScheme;
            }

            return true;
        }

        private static bool TryParseMediaRssPlayer(XElement playerElement, out MediaRssPlayer parsedPlayer)
        {
            parsedPlayer = default;

            if (!TryParseStringAttribute(playerElement?.Attribute("url"), out var parsedUrl))
                return false;

            parsedPlayer = new MediaRssPlayer
            {
                Url = parsedUrl,
            };

            if (TryParseIntegerAttribute(playerElement?.Attribute("height"), out var parsedHeight))
            {
                parsedPlayer.Height = parsedHeight;
            }

            if (TryParseIntegerAttribute(playerElement?.Attribute("width"), out var parsedWidth))
            {
                parsedPlayer.Width = parsedWidth;
            }

            return true;
        }

        private static bool TryParseMediaRssHash(XElement hashElement, out MediaRssHash parsedHash)
        {
            parsedHash = default;

            if (string.IsNullOrWhiteSpace(hashElement?.Value))
                return false;

            parsedHash = new MediaRssHash
            {
                Value = hashElement.Value.Trim(),
            };

            if (TryParseEnumAttribute<MediaRssHashAlgo>(hashElement.Attribute("algo"), out var parsedAlgo))
            {
                parsedHash.Algo = parsedAlgo;
            }

            return true;
        }

        private static bool TryParseMediaRssCategory(XElement categoryElement, out MediaRssCategory parsedCategory)
        {
            parsedCategory = default;

            if (string.IsNullOrWhiteSpace(categoryElement?.Value))
                return false;

            parsedCategory = new MediaRssCategory
            {
                Value = categoryElement.Value.Trim(),
            };

            if (TryParseStringAttribute(categoryElement.Attribute("label"), out var parsedLabel))
            {
                parsedCategory.Label = parsedLabel;
            }

            if (TryParseStringAttribute(categoryElement.Attribute("scheme"), out var parsedScheme))
            {
                parsedCategory.Scheme = parsedScheme;
            }

            return true;
        }

        private static bool TryParseMediaRssThumbnail(XElement thumbnailElement, out MediaRssThumbnail parsedThumbnail)
        {
            parsedThumbnail = default;

            if (!TryParseStringAttribute(thumbnailElement?.Attribute("url"), out var parsedUrl))
                return false;

            parsedThumbnail = new MediaRssThumbnail
            {
                Url = parsedUrl,
            };

            if (TryParseIntegerAttribute(thumbnailElement?.Attribute("height"), out var parsedHeight))
            {
                parsedThumbnail.Height = parsedHeight;
            }

            if (TryParseIntegerAttribute(thumbnailElement?.Attribute("width"), out var parsedWidth))
            {
                parsedThumbnail.Width = parsedWidth;
            }

            if (TryParseTimeSpanAttribute(thumbnailElement?.Attribute("time"), out var parsedTime))
            {
                parsedThumbnail.Time = parsedTime;
            }

            return true;
        }

        private static bool TryParseMediaRssKeywords(XElement keywordsElement, out IList<string> parsedKeywords)
        {
            parsedKeywords = default;

            if (string.IsNullOrWhiteSpace(keywordsElement?.Value))
                return false;

            parsedKeywords = keywordsElement.Value.Split(',')
                .Select(x => x.Trim())
                .Where(x => !string.IsNullOrEmpty(x))
                .ToList();

            if (!parsedKeywords.Any())
                return false;

            return true;
        }

        private static bool TryParseMediaRssTypedText<TMediaRssTypedText>(XElement typedTextElement, out TMediaRssTypedText parsedTypedText)
            where TMediaRssTypedText : MediaRssTypedText, new()
        {
            parsedTypedText = default;

            if (string.IsNullOrWhiteSpace(typedTextElement?.Value))
                return false;

            parsedTypedText = new TMediaRssTypedText
            {
                Value = typedTextElement.Value.Trim(),
            };

            if (TryParseStringAttribute(typedTextElement.Attribute("type"), out var parsedType))
            {
                parsedTypedText.Type = parsedType;
            }

            return true;
        }

        private static bool TryParseMediaRssRating(XElement ratingElement, out MediaRssRating parsedRating)
        {
            parsedRating = default;

            if (string.IsNullOrWhiteSpace(ratingElement?.Value))
                return false;

            parsedRating = new MediaRssRating
            {
                Value = ratingElement.Value.Trim(),
            };

            if (TryParseStringAttribute(ratingElement.Attribute("scheme"), out var parsedScheme))
            {
                parsedRating.Scheme = parsedScheme;
            }

            return true;
        }
    }
}
