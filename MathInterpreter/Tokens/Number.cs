using System.Globalization;

namespace MathInterpreter.Tokens
{
    public  struct Number : IToken
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

        public override string ToString()
        {
            return Value.ToString(CultureInfo.CurrentCulture);
        }
    }
}