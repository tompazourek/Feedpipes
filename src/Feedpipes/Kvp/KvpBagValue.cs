using System;
using System.Diagnostics;

namespace Feedpipes.Kvp
{
    [DebuggerDisplay("{" + nameof(Value) + "}")]
    public class KvpBagValue
    {
        public KvpBagValue(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value));

            Value = value;
        }

        public string Value { get; }

        public override string ToString() => Value;

        public void Deconstruct(out string value) => value = Value;
        public static implicit operator KvpBagValue(string input) => new KvpBagValue(input);
        public static implicit operator string(KvpBagValue input) => input.Value;
    }
}