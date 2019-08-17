using System;

namespace Feedpipes.Syndication.Kvp
{
    public class KvpBagValue
    {
        public KvpBagValue(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value));

            Value = value;
        }

        public string Value { get; }

        public void Deconstruct(out string value) => value = Value;
        public static implicit operator KvpBagValue(string input) => new KvpBagValue(input);
        public static implicit operator string(KvpBagValue input) => input.Value;
    }
}