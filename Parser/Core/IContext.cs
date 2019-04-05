using SyntaxAnalysis.Identifiers;

namespace SyntaxAnalysis.Core
{
    public interface IContext
    {
        void SetIdentifier(string name, IIdentifier identifier);

        IIdentifier GetIdentifier(string name);
    }
}
