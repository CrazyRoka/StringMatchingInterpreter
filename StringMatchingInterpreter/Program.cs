using Lexer.Core;
using System;
using System.IO;

namespace StringMatchingInterpreter
{
    class Program
    {
        static void Main(string[] args)
        {
            string programText = File.ReadAllText("test.txt");
            var tokenizer = new Tokenizer();
            var tokens = tokenizer.Tokenize(programText);
            foreach (var token in tokens)
            {
                Console.WriteLine($"{token.TokenType.ToString()} {token.Value}");
            }
        }
    }
}
