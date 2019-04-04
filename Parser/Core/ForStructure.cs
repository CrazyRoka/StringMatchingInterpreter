using System.Collections.Generic;

namespace SyntaxAnalysis.Core
{
    public class ForStructure : IStructure
    {
        private Statement _initialize;
        private Statement _condition;
        private Statement _increment;
        private IEnumerable<Statement> _body;
        public ForStructure(Statement initialize, Statement condition, Statement increment, IEnumerable<Statement> body)
        {
            _initialize = initialize;
            _condition = condition;
            _increment = increment;
            _body = body;
        }

        public void Execute()
        {
            throw new System.NotImplementedException();
        }
    }
}
