using Lexer.Core;
using System.Collections.Generic;

namespace SyntaxAnalysis.Core
{
    public class Statement : IStructure
    {
        private List<DslToken> _tokens;

        public void AddToken(DslToken token) => _tokens.Add(token);

        public void Execute()
        {
            throw new System.NotImplementedException();
        }
    }
}
