using System;
using System.Collections.Generic;
using System.Linq;
using Feedpipes.Syndication.JsonFeedFormat.Entities;
using Feedpipes.Syndication.Timestamps.Rfc3339;
using Newtonsoft.Json.Linq;

namespace Feedpipes.Syndication.JsonFeedFormat
{
    public static class JsonFeedFormatter
    {
        public static bool TryFormatJsonFeed(JsonFeed feedToFormat, out JObject feedObject)
        {
            feedObject = default;

            if (feedToFormat == null)
                return false;

            feedObject = new JObject
            {
                new JProperty("version", new JValue(JsonFeedConstants.Version)),
            };

            if (TryFormatJsonFeedRequiredStringProperty("title", feedToFormat.Title, out var titleProperty))
            {
                feedObject.Add(titleProperty);
            }

            if (TryFormatJsonFeedOptionalStringProperty("home_page_url", feedToFormat.HomePageUrl, out var homePageUrlProperty))
            {
                feedObject.Add(homePageUrlProperty);
            }

            if (TryFormatJsonFeedOptionalStringProperty("feed_url", feedToFormat.FeedUrl, out var feedUrlProperty))
            {
                feedObject.Add(feedUrlProperty);
            }

            if (TryFormatJsonFeedOptionalStringProperty("description", feedToFormat.Description, out var descriptionProperty))
            {
                feedObject.Add(descriptionProperty);
            }

            if (TryFormatJsonFeedOptionalStringProperty("user_comment", feedToFormat.UserComment, out var userCommentProperty))
            {
                feedObject.Add(userCommentProperty);
            }

            if (TryFormatJsonFeedOptionalStringProperty("next_url", feedToFormat.NextUrl, out var nextUrlProperty))
            {
                feedObject.Add(nextUrlProperty);
            }

            if (TryFormatJsonFeedOptionalStringProperty("icon", feedToFormat.Icon, out var iconProperty))
            {
                feedObject.Add(iconProperty);
            }

            if (TryFormatJsonFeedOptionalStringProperty("favicon", feedToFormat.Favicon, out var faviconProperty))
            {
                feedObject.Add(faviconProperty);
            }

            if (TryFormatJsonFeedAuthorProperty("author", feedToFormat.Author, out var authorProperty))
            {
                feedObject.Add(authorProperty);
            }

            if (TryFormatJsonFeedOptionalBoolProperty("expired", feedToFormat.Expired, out var expiredProperty))
            {
                feedObject.Add(expiredProperty);
            }

            if (TryFormatJsonFeedHubsProperty("hubs", feedToFormat.Hubs, out var hubsProperty))
            {
                feedObject.Add(hubsProperty);
            }

            if (TryFormatJsonFeedItemsProperty("items", feedToFormat.Items, out var itemsProperty))
            {
                feedObject.Add(itemsProperty);
            }

            return true;
        }

        private static bool TryFormatJsonFeedItemsProperty(string propertyName, IList<JsonFeedItem> itemsToFormat, out JProperty itemsProperty)
        {
            var itemsArray = new JArray();

            if (itemsToFormat != null)
            {
                foreach (var itemToFormat in itemsToFormat)
                {
                    if (!TryFormatJsonFeedItemObject(itemToFormat, out var itemObject))
                        continue;

                    itemsArray.Add(itemObject);
                }
            }

            itemsProperty = new JProperty(propertyName, itemsArray);
            return true;
        }

        private static bool TryFormatJsonFeedItemObject(JsonFeedItem itemToFormat, out JObject itemObject)
        {
            itemObject = default;

            if (itemToFormat == null)
                return false;

            itemObject = new JObject();

            if (TryFormatJsonFeedRequiredStringProperty("id", itemToFormat.Id, out var idProperty))
            {
                itemObject.Add(idProperty);
            }

            if (TryFormatJsonFeedOptionalStringProperty("url", itemToFormat.Url, out var urlProperty))
            {
                itemObject.Add(urlProperty);
            }

            if (TryFormatJsonFeedOptionalStringProperty("external_url", itemToFormat.ExternalUrl, out var externalUrlProperty))
            {
                itemObject.Add(externalUrlProperty);
            }

            if (TryFormatJsonFeedOptionalStringProperty("title", itemToFormat.Title, out var titleProperty))
            {
                itemObject.Add(titleProperty);
            }

            if (TryFormatJsonFeedOptionalStringProperty("content_html", itemToFormat.ContentHtml, out var contentHtmlProperty))
            {
                itemObject.Add(contentHtmlProperty);
            }

            if (TryFormatJsonFeedOptionalStringProperty("content_text", itemToFormat.ContentText, out var contentTextProperty))
            {
                itemObject.Add(contentTextProperty);
            }

            if (TryFormatJsonFeedOptionalStringProperty("summary", itemToFormat.Summary, out var summaryProperty))
            {
                itemObject.Add(summaryProperty);
            }

            if (TryFormatJsonFeedOptionalStringProperty("image", itemToFormat.Image, out var imageProperty))
            {
                itemObject.Add(imageProperty);
            }

            if (TryFormatJsonFeedOptionalStringProperty("banner_image", itemToFormat.BannerImage, out var bannerImageProperty))
            {
                itemObject.Add(bannerImageProperty);
            }

            if (TryFormatJsonFeedOptionalTimestampProperty("date_published", itemToFormat.DatePublished, out var datePublishedProperty))
            {
                itemObject.Add(datePublishedProperty);
            }

            if (TryFormatJsonFeedOptionalTimestampProperty("date_modified", itemToFormat.DateModified, out var dateModifiedProperty))
            {
                itemObject.Add(dateModifiedProperty);
            }

            if (TryFormatJsonFeedAuthorProperty("author", itemToFormat.Author, out var authorProperty))
            {
                itemObject.Add(authorProperty);
            }

            if (TryFormatJsonFeedTagsProperty("tags", itemToFormat.Tags, out var tagsProperty))
            {
                itemObject.Add(tagsProperty);
            }

            if (TryFormatJsonFeedAttachmentsProperty("attachments", itemToFormat.Attachments, out var attachmentsProperty))
            {
                itemObject.Add(attachmentsProperty);
            }

            return true;
        }

        private static bool TryFormatJsonFeedAttachmentsProperty(string propertyName, IList<JsonFeedAttachment> attachmentsToFormat, out JProperty attachmentsProperty)
        {
            attachmentsProperty = default;
            var attachmentsArray = new JArray();

            if (attachmentsToFormat != null)
            {
                foreach (var attachmentToFormat in attachmentsToFormat)
                {
                    if (attachmentToFormat == null)
                        continue;

                    if (!TryFormatJsonFeedAttachmentObject(attachmentToFormat, out var attachmentObject))
                        continue;

                    attachmentsArray.Add(attachmentObject);
                }
            }

            if (!attachmentsArray.Any())
                return false;

            attachmentsProperty = new JProperty(propertyName, attachmentsArray);
            return true;
        }

        private static bool TryFormatJsonFeedAttachmentObject(JsonFeedAttachment attachmentToFormat, out JObject attachmentObject)
        {
            attachmentObject = null;

            if (attachmentToFormat == null)
                return false;

            if (TryFormatJsonFeedRequiredStringProperty("url", attachmentToFormat.Url, out var urlProperty))
            {
                // ReSharper disable once ConstantNullCoalescingCondition
                attachmentObject = attachmentObject ?? new JObject();
                attachmentObject.Add(urlProperty);
            }

            if (TryFormatJsonFeedOptionalStringProperty("mime_type", attachmentToFormat.MimeType, out var mimeTypeProperty))
            {
                attachmentObject = attachmentObject ?? new JObject();
                attachmentObject.Add(mimeTypeProperty);
            }

            if (TryFormatJsonFeedOptionalStringProperty("title", attachmentToFormat.Title, out var titleProperty))
            {
                attachmentObject = attachmentObject ?? new JObject();
                attachmentObject.Add(titleProperty);
            }

            if (TryFormatJsonFeedOptionalIntegerProperty("size_in_bytes", attachmentToFormat.SizeInBytes, out var sizeInBytesProperty))
            {
                attachmentObject = attachmentObject ?? new JObject();
                attachmentObject.Add(sizeInBytesProperty);
            }

            if (TryFormatJsonFeedOptionalIntegerProperty("duration_in_seconds", attachmentToFormat.DurationInSeconds, out var durationInSecondsProperty))
            {
                attachmentObject = attachmentObject ?? new JObject();
                attachmentObject.Add(durationInSecondsProperty);
            }

            if (attachmentObject == null)
                return false;

            return true;
        }

        private static bool TryFormatJsonFeedTagsProperty(string propertyName, IList<string> tagsToFormat, out JProperty tagsProperty)
        {
            tagsProperty = default;
            var tagsArray = new JArray();

            if (tagsToFormat != null)
            {
                foreach (var tagToFormat in tagsToFormat)
                {
                    if (string.IsNullOrEmpty(tagToFormat))
                        continue;

                    tagsArray.Add(new JValue(tagToFormat));
                }
            }

            if (!tagsArray.Any())
                return false;

            tagsProperty = new JProperty(propertyName, tagsArray);
            return true;
        }

        private static bool TryFormatJsonFeedAuthorProperty(string propertyName, JsonFeedAuthor authorToFormat, out JProperty authorProperty)
        {
            authorProperty = default;

            if (!TryFormatJsonFeedAuthorObject(authorToFormat, out var authorObject))
                return false;

            authorProperty = new JProperty(propertyName, authorObject);
            return true;
        }

        private static bool TryFormatJsonFeedAuthorObject(JsonFeedAuthor authorToFormat, out JObject authorObject)
        {
            authorObject = null;

            if (authorToFormat == null)
                return false;

            if (TryFormatJsonFeedOptionalStringProperty("name", authorToFormat.Name, out var nameProperty))
            {
                // ReSharper disable once ConstantNullCoalescingCondition
                authorObject = authorObject ?? new JObject();
                authorObject.Add(nameProperty);
            }

            if (TryFormatJsonFeedOptionalStringProperty("url", authorToFormat.Url, out var urlProperty))
            {
                authorObject = authorObject ?? new JObject();
                authorObject.Add(urlProperty);
            }

            if (TryFormatJsonFeedOptionalStringProperty("avatar", authorToFormat.Avatar, out var avatarProperty))
            {
                authorObject = authorObject ?? new JObject();
                authorObject.Add(avatarProperty);
            }

            if (authorObject == null)
                return false;

            return true;
        }

        private static bool TryFormatJsonFeedHubsProperty(string propertyName, IList<JsonFeedHub> hubsToFormat, out JProperty hubsProperty)
        {
            hubsProperty = default;
            var hubsArray = new JArray();

            if (hubsToFormat != null)
            {
                foreach (var hubToFormat in hubsToFormat)
                {
                    if (hubToFormat == null)
                        continue;

                    if (!TryFormatJsonFeedHubObject(hubToFormat, out var attachmentObject))
                        continue;

                    hubsArray.Add(attachmentObject);
                }
            }

            if (!hubsArray.Any())
                return false;

            hubsProperty = new JProperty(propertyName, hubsArray);
            return true;
        }

        private static bool TryFormatJsonFeedHubObject(JsonFeedHub hubToFormat, out JObject hubObject)
        {
            hubObject = null;

            if (hubToFormat == null)
                return false;

            if (TryFormatJsonFeedOptionalStringProperty("type", hubToFormat.Type, out var typeProperty))
            {
                // ReSharper disable once ConstantNullCoalescingCondition
                hubObject = hubObject ?? new JObject();
                hubObject.Add(typeProperty);
            }

            if (TryFormatJsonFeedOptionalStringProperty("url", hubToFormat.Url, out var urlProperty))
            {
                hubObject = hubObject ?? new JObject();
                hubObject.Add(urlProperty);
            }

            if (hubObject == null)
                return false;

            return true;
        }

        private static bool TryFormatJsonFeedRequiredStringProperty(string propertyName, string stringToFormat, out JProperty stringProperty)
        {
            stringProperty = new JProperty(propertyName, new JValue(stringToFormat ?? string.Empty));
            return true;
        }

        private static bool TryFormatJsonFeedOptionalStringProperty(string propertyName, string stringToFormat, out JProperty stringProperty)
        {
            stringProperty = default;

            if (string.IsNullOrEmpty(stringToFormat))
                return false;

            stringProperty = new JProperty(propertyName, new JValue(stringToFormat));
            return true;
        }

        private static bool TryFormatJsonFeedOptionalBoolProperty(string propertyName, bool? boolToFormat, out JProperty boolProperty)
        {
            boolProperty = default;

            if (boolToFormat == null)
                return false;

            boolProperty = new JProperty(propertyName, new JValue(boolToFormat.Value));
            return true;
        }

        private static bool TryFormatJsonFeedOptionalIntegerProperty(string propertyName, int? integerToFormat, out JProperty integerProperty)
        {
            integerProperty = default;

            if (integerToFormat == null)
                return false;

            integerProperty = new JProperty(propertyName, new JValue(integerToFormat.Value));
            return true;
        }

        private static bool TryFormatJsonFeedOptionalTimestampProperty(string propertyName, DateTimeOffset? timestampToFormat, out JProperty timestampProperty)
        {
            timestampProperty = default;

            if (timestampToFormat == null)
                return false;

            if (!Rfc3339TimestampFormatter.TryFormatTimestampAsString(timestampToFormat, out var formattedTimestamp))
                return false;

            timestampProperty = new JProperty(propertyName, new JValue(formattedTimestamp));
            return true;
        }
    }
}