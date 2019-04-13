using CodeTranslator.Core;
using Executor.Core;
using Lexer.Core;
using System.IO;

namespace StringMatchingInterpreter
{
    class Program
    {
        static void Main(string[] args)
        {
            string programText = File.ReadAllText("program.txt");
            var tokenizer = new Tokenizer();
            var tokens = tokenizer.Tokenize(programText);

            var translator = new Translator();
            var text = translator.Translate(tokens);

            RokaProgramExecutor.Execute(text);
        }
    }
}
