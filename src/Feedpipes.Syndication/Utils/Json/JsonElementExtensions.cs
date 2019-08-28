using System.Collections.Generic;
using System.Text.Json;

namespace Feedpipes.Syndication.Utils.Json
{
    internal static class JsonElementExtensions
    {
        public static bool TryGetString(in this JsonElement jsonElement, out string value)
        {
            value = null;

            switch (jsonElement.ValueKind)
            {
                case JsonValueKind.String:
                    value = jsonElement.GetString();
                    return true;
                default:
                    return false;
            }
        }

        public static bool TryGetBool(in this JsonElement jsonElement, out bool value)
        {
            value = false;

            switch (jsonElement.ValueKind)
            {
                case JsonValueKind.True:
                    value = true;
                    return true;
                case JsonValueKind.False:
                    return true;
                default:
                    return false;
            }
        }

        public static bool TryGetArray(in this JsonElement jsonElement, out IEnumerable<JsonElement> values)
        {
            values = null;

            switch (jsonElement.ValueKind)
            {
                case JsonValueKind.Array:
                    values = jsonElement.EnumerateArray();
                    return true;
                default:
                    return false;
            }
        }
    }
}