namespace MathInterpreter.Tokens
{
    public class Token : IToken
    {
        public string Value { get; }

        public Token(string value)
        {
            Value = value;
        }

        protected bool Equals(Token other)
        {
            return string.Equals(Value, other.Value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Token) obj);
        }

        public override int GetHashCode()
        {
            return (Value != null ? Value.GetHashCode() : 0);
        }

        public static bool operator ==(Token first, Token second)
        {
            return first?.Value == second?.Value;
        }

        public static bool operator !=(Token first, Token second)
        {
            return !(first == second);
        }

        public static implicit operator Token(string token)
        {
            return new Token(token);
        }

        public override string ToString()
        {
            return Value;
        }
    }
}