using System;
using Feedpipes.Syndication.Extensions;
using Feedpipes.Syndication.JsonFeedFormat.Entities;
using Feedpipes.Syndication.Timestamps.Relaxed;
using Feedpipes.Syndication.Utils.Json;
using Newtonsoft.Json.Linq;

namespace Feedpipes.Syndication.JsonFeedFormat
{
    public static class JsonFeedParser
    {
        public static bool TryParseJsonFeed(in JObject feedObject, out JsonFeed parsedFeed, ExtensionManifestDirectory extensionManifestDirectory = null)
        {
            parsedFeed = default;

            if (!feedObject.TryGetStringProperty("version", out var parsedVersion))
                return false;

            if (!JsonFeedConstants.RecognizedVersions.Contains(parsedVersion))
                return false;

            if (extensionManifestDirectory == null)
            {
                extensionManifestDirectory = ExtensionManifestDirectory.DefaultForJsonFeed;
            }

            parsedFeed = new JsonFeed();

            if (feedObject.TryGetStringProperty("title", out var parsedTitle))
            {
                parsedFeed.Title = parsedTitle;
            }

            if (feedObject.TryGetStringProperty("home_page_url", out var parsedHomePageUrl))
            {
                parsedFeed.HomePageUrl = parsedHomePageUrl;
            }

            if (feedObject.TryGetStringProperty("feed_url", out var parsedFeedUrl))
            {
                parsedFeed.FeedUrl = parsedFeedUrl;
            }

            if (feedObject.TryGetStringProperty("description", out var parsedDescription))
            {
                parsedFeed.Description = parsedDescription;
            }

            if (feedObject.TryGetStringProperty("user_comment", out var parsedUserComment))
            {
                parsedFeed.UserComment = parsedUserComment;
            }

            if (feedObject.TryGetStringProperty("next_url", out var parsedNextUrl))
            {
                parsedFeed.NextUrl = parsedNextUrl;
            }

            if (feedObject.TryGetStringProperty("icon", out var parsedIcon))
            {
                parsedFeed.Icon = parsedIcon;
            }

            if (feedObject.TryGetStringProperty("favicon", out var parsedFavicon))
            {
                parsedFeed.Favicon = parsedFavicon;
            }

            if (feedObject.TryGetJObjectProperty("author", out var authorObject) && TryParseJsonFeedAuthor(authorObject, out var parsedAuthor))
            {
                parsedFeed.Author = parsedAuthor;
            }

            if (feedObject.TryGetBoolProperty("expired", out var parsedExpired))
            {
                parsedFeed.Expired = parsedExpired;
            }

            if (feedObject.TryGetJObjectArrayProperty("hubs", out var hubObjects))
            {
                foreach (var hubObject in hubObjects)
                {
                    if (TryParseJsonFeedHub(hubObject, out var parsedHub))
                    {
                        parsedFeed.Hubs.Add(parsedHub);
                    }
                }
            }

            if (feedObject.TryGetJObjectArrayProperty("items", out var itemObjects))
            {
                foreach (var itemObject in itemObjects)
                {
                    if (TryParseJsonFeedItemElement(itemObject, extensionManifestDirectory, out var parsedItem))
                    {
                        parsedFeed.Items.Add(parsedItem);
                    }
                }
            }
            
            // extensions
            ExtensibleEntityParser.ParseJObjectExtensions(feedObject, extensionManifestDirectory, parsedFeed);

            return true;
        }

        private static bool TryParseJsonFeedItemElement(in JObject itemObject, ExtensionManifestDirectory extensionManifestDirectory, out JsonFeedItem parsedItem)
        {
            parsedItem = null;

            if (itemObject.TryGetStringProperty("id", out var parsedId))
            {
                // ReSharper disable once ConstantNullCoalescingCondition
                parsedItem = parsedItem ?? new JsonFeedItem();
                parsedItem.Id = parsedId;
            }

            if (itemObject.TryGetStringProperty("url", out var parsedUrl))
            {
                parsedItem = parsedItem ?? new JsonFeedItem();
                parsedItem.Url = parsedUrl;
            }

            if (itemObject.TryGetStringProperty("external_url", out var parsedExternalUrl))
            {
                parsedItem = parsedItem ?? new JsonFeedItem();
                parsedItem.ExternalUrl = parsedExternalUrl;
            }

            if (itemObject.TryGetStringProperty("title", out var parsedTitle))
            {
                parsedItem = parsedItem ?? new JsonFeedItem();
                parsedItem.Title = parsedTitle;
            }

            if (itemObject.TryGetStringProperty("content_html", out var parsedContentHtml))
            {
                parsedItem = parsedItem ?? new JsonFeedItem();
                parsedItem.ContentHtml = parsedContentHtml;
            }

            if (itemObject.TryGetStringProperty("content_text", out var parsedContentText))
            {
                parsedItem = parsedItem ?? new JsonFeedItem();
                parsedItem.ContentText = parsedContentText;
            }

            if (itemObject.TryGetStringProperty("summary", out var parsedSummary))
            {
                parsedItem = parsedItem ?? new JsonFeedItem();
                parsedItem.Summary = parsedSummary;
            }

            if (itemObject.TryGetStringProperty("image", out var parsedImage))
            {
                parsedItem = parsedItem ?? new JsonFeedItem();
                parsedItem.Image = parsedImage;
            }

            if (itemObject.TryGetStringProperty("banner_image", out var parsedBannerImage))
            {
                parsedItem = parsedItem ?? new JsonFeedItem();
                parsedItem.BannerImage = parsedBannerImage;
            }

            if (TryParseJsonFeedTimestampProperty(itemObject, "date_published", out var parsedDatePublished))
            {
                parsedItem = parsedItem ?? new JsonFeedItem();
                parsedItem.DatePublished = parsedDatePublished;
            }

            if (TryParseJsonFeedTimestampProperty(itemObject, "date_modified", out var parsedDateModified))
            {
                parsedItem = parsedItem ?? new JsonFeedItem();
                parsedItem.DateModified = parsedDateModified;
            }

            if (itemObject.TryGetJObjectProperty("author", out var authorElement) && TryParseJsonFeedAuthor(authorElement, out var parsedAuthor))
            {
                parsedItem = parsedItem ?? new JsonFeedItem();
                parsedItem.Author = parsedAuthor;
            }

            if (itemObject.TryGetStringArrayProperty("tags", out var parsedTags))
            {
                foreach (var parsedTag in parsedTags)
                {
                    parsedItem = parsedItem ?? new JsonFeedItem();
                    parsedItem.Tags.Add(parsedTag);
                }
            }

            if (itemObject.TryGetJObjectArrayProperty("attachments", out var attachmentElements))
            {
                foreach (var attachmentElement in attachmentElements)
                {
                    if (TryParseJsonFeedAttachment(attachmentElement, out var parsedAttachment))
                    {
                        parsedItem = parsedItem ?? new JsonFeedItem();
                        parsedItem.Attachments.Add(parsedAttachment);
                    }
                }
            }
            
            if (parsedItem == null)
                return false;

            // extensions
            ExtensibleEntityParser.ParseJObjectExtensions(itemObject, extensionManifestDirectory, parsedItem);

            return true;
        }

        private static bool TryParseJsonFeedAttachment(in JObject attachmentObject, out JsonFeedAttachment parsedAttachment)
        {
            parsedAttachment = null;

            if (attachmentObject.TryGetStringProperty("url", out var parsedUrl))
            {
                // ReSharper disable once ConstantNullCoalescingCondition
                parsedAttachment = parsedAttachment ?? new JsonFeedAttachment();
                parsedAttachment.Url = parsedUrl;
            }

            if (attachmentObject.TryGetStringProperty("mime_type", out var parsedMimeType))
            {
                parsedAttachment = parsedAttachment ?? new JsonFeedAttachment();
                parsedAttachment.MimeType = parsedMimeType;
            }

            if (attachmentObject.TryGetStringProperty("title", out var parsedTitle))
            {
                parsedAttachment = parsedAttachment ?? new JsonFeedAttachment();
                parsedAttachment.Title = parsedTitle;
            }

            if (TryParseJsonFeedNumberProperty(attachmentObject, "size_in_bytes", out var parsedSizeInBytes))
            {
                parsedAttachment = parsedAttachment ?? new JsonFeedAttachment();
                parsedAttachment.SizeInBytes = parsedSizeInBytes;
            }

            if (TryParseJsonFeedNumberProperty(attachmentObject, "duration_in_seconds", out var parsedDurationInSeconds))
            {
                parsedAttachment = parsedAttachment ?? new JsonFeedAttachment();
                parsedAttachment.DurationInSeconds = parsedDurationInSeconds;
            }

            if (parsedAttachment == null)
                return false;

            return true;
        }

        private static bool TryParseJsonFeedTimestampProperty(in JObject parentObject, string propertyName, out DateTimeOffset parsedTimestamp)
        {
            parsedTimestamp = default;

            if (!parentObject.TryGetStringProperty(propertyName, out var parsedString))
                return false;

            if (!RelaxedTimestampParser.TryParseTimestampFromString(parsedString, out parsedTimestamp))
                return false;

            return true;
        }

        private static bool TryParseJsonFeedHub(in JObject hubObject, out JsonFeedHub parsedHub)
        {
            parsedHub = null;

            if (hubObject.TryGetStringProperty("type", out var parsedType))
            {
                // ReSharper disable once ConstantNullCoalescingCondition
                parsedHub = parsedHub ?? new JsonFeedHub();
                parsedHub.Type = parsedType;
            }

            if (hubObject.TryGetStringProperty("url", out var parsedUrl))
            {
                parsedHub = parsedHub ?? new JsonFeedHub();
                parsedHub.Url = parsedUrl;
            }

            if (parsedHub == null)
                return false;

            return true;
        }

        private static bool TryParseJsonFeedAuthor(in JObject authorObject, out JsonFeedAuthor parsedAuthor)
        {
            parsedAuthor = null;

            if (authorObject.TryGetStringProperty("name", out var parsedName))
            {
                // ReSharper disable once ConstantNullCoalescingCondition
                parsedAuthor = parsedAuthor ?? new JsonFeedAuthor();
                parsedAuthor.Name = parsedName;
            }

            if (authorObject.TryGetStringProperty("url", out var parsedUrl))
            {
                parsedAuthor = parsedAuthor ?? new JsonFeedAuthor();
                parsedAuthor.Url = parsedUrl;
            }

            if (authorObject.TryGetStringProperty("avatar", out var parsedAvatar))
            {
                parsedAuthor = parsedAuthor ?? new JsonFeedAuthor();
                parsedAuthor.Avatar = parsedAvatar;
            }

            if (parsedAuthor == null)
                return false;

            return true;
        }

        private static bool TryParseJsonFeedNumberProperty(in JObject parentObject, string propertyName, out int parsedValue)
        {
            parsedValue = default;

            if (parentObject.TryGetIntegerProperty(propertyName, out parsedValue))
                return true;

            if (parentObject.TryGetDoubleProperty(propertyName, out var parsedDouble))
            {
                parsedValue = (int) parsedDouble;
                return true;
            }

            return false;
        }
    }
}