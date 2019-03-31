using System;
using System.Collections.Generic;
using System.Text;

namespace Lexer.Core.Exceptions
{
    public class DslParserException : Exception
    {
        public DslParserException() : base() { }
        public DslParserException(string message) : base(message) { }
    }
}
