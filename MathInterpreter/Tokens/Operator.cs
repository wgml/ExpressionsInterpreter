using System;
using System.Security.Policy;
using static MathInterpreter.Tokens.Tokens;

namespace MathInterpreter.Tokens
{
    public class Operator : Token
    {
        public readonly uint Precedence;
        public readonly Associativity Assoc;
        private readonly Func<Number, Number, Number> _action;

        internal Operator(string token, uint precedence,
            Associativity associativity, Func<Number, Number, Number> action)
            : base(token)
        {
            Precedence = precedence;
            Assoc = associativity;
            _action = action;
        }

        public bool Precedent(Operator other)
        {
            return Precedence >= other.Precedence;
        }

        public Number Evaluate(Number first, Number second)
        {
            return _action(first, second);
        }

        public bool LeftAssociative()
        {
            return Assoc == Associativity.Left;
        }
    }
}