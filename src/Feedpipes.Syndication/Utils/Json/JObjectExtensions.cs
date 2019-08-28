using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Feedpipes.Syndication.Utils.Json
{
    internal static class JObjectExtensions
    {
        public static void AddRange(this JObject obj, IEnumerable<JToken> tokens)
        {
            foreach (var token in tokens)
            {
                obj.Add(token);
            }
        }

        public static bool TryGetJObjectProperty(this JObject parentElement, string propertyName, out JObject parsedValue)
        {
            parsedValue = default;

            var property = parentElement?.Property(propertyName);
            if (property == null)
                return false;

            if (property.Value?.Type != JTokenType.Object)
                return false;

            parsedValue = (JObject) property.Value;
            return true;
        }

        public static bool TryGetJObjectArrayProperty(this JObject parentElement, string propertyName, out IList<JObject> parsedValues)
        {
            parsedValues = default;

            var property = parentElement?.Property(propertyName);
            if (property == null)
                return false;

            if (property.Value?.Type != JTokenType.Array)
                return false;

            var valuesArray = (JArray) property.Value;
            var results = new List<JObject>();

            foreach (var value in valuesArray)
            {
                if (value.Type != JTokenType.Object)
                    continue;

                results.Add((JObject)value);
            }

            if (!results.Any())
                return false;

            parsedValues = results;
            return true;
        }
        
        public static bool TryGetStringArrayProperty(this JObject parentElement, string propertyName, out IList<string> parsedValues)
        {
            parsedValues = default;

            var property = parentElement?.Property(propertyName);
            if (property == null)
                return false;

            if (property.Value?.Type != JTokenType.Array)
                return false;

            var valuesArray = (JArray) property.Value;
            var results = new List<string>();

            foreach (var value in valuesArray)
            {
                if (value.Type != JTokenType.String)
                    continue;

                results.Add(value.Value<string>());
            }

            if (!results.Any())
                return false;

            parsedValues = results;
            return true;
        }
        
        public static bool TryGetStringProperty(this JObject parentElement, string propertyName, out string parsedValue)
        {
            parsedValue = default;

            var property = parentElement?.Property(propertyName);
            if (property == null)
                return false;

            if (property.Value?.Type != JTokenType.String && property.Value?.Type != JTokenType.Date)
                return false;

            parsedValue = property.Value.Value<string>();
            return true;
        }
        
        public static bool TryGetBoolProperty(this JObject parentElement, string propertyName, out bool parsedValue)
        {
            parsedValue = default;

            var property = parentElement?.Property(propertyName);
            if (property == null)
                return false;

            if (property.Value?.Type != JTokenType.Boolean)
                return false;

            parsedValue = property.Value.Value<bool>();
            return true;
        }
        
        public static bool TryGetIntegerProperty(this JObject parentElement, string propertyName, out int parsedValue)
        {
            parsedValue = default;

            var property = parentElement?.Property(propertyName);
            if (property == null)
                return false;

            if (property.Value?.Type != JTokenType.Integer)
                return false;
            
            parsedValue = property.Value.Value<int>();
            return true;
        }
        
        public static bool TryGetDoubleProperty(this JObject parentElement, string propertyName, out double parsedValue)
        {
            parsedValue = default;

            var property = parentElement?.Property(propertyName);
            if (property == null)
                return false;

            if (property.Value?.Type != JTokenType.Float)
                return false;
            
            parsedValue = property.Value.Value<double>();
            return true;
        }
    }
}