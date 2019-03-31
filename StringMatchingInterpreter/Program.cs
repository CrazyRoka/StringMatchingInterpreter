using Lexer.Core;
using System;

namespace StringMatchingInterpreter
{
    class Program
    {
        static void Main(string[] args)
        {
            string programText = "\"Hello" + Environment.NewLine + "World\"";
            var tokenizer = new Tokenizer();
            var tokens = tokenizer.Tokenize(programText);
            foreach (var token in tokens)
            {
                Console.WriteLine($"{token.TokenType.ToString()} {token.Value}");
            }
        }
    }
}
