using Lexer.Core;
using Lexer.Core.Exceptions;
using Lexer.Enums;
using SyntaxAnalysis.Identifiers;
using System.Collections.Generic;

namespace SyntaxAnalysis.Core
{
    public class Statement : IStructure
    {
        private List<DslToken> _tokens = new List<DslToken>();

        public void AddToken(DslToken token) => _tokens.Add(token);

        public void Execute(IContext context)
        {
            if (_tokens.Count == 0) return;

            if(_tokens[0].TokenType == TokenType.Var)
            {
                var name = _tokens[1].Value;
                var value = ParseSubcode(context, _tokens.GetRange(3, _tokens.Count - 3));
                context.SetIdentifier(name, value);
            }
            else if(_tokens[0].TokenType == TokenType.Identifier)
            {
                ParseSubcode(context, _tokens);
            }
            else
            {
                throw new DslParserException("Invalid start of statement");
            }
        }

        public IIdentifier ParseSubcode(IContext context, List<DslToken> code)
        {
            if(code[0].Value.ToLower() == "length")
            {

            }
            else if(code[0].Value.ToLower() == "print")
            {
                if(code[1].TokenType != TokenType.OpenParenthesis || code[code.Count - 1].TokenType != TokenType.CloseParenthesis)
                {
                    throw new DslParserException("Invalid function call");
                }
                PrintFunction(ParseSubcode(context, code.GetRange(2, code.Count - 3)));
            }
            else
            {

            }
        }

        private IntegerIdentifier LengthFunction(IIdentifier identifier)
        {
            var strValue = (identifier as StringIdentifier)?.Value;
            if (strValue != null)
            {
                return new IntegerIdentifier(strValue.Length);
            }
            throw new DslParserException($"Expected string value, but got {identifier.GetType().Name}");
        }

        private void PrintFunction(IIdentifier identifier)
        {
            System.Console.WriteLine(identifier.Value);
        }
    }
}
