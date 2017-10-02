using System;
using System.Collections.Generic;
using System.Linq;
using MathInterpreter.Tokens;

namespace MathInterpreter
{
    public class Interpreter
    {
        private Dictionary<Token, Variable> _variables = new Dictionary<Token, Variable>();

        public double Evaluate(string tokenString)
        {
            tokenString = Spaceify(tokenString);

            Variable variable;
            var tokens = Tokenize(tokenString.Split(' ')
                    .AsEnumerable()
                    .Select((s, i) => s.Trim())
                    .Where(s => s.Length > 0)
                    .ToArray(),
                out variable);
            var result = EvaluatePostFix(tokens, variable);
            if (variable != null)
                _variables[variable.Token] = variable;
            return result;
        }

        private static bool IsNumber(string str)
        {
            double ignored;
            return double.TryParse(str, out ignored);
        }

        private static bool IsIdentifier(string str)
        {
            if (!char.IsLetter(str[0]))
                return false;

            for (int i = 1; i < str.Length; i++)
                if (!char.IsLetterOrDigit(str[i]))
                    return false;
            return true;
        }

        private IEnumerable<IToken> Tokenize(string[] tokens, out Variable output)
        {
            var outputQueue = new Queue<IToken>();
            var operatorsStack = new Stack<Token>();

            var firstToken = true;
            output = null;

            for (int i = 0; i < tokens.Length; i++)
            {
                var token = tokens[i];
                if (IsNumber(token))
                    HandleNumber(token, outputQueue);
                else if (IsIdentifier(token))
                    HandleIdentifier(token, outputQueue, i == 0 && IsAsignment(tokens));
                else
                    HandleOperator(token, outputQueue, operatorsStack, ref output);
                firstToken = false;
            }
            while (operatorsStack.Count > 0)
                outputQueue.Enqueue(operatorsStack.Pop());
            return outputQueue;
        }

        private static void HandleNumber(string token, Queue<IToken> outputQueue)
        {
            outputQueue.Enqueue(new Value(token));
        }

        private void HandleIdentifier(string token, Queue<IToken> outputQueue, bool isAsignment)
        {
            Variable variable;
            _variables.TryGetValue(token, out variable);
            if (!isAsignment && variable == null)
                throw new ArgumentException("Unknown identifier: " + token);
            else if (isAsignment && variable == null)
                outputQueue.Enqueue(new Variable(token, 0));
            else
                outputQueue.Enqueue(_variables[token]);
        }

        private static void HandleOperator(string token, Queue<IToken> outputQueue, Stack<Token> operatorsStack,
            ref Variable output)
        {
            var op = Tokens.Tokens.Of(token);

            if (op == Tokens.Tokens.Assignment)
            {
                if (outputQueue.Count != 1)
                    throw new ArgumentException("Bad assignment expression");
                output = outputQueue.Dequeue() as Variable;
            }
            else if (op == Tokens.Tokens.LeftBracket)
            {
                operatorsStack.Push(op);
            }
            else if (op == Tokens.Tokens.RightBracket)
            {
                while (operatorsStack.Peek() != Tokens.Tokens.LeftBracket)
                    outputQueue.Enqueue(operatorsStack.Pop());
                operatorsStack.Pop(); // remove LeftBracket also
            }
            else // math operator
            {
                while (operatorsStack.Count > 0)
                {
                    var stackToken = operatorsStack.Peek();
                    var stackOp = stackToken as Operator;

                    if (stackOp != null && (stackOp.Precedent(op as Operator) && stackOp.LeftAssociative()))
                        outputQueue.Enqueue(operatorsStack.Pop());
                    else
                        break;
                }
                operatorsStack.Push(op);
            }
        }

        private static bool IsAsignment(string[] tokens)
        {
            return tokens.Length >= 2 && Tokens.Tokens.Of(tokens[1]) == Tokens.Tokens.Assignment;
        }

        private static Value EvaluatePostFix(IEnumerable<IToken> tokens, Value variable)
        {
            var stack = new Stack<Value>();
            foreach (var token in tokens)
            {
                if (token is Operator)
                {
                    var op2 = stack.Pop();
                    var op1 = stack.Pop();
                    var op = token as Operator;
                    stack.Push(op.Evaluate(op1, op2));
                }
                else
                {
                    stack.Push(token as Value);
                }
            }
            if (stack.Count != 1)
                throw new ArgumentException("Invalid token list.");
            var result = stack.Pop();
            if (variable == null) return result;

            variable.Val = result;
            return variable;
        }

        private static string Spaceify(string str)
        {
            var result = "";

            foreach (var c in str)
            {
                if (!char.IsLetterOrDigit(c) && c != ' ' && c != '.')
                    result += " " + c + " ";
                else
                    result += c;
            }
            return result;
        }
    }
}