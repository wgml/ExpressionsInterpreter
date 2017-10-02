using System;

namespace MathInterpreter.Tokens
{
    public class Variable : Value
    {
        public Token Token { get; }

        public Variable(Token token, Value value)
            : base(value)
        {
            Token = token;
        }

        public override string ToString()
        {
            return Token + " = " + base.ToString();
        }

        public static implicit operator double(Variable value)
        {
            return value.Val;
        }
    }
}