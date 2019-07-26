using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Feedpipes.Syndication.Extensions.Rss10Slash.Entities;

namespace Feedpipes.Syndication.Extensions.Rss10Slash
{
    public class Rss10SlashExtensionParser : IFeedExtensionParser
    {
        public IEnumerable<IFeedExtensionEntity> ParseExtensionEntities(XElement parentElement)
        {
            if (parentElement == null)
                yield break;

            foreach (var element in parentElement.Elements(Rss10SlashExtensionConstants.Namespace + "section"))
            {
                if (!TryParseRss10SlashSection(element, out var entity))
                    continue;

                yield return entity;
            }

            foreach (var element in parentElement.Elements(Rss10SlashExtensionConstants.Namespace + "department"))
            {
                if (!TryParseRss10SlashDepartment(element, out var entity))
                    continue;

                yield return entity;
            }

            foreach (var element in parentElement.Elements(Rss10SlashExtensionConstants.Namespace + "comments"))
            {
                if (!TryParseRss10SlashComments(element, out var entity))
                    continue;

                yield return entity;
            }

            foreach (var element in parentElement.Elements(Rss10SlashExtensionConstants.Namespace + "hit_parade"))
            {
                if (!TryParseRss10SlashHitParade(element, out var entity))
                    continue;

                yield return entity;
            }
        }

        private bool TryParseRss10SlashSection(XElement element, out Rss10SlashSection entity)
        {
            entity = default;

            if (element == null)
                return false;

            entity = new Rss10SlashSection { Value = element.Value.Trim() };
            return true;
        }

        private bool TryParseRss10SlashDepartment(XElement element, out Rss10SlashDepartment entity)
        {
            entity = default;

            if (element == null)
                return false;

            entity = new Rss10SlashDepartment { Value = element.Value.Trim() };
            return true;
        }

        private bool TryParseRss10SlashComments(XElement element, out Rss10SlashComments entity)
        {
            entity = default;

            if (element == null)
                return false;

            var valueString = element.Value.Trim();
            if (!int.TryParse(valueString, out var valueInt))
                return false;

            entity = new Rss10SlashComments { Count = valueInt };
            return true;
        }

        private bool TryParseRss10SlashHitParade(XElement element, out Rss10SlashHitParade entity)
        {
            entity = default;

            if (element == null)
                return false;

            var valueString = element.Value.Trim();
            var valueStringParts = valueString.Split(",").Select(x => x.Trim());
            var valueInts = new List<int>();

            foreach (var valueStringPart in valueStringParts)
            {
                if (!int.TryParse(valueStringPart, out var valueInt))
                    continue;

                valueInts.Add(valueInt);
            }

            if (!valueInts.Any())
                return false;

            entity = new Rss10SlashHitParade { Identifiers = valueInts };
            return true;
        }
    }
}