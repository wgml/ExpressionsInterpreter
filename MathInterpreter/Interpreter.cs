﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using MathInterpreter.Tokens;

namespace MathInterpreter
{
    public class Interpreter
    {
        
        public static double Evaluate(string tokenString)
        {
            tokenString = Spaceify(tokenString);

            return EvaluatePostFix(
                Tokenize(tokenString.Split(' ')
                    .AsEnumerable()
                    .Select((s, i) => s.Trim())
                    .Where(s => s.Length > 0)
                    .ToArray()
                )
            );
        }

        private static bool IsNumber(string str)
        {
            double ignored;
            return double.TryParse(str, out ignored);
        }

        private static IEnumerable<IToken> Tokenize(IEnumerable<string> tokens)
        {
            var outputQueue = new Queue<IToken>();
            var operatorsStack = new Stack<Operator>();
            
            foreach (var token in tokens)
            {
                if (IsNumber(token))
                {
                    outputQueue.Enqueue(new Number(token));
                }
                else
                {
                    var op = new Operator(token);
                    if (op == Operator.LeftBracket)
                    {
                        operatorsStack.Push(op);
                    }
                    else if (op == Operator.RightBracket)
                    {
                        while (operatorsStack.Peek() != Operator.LeftBracket)
                        {
                            outputQueue.Enqueue(operatorsStack.Pop());
                        }
                        operatorsStack.Pop(); // remove LeftBracket also
                    }
                    else // math operator
                    {
                        while (operatorsStack.Count > 0)
                        {
                            var stackOp = operatorsStack.Peek();
                            if (stackOp.Precedent(op) && stackOp.LeftAssociative())
                            {
                                outputQueue.Enqueue(operatorsStack.Pop());
                            }
                            else
                            {
                                break;
                            }
                        }
                        operatorsStack.Push(op);
                    }
                }
            }
            while (operatorsStack.Count > 0)
                outputQueue.Enqueue(operatorsStack.Pop());
            return outputQueue;
        }

        private static double EvaluatePostFix(IEnumerable<IToken> tokens)
        {
            var stack = new Stack<Number>();
            foreach (var token in tokens)
            {
                if (token is Operator)
                {
                    var op2 = stack.Pop();
                    var op1 = stack.Pop();
                    var op = (Operator) token;
                    stack.Push(op.Evaluate(op1, op2));
                }
                else
                {
                    stack.Push((Number) token);
                }
            }
            if (stack.Count != 1)
                throw new ArgumentException("Invalid token list.");
            return stack.Pop().Value;
        }

        private static string Spaceify(string str)
        {
            var result = "";

            foreach (var c in str)
            {
                if (!char.IsDigit(c) && c != ' ' && c != '.')
                    result += " " + c + " ";
                else
                    result += c;
            }
            return result;
        }
    }
}