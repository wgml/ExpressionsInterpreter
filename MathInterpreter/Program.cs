using System;
using System.Collections.Generic;

namespace MathInterpreter
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            while (true)
            {
                Console.Out.WriteLine("Enter expression or type exit to quit.");
                Console.Out.Write("> ");
                var line = System.Console.ReadLine();
                if (line == "exit" || line == "quit")
                    Console.Out.WriteLine("Bye...");
                try
                {
                    Console.Out.WriteLine("= " + Interpreter.Evaluate(line));
                }
                catch (Exception e)
                {
                    Console.Out.WriteLine("! Invalid expression: " + e.Message);
                }
            }

        }
    }
}