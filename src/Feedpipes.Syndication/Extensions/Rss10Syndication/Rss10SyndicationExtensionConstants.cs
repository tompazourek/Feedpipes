﻿using System.Xml.Linq;

namespace Feedpipes.Syndication.Extensions.Rss10Syndication
{
    /// <remarks>
    /// Spec: http://web.resource.org/rss/1.0/modules/syndication/
    /// </remarks>
    public static class Rss10SyndicationExtensionConstants
    {
        public const string NamespaceAlias = "sy";
        public static readonly XNamespace Namespace = "http://purl.org/rss/1.0/modules/syndication/";
    }
}