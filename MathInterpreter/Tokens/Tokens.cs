using System;

namespace MathInterpreter.Tokens
{
    public abstract class Tokens
    {
        public static readonly Operator Add = new Operator("+", 0, Associativity.Left, (op1, op2) => op1 + op2);
        public static Operator Subtract = new Operator("-", 0, Associativity.Left, (op1, op2) => op1 - op2);
        public static Operator Multiply = new Operator("*", 1, Associativity.Left, (op1, op2) => op1 * op2);
        public static Operator Divide = new Operator("/", 1, Associativity.Left, (op1, op2) => op1 / op2);
        public static Operator Power = new Operator("^", 2, Associativity.Right, (op1, op2) => Math.Pow(op1, op2));

        public static Token LeftBracket = "(";
        public static Token RightBracket = ")";

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
                default:
                    throw new ArgumentException("Unknown token: " + token);
            }
        }
    }
}