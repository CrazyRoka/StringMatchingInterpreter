using CodeTranslator.Core;
using Lexer.Core;
using System.Collections;
using System.Collections.Generic;
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
            //foreach (var token in tokens)
            //{
            //    Console.WriteLine($"{token.TokenType.ToString()} {token.Value}");
            //}
            //var list = new Analyser().Parse(tokens);
            //foreach(var str in list)
            //{
            //    str.Execute();
            //}
            var translator = new Translator();
            var text = translator.Translate(tokens);
            System.Console.WriteLine(text);
        }
    }
}
