using System;

namespace MathInterpreter.Tokens
{
    public abstract class Tokens
    {
        public static readonly Operator Add = new Operator("+", 0, Associativity.Left, (op1, op2) => op1 + op2);
        public static readonly Operator Subtract = new Operator("-", 0, Associativity.Left, (op1, op2) => op1 - op2);
        public static readonly Operator Multiply = new Operator("*", 1, Associativity.Left, (op1, op2) => op1 * op2);
        public static readonly Operator Divide = new Operator("/", 1, Associativity.Left, (op1, op2) => op1 / op2);

        public static readonly Operator Power = new Operator("^", 2, Associativity.Right,
            (op1, op2) => Math.Pow(op1, op2));

        public static readonly Token LeftBracket = "(";
        public static readonly Token RightBracket = ")";
        public static readonly Token Assignment = "=";

        public static Token Of(string token)
        {
            switch (token)
            {
                case "+":
                    return Add;
                case "-":
                    return Subtract;
                case "*":
                    return Multiply;
                case "/":
                    return Divide;
                case "^":
                    return Power;
                case "(":
                    return LeftBracket;
                case ")":
                    return RightBracket;
                case "=":
                    return Assignment;
                default:
                    return new Token(token);
            }
        }
    }
}