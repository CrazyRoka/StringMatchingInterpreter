using Lexer.Core;
using System.Collections.Generic;

namespace SyntaxAnalysis.Core
{
    public class Statement : IStructure
    {
        private List<DslToken> _tokens = new List<DslToken>();

        public void AddToken(DslToken token) => _tokens.Add(token);

        public void Execute()
        {
            System.Console.WriteLine("STATEMENT");
            foreach(var token in _tokens)
            {
                System.Console.WriteLine(token.TokenType.ToString());
            }
            //throw new System.NotImplementedException();
        }
    }
}
