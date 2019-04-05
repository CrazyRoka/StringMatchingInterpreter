using System.Collections.Generic;
using System.Linq;

namespace SyntaxAnalysis.Identifiers
{
    public class CharSetIdentifier : IIdentifier
    {
        private ISet<char> _charSet = new HashSet<char>();

        public string Value
        {
            get => string.Join("", _charSet.ToArray());
            set => _charSet = new HashSet<char>(value);
        }
    }
}
