using System;

namespace MathInterpreter.Tokens
{
    public struct Operator : IToken
    {
        public static Operator Add = new Operator("+");
        public static Operator Subtract = new Operator("-");
        public static Operator Multiply = new Operator("*");
        public static Operator Divide = new Operator("/");
        public static Operator Power = new Operator("^");

        public static Operator LeftBracket = new Operator("(");
        public static Operator RightBracket = new Operator(")");

        public readonly char Value;

        public Operator(string token)
        {
            if (token.Length != 1)
                throw new ArgumentException("Invalid token: " + token);
            Value = token[0];
        }

        public bool Precedent(Operator other)
        {
            return Precedence(this) >= Precedence(other);
        }

        public Number Evaluate(Number first, Number second)
        {
            if (this == Add)
                return new Number(first.Value + second.Value);
            else if (this == Subtract)
                return new Number(first.Value - second.Value);
            else if (this == Multiply)
                return new Number(first.Value * second.Value);
            else if (this == Divide)
                return new Number(first.Value / second.Value);
            else if (this == Power)
                return new Number(Math.Pow(first.Value, second.Value));
            else
                throw new ArgumentException("Unknown operator: " + this.Value);
        }

        public bool LeftAssociative()
        {
            return this == Add || this == Subtract || this == Multiply || this == Divide;
        }

        private static int Precedence(Operator op)
        {
            if (op == Add || op == Subtract)
                return 0;
            if (op == Multiply || op == Divide)
                return 1;
            if (op == Power)
                return 2;
            throw new ArgumentException("Operator not recognized: " + op);
        }

        public static bool operator ==(Operator first, Operator second)
        {
            return first.Value == second.Value;
        }

        public static bool operator !=(Operator first, Operator second)
        {
            return first.Value != second.Value;
        }

        public bool Equals(Operator other)
        {
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Operator && Equals((Operator) obj);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}