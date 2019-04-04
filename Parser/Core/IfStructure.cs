using System.Collections.Generic;

namespace SyntaxAnalysis.Core
{
    public class IfStructure : IStructure
    {
        private Statement _condition;
        private IEnumerable<Statement> _body;
        public IfStructure(Statement condition, IEnumerable<Statement> body)
        {
            _condition = condition;
            _body = body;
        }

        public void Execute()
        {
            throw new System.NotImplementedException();
        }
    }
}
