using Lexer.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeTranslator.Core
{
    public class Translator
    {
        public string Translate(List<DslToken> tokens)
        {
            var text = @"
                using System;
                namespace RokaProgramming
                {
                }
            ";
            foreach (var token in tokens)
            {
                Console.WriteLine($"{token.Value} {token.TokenType}");
            }
            return null;
        }
    }
}
