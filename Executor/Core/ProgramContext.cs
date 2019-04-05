using SyntaxAnalysis.Core;
using SyntaxAnalysis.Identifiers;
using System.Collections.Generic;

namespace Executor.Core
{
    public class ProgramContext : IContext
    {
        private IDictionary<string, IIdentifier> context = new Dictionary<string, IIdentifier>();
        public void Execute(IEnumerable<IStructure> program)
        {
            foreach(var code in program)
            {
                code.Execute(this);
            }
        }

        public void SetIdentifier(string name, IIdentifier identifier) => context[name] = identifier;

        public IIdentifier GetIdentifier(string name) => context[name];
    }
}
