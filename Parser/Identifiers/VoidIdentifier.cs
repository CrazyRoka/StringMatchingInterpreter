using Lexer.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyntaxAnalysis.Identifiers
{
    public class VoidIdentifier : IIdentifier
    {
        public string Value
        {
            get => null;
            set => throw new DslParserException("Void can not have value");
        }
    }
}
