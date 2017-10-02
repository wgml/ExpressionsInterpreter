using System.Globalization;

namespace MathInterpreter.Tokens
{
    public class Value : IToken
    {
        public double Val { get; set; }

        public Value(string value)
        {
            Val = double.Parse(value);
        }

        public Value(double value)
        {
            Val = value;
        }

        public static implicit operator Value(double value)
        {
            return new Value(value);
        }

        public static implicit operator double(Value value)
        {
            return value.Val;
        }

        public override string ToString()
        {
            return Val.ToString(CultureInfo.CurrentCulture);
        }
    }
}