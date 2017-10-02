using System;

namespace MathInterpreter
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var interpreter = new Interpreter();
            Console.Out.WriteLine("Enter expression or type exit or quit.");

            while (true)
            {
                Console.Out.Write("> ");
                var line = System.Console.ReadLine();
                if (line == "exit" || line == "quit")
                {
                    Console.Out.WriteLine("Bye...");
                    break;
                }
                try
                {
                    Console.Out.WriteLine("= " + interpreter.Evaluate(line));
                }
                catch (Exception e)
                {
                    Console.Out.WriteLine("! Invalid expression: " + e.Message);
                }
            }
        }
    }
}