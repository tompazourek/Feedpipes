using System;
using System.Text.Json;
using Feedpipes.Syndication.JsonFeed.Entities;
using Feedpipes.Syndication.Timestamps.Relaxed;
using Feedpipes.Syndication.Utils.Json;

namespace Feedpipes.Syndication.JsonFeed
{
    public static class JsonFeedParser
    {
        public static bool TryParseJsonFeed(in JsonDocument document, out Entities.JsonFeed parsedFeed)
        {
            parsedFeed = default;

            var feedElement = document.RootElement;
            if (!feedElement.TryGetProperty("version", out var versionElement))
                return false;

            if (!versionElement.TryGetString(out var parsedVersion))
                return false;

            if (!JsonFeedConstants.RecognizedVersions.Contains(parsedVersion))
                return false;

            parsedFeed = new Entities.JsonFeed { Version = parsedVersion };

            if (TryParseJsonFeedStringElement(feedElement, "title", out var parsedTitle))
            {
                parsedFeed.Title = parsedTitle;
            }

            if (TryParseJsonFeedStringElement(feedElement, "home_page_url", out var parsedHomePageUrl))
            {
                parsedFeed.HomePageUrl = parsedHomePageUrl;
            }

            if (TryParseJsonFeedStringElement(feedElement, "feed_url", out var parsedFeedUrl))
            {
                parsedFeed.FeedUrl = parsedFeedUrl;
            }

            if (TryParseJsonFeedStringElement(feedElement, "description", out var parsedDescription))
            {
                parsedFeed.Description = parsedDescription;
            }

            if (TryParseJsonFeedStringElement(feedElement, "user_comment", out var parsedUserComment))
            {
                parsedFeed.UserComment = parsedUserComment;
            }

            if (TryParseJsonFeedStringElement(feedElement, "next_url", out var parsedNextUrl))
            {
                parsedFeed.NextUrl = parsedNextUrl;
            }

            if (TryParseJsonFeedStringElement(feedElement, "icon", out var parsedIcon))
            {
                parsedFeed.Icon = parsedIcon;
            }

            if (TryParseJsonFeedStringElement(feedElement, "favicon", out var parsedFavicon))
            {
                parsedFeed.Favicon = parsedFavicon;
            }

            if (feedElement.TryGetProperty("author", out var authorElement) && TryParseJsonFeedAuthor(authorElement, out var parsedAuthor))
            {
                parsedFeed.Author = parsedAuthor;
            }

            if (TryParseJsonFeedBoolElement(feedElement, "expired", out var parsedExpired))
            {
                parsedFeed.Expired = parsedExpired;
            }

            if (feedElement.TryGetProperty("hubs", out var hubsElement) && hubsElement.TryGetArray(out var hubElements))
            {
                foreach (var hubElement in hubElements)
                {
                    if (TryParseJsonFeedHub(hubElement, out var parsedHub))
                    {
                        parsedFeed.Hubs.Add(parsedHub);
                    }
                }
            }

            if (feedElement.TryGetProperty("items", out var itemsElement) && itemsElement.TryGetArray(out var itemElements))
            {
                foreach (var itemElement in itemElements)
                {
                    if (TryParseJsonFeedItemElement(itemElement, out var parsedItem))
                    {
                        parsedFeed.Items.Add(parsedItem);
                    }
                }
            }

            return true;
        }

        private static bool TryParseJsonFeedItemElement(in JsonElement itemElement, out JsonFeedItem parsedItem)
        {
            parsedItem = null;

            if (TryParseJsonFeedStringElement(itemElement, "id", out var parsedId))
            {
                // ReSharper disable once ConstantNullCoalescingCondition
                parsedItem = parsedItem ?? new JsonFeedItem();
                parsedItem.Id = parsedId;
            }

            if (TryParseJsonFeedStringElement(itemElement, "url", out var parsedUrl))
            {
                parsedItem = parsedItem ?? new JsonFeedItem();
                parsedItem.Url = parsedUrl;
            }

            if (TryParseJsonFeedStringElement(itemElement, "external_url", out var parsedExternalUrl))
            {
                parsedItem = parsedItem ?? new JsonFeedItem();
                parsedItem.ExternalUrl = parsedExternalUrl;
            }

            if (TryParseJsonFeedStringElement(itemElement, "title", out var parsedTitle))
            {
                parsedItem = parsedItem ?? new JsonFeedItem();
                parsedItem.Title = parsedTitle;
            }

            if (TryParseJsonFeedStringElement(itemElement, "content_html", out var parsedContentHtml))
            {
                parsedItem = parsedItem ?? new JsonFeedItem();
                parsedItem.ContentHtml = parsedContentHtml;
            }

            if (TryParseJsonFeedStringElement(itemElement, "content_text", out var parsedContentText))
            {
                parsedItem = parsedItem ?? new JsonFeedItem();
                parsedItem.ContentText = parsedContentText;
            }

            if (TryParseJsonFeedStringElement(itemElement, "summary", out var parsedSummary))
            {
                parsedItem = parsedItem ?? new JsonFeedItem();
                parsedItem.Summary = parsedSummary;
            }

            if (TryParseJsonFeedStringElement(itemElement, "image", out var parsedImage))
            {
                parsedItem = parsedItem ?? new JsonFeedItem();
                parsedItem.Image = parsedImage;
            }

            if (TryParseJsonFeedStringElement(itemElement, "banner_image", out var parsedBannerImage))
            {
                parsedItem = parsedItem ?? new JsonFeedItem();
                parsedItem.BannerImage = parsedBannerImage;
            }

            if (TryParseJsonFeedTimestampElement(itemElement, "date_published", out var parsedDatePublished))
            {
                parsedItem = parsedItem ?? new JsonFeedItem();
                parsedItem.DatePublished = parsedDatePublished;
            }

            if (TryParseJsonFeedTimestampElement(itemElement, "date_modified", out var parsedDateModified))
            {
                parsedItem = parsedItem ?? new JsonFeedItem();
                parsedItem.DateModified = parsedDateModified;
            }

            if (itemElement.TryGetProperty("author", out var authorElement) && TryParseJsonFeedAuthor(authorElement, out var parsedAuthor))
            {
                parsedItem = parsedItem ?? new JsonFeedItem();
                parsedItem.Author = parsedAuthor;
            }

            if (itemElement.TryGetProperty("tags", out var tagsElement) && tagsElement.TryGetArray(out var tagElements))
            {
                foreach (var tagElement in tagElements)
                {
                    if (tagElement.TryGetString(out var parsedTag))
                    {
                        parsedItem = parsedItem ?? new JsonFeedItem();
                        parsedItem.Tags.Add(parsedTag);
                    }
                }
            }

            if (itemElement.TryGetProperty("attachments", out var attachmentsElement) && attachmentsElement.TryGetArray(out var attachmentElements))
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

            return true;
        }

        private static bool TryParseJsonFeedAttachment(in JsonElement attachmentElement, out JsonFeedAttachment parsedAttachment)
        {
            parsedAttachment = null;

            if (TryParseJsonFeedStringElement(attachmentElement, "url", out var parsedUrl))
            {
                // ReSharper disable once ConstantNullCoalescingCondition
                parsedAttachment = parsedAttachment ?? new JsonFeedAttachment();
                parsedAttachment.Url = parsedUrl;
            }

            if (TryParseJsonFeedStringElement(attachmentElement, "mime_type", out var parsedMimeType))
            {
                parsedAttachment = parsedAttachment ?? new JsonFeedAttachment();
                parsedAttachment.MimeType = parsedMimeType;
            }

            if (TryParseJsonFeedStringElement(attachmentElement, "title", out var parsedTitle))
            {
                parsedAttachment = parsedAttachment ?? new JsonFeedAttachment();
                parsedAttachment.Title = parsedTitle;
            }

            if (TryParseJsonFeedNumberElement(attachmentElement, "size_in_bytes", out var parsedSizeInBytes))
            {
                parsedAttachment = parsedAttachment ?? new JsonFeedAttachment();
                parsedAttachment.SizeInBytes = parsedSizeInBytes;
            }

            if (TryParseJsonFeedNumberElement(attachmentElement, "duration_in_seconds", out var parsedDurationInSeconds))
            {
                parsedAttachment = parsedAttachment ?? new JsonFeedAttachment();
                parsedAttachment.DurationInSeconds = parsedDurationInSeconds;
            }

            if (parsedAttachment == null)
                return false;

            return true;
        }

        private static bool TryParseJsonFeedTimestampElement(in JsonElement parentElement, string propertyName, out DateTimeOffset parsedTimestamp)
        {
            parsedTimestamp = default;

            if (!parentElement.TryGetProperty(propertyName, out var propertyElement))
                return false;

            if (!propertyElement.TryGetString(out var parsedString))
                return false;

            if (!RelaxedTimestampParser.TryParseTimestampFromString(parsedString, out parsedTimestamp))
                return false;

            return true;
        }

        private static bool TryParseJsonFeedHub(in JsonElement hubElement, out JsonFeedHub parsedHub)
        {
            parsedHub = null;

            if (TryParseJsonFeedStringElement(hubElement, "type", out var parsedType))
            {
                // ReSharper disable once ConstantNullCoalescingCondition
                parsedHub = parsedHub ?? new JsonFeedHub();
                parsedHub.Type = parsedType;
            }

            if (TryParseJsonFeedStringElement(hubElement, "url", out var parsedUrl))
            {
                parsedHub = parsedHub ?? new JsonFeedHub();
                parsedHub.Url = parsedUrl;
            }

            if (parsedHub == null)
                return false;

            return true;
        }

        private static bool TryParseJsonFeedAuthor(in JsonElement authorElement, out JsonFeedAuthor parsedAuthor)
        {
            parsedAuthor = null;

            if (TryParseJsonFeedStringElement(authorElement, "name", out var parsedName))
            {
                // ReSharper disable once ConstantNullCoalescingCondition
                parsedAuthor = parsedAuthor ?? new JsonFeedAuthor();
                parsedAuthor.Name = parsedName;
            }

            if (TryParseJsonFeedStringElement(authorElement, "url", out var parsedUrl))
            {
                parsedAuthor = parsedAuthor ?? new JsonFeedAuthor();
                parsedAuthor.Url = parsedUrl;
            }

            if (TryParseJsonFeedStringElement(authorElement, "avatar", out var parsedAvatar))
            {
                parsedAuthor = parsedAuthor ?? new JsonFeedAuthor();
                parsedAuthor.Avatar = parsedAvatar;
            }

            if (parsedAuthor == null)
                return false;

            return true;
        }

        private static bool TryParseJsonFeedStringElement(in JsonElement parentElement, string propertyName, out string parsedValue)
        {
            parsedValue = default;

            if (!parentElement.TryGetProperty(propertyName, out var propertyElement))
                return false;

            if (!propertyElement.TryGetString(out parsedValue))
                return false;

            return true;
        }

        private static bool TryParseJsonFeedBoolElement(in JsonElement parentElement, string propertyName, out bool parsedValue)
        {
            parsedValue = default;

            if (!parentElement.TryGetProperty(propertyName, out var propertyElement))
                return false;

            if (!propertyElement.TryGetBool(out parsedValue))
                return false;

            return true;
        }

        private static bool TryParseJsonFeedNumberElement(in JsonElement parentElement, string propertyName, out int parsedValue)
        {
            parsedValue = default;

            if (!parentElement.TryGetProperty(propertyName, out var propertyElement))
                return false;

            if (propertyElement.TryGetInt32(out parsedValue))
                return true;

            if (propertyElement.TryGetDouble(out var parsedDouble))
            {
                parsedValue = (int) parsedDouble;
                return true;
            }

            if (propertyElement.TryGetDecimal(out var parsedDecimal))
            {
                parsedValue = (int) parsedDecimal;
                return true;
            }

            return false;
        }
    }
}