using System.Collections.Generic;

namespace SyntaxAnalysis.Core
{
    public class IfStructure : IStructure
    {
        private Statement _condition;
        private IEnumerable<IStructure> _body;
        public IfStructure(Statement condition, IEnumerable<Statement> body)
        {
            _condition = condition;
            _body = body;
        }

        public void Execute()
        {
            System.Console.WriteLine("IF: ");
            System.Console.WriteLine("CONDITION:");
            _condition.Execute();
            System.Console.WriteLine("END CONDITION");
            foreach(var structure in _body)
            {
                structure.Execute();
            }
            //throw new System.NotImplementedException();
            System.Console.WriteLine("EndIF");
        }
    }
}
