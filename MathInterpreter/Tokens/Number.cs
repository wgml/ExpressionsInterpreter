using System.Globalization;

namespace MathInterpreter.Tokens
{
    public class Number : IToken
    {
        public readonly double Value;

        public Number(string token)
        {
            Value = double.Parse(token);
        }

        public Number(double value)
        {
            Value = value;
        }

        public static implicit operator Number(double value)
        {
            return new Number(value);
        }

        public static implicit operator double(Number number)
        {
            return number.Value;
        }

        public override string ToString()
        {
            return Value.ToString(CultureInfo.CurrentCulture);
        }
    }
}