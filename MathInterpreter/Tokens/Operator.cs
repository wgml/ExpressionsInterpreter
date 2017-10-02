using System;

namespace MathInterpreter.Tokens
{
    public class Operator : Token
    {
        public readonly uint Precedence;
        public readonly Associativity Assoc;
        private readonly Func<Value, Value, Value> _action;

        internal Operator(string token, uint precedence,
            Associativity associativity, Func<Value, Value, Value> action)
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

        public Value Evaluate(Value first, Value second)
        {
            return _action(first, second);
        }

        public bool LeftAssociative()
        {
            return Assoc == Associativity.Left;
        }
    }
}