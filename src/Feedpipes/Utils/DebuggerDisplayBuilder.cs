using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Feedpipes.Utils
{
    /// <summary>
    /// Helps building debugger display for purposes of easier debugging.
    /// </summary>
    /// <remarks>
    /// Based on: http://stackoverflow.com/a/2417736/108374
    /// </remarks>
    internal static class DebuggerDisplayBuilder
    {
        public static DebuggerDisplayBuilder<T> Create<T>(T obj)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));

            return new DebuggerDisplayBuilder<T>(obj);
        }
    }

    internal class DebuggerDisplayBuilder<T>
    {
        private readonly T _obj;
        private readonly Type _objType;
        private readonly StringBuilder _innerSb;


        internal DebuggerDisplayBuilder(T obj)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            _obj = obj;
            _objType = obj.GetType();
            _innerSb = new StringBuilder();
        }

        private static string GenericPropertyValueFormatter<TProperty>(TProperty propertyValue)
        {
            string propertyValueFormatted;
            if (propertyValue == null)
            {
                propertyValueFormatted = "null";
            }
            else
            {
                if (propertyValue is string propertyValueString)
                {
                    propertyValueFormatted = propertyValueString;
                }
                else if (propertyValue is IEnumerable propertyValueEnumerable)
                {
                    propertyValueFormatted = $"Count = {propertyValueEnumerable.Cast<object>().Count()}";
                }
                else
                {
                    propertyValueFormatted = $"{propertyValue}";
                }
            }

            if (propertyValue == null || propertyValueFormatted.Length == 0)
                return null; // don't add empty properties

            return propertyValueFormatted;
        }

        public DebuggerDisplayBuilder<T> Append<TProperty>(Expression<Func<T, TProperty>> expression, bool? noQuotes = null)
            => Append(expression, GenericPropertyValueFormatter, noQuotes);

        public DebuggerDisplayBuilder<T> Append<TProperty>(Expression<Func<T, TProperty>> expression, Func<TProperty, string> propertyValueFormatter, bool? noQuotes = null)
        {
            if (expression == null)
                throw new ArgumentNullException(nameof(expression));

            if (!TryGetPropertyName(expression, out var propertyName))
                throw new ArgumentException("Expression must be a simple property expression.");

            var func = expression.Compile();
            var propertyValue = func(_obj);
            var propertyValueFormatted = propertyValueFormatter(propertyValue);

            if (string.IsNullOrEmpty(propertyValueFormatted))
                return this; // don't add empty properties

            if (_innerSb.Length >= 1)
                _innerSb.Append(", ");

            if (noQuotes == false || noQuotes == null && propertyValue is string)
            {
                propertyValueFormatted = $"\"{propertyValueFormatted.Escape()}\"";
            }

            _innerSb.Append($"{propertyName}: {propertyValueFormatted}");
            return this;
        }

        private static bool TryGetPropertyName<TProperty>(Expression<Func<T, TProperty>> expression, out string propertyName)
        {
            if (expression == null)
                throw new ArgumentNullException(nameof(expression));

            propertyName = default;

            if (!(expression.Body is MemberExpression propertyExpression))
                return false;

            propertyName = propertyExpression.Member.Name;
            return true;
        }

        public static implicit operator string(DebuggerDisplayBuilder<T> input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            return input.ToString();
        }

        public override string ToString()
        {
            var properties = _innerSb.ToString();

            return properties.Length > 0
                ? $"{{{properties}}}"
                : _objType.Name;
        }
    }
}
