using Lexer.Core;
using Lexer.Core.Exceptions;
using Lexer.Enums;
using System.Collections.Generic;

namespace SyntaxAnalysis.Core
{
    public class Analyser
    {
        private List<DslToken> _tokens;
        private int _index;
        public IEnumerable<IStructure> Parse(List<DslToken> tokens)
        {
            var result = new List<IStructure>();
            _tokens = tokens;
            _index = 0;
            while(_index < tokens.Count)
            {
                if(tokens[_index].TokenType == TokenType.If)
                {
                    GetToken(TokenType.If);
                    GetToken(TokenType.OpenParenthesis);
                    var condition = new Statement();
                    do
                    {
                        var current = _tokens[_index++];
                        if (current.TokenType == TokenType.StatementTerminator || current.TokenType == TokenType.SequenceTerminator)
                        {
                            throw new DslParserException($"Invalid 'IF' structure. Not expected {current.TokenType.ToString()}");
                        }
                        if(current.TokenType == TokenType.CloseParenthesis)
                        {
                            break;
                        }
                        condition.AddToken(current);
                    } while (true);
                    Statement[] statements;
                    if (_tokens[_index].TokenType == TokenType.OpenBraces)
                    {
                        statements = ReadBlock();
                    }
                    else
                    {
                        statements = new Statement[] { ReadStatement() };
                    }
                    var structure = new IfStructure(condition, statements);
                    result.Add(structure);
                }
                //else if(tokens[_index].TokenType == TokenType.For)
                //{

                //}
                //else
                //{

                //}
                //_index++;
                else
                {
                    var current = _tokens[_index];
                    if(current.TokenType == TokenType.SequenceTerminator)
                    {
                        break;
                    }
                    if(current.TokenType == TokenType.OpenBraces)
                    {
                        result.AddRange(ReadBlock());
                    }
                    else
                    {
                        result.Add(ReadStatement());
                    }
                }
            }
            return result;
        }

        private DslToken GetToken(TokenType type)
        {
            if (_tokens[_index].TokenType != type)
            {
                throw new DslParserException($"Excpected {type.ToString()}, but got {_tokens[_index].ToString()}");
            }
            return _tokens[_index];
        } 

        private Statement ReadStatement()
        {
            var statement = new Statement();
            do
            {
                var current = _tokens[_index++];
                if(current.TokenType == TokenType.SequenceTerminator)
                {
                    throw new DslParserException("Unexpected end of input");
                }
                if (current.TokenType == TokenType.StatementTerminator)
                {
                    break;
                }
                statement.AddToken(current);
            } while (true);
            return statement;
        }

        private Statement[] ReadBlock()
        {
            var list = new List<Statement>();
            GetToken(TokenType.OpenBraces);
            do
            {
                var current = _tokens[index];
                if(current.TokenType == TokenType.SequenceTerminator)
                {
                    throw new DslParserException("Unexpected end of input");
                }
                if(current.TokenType == TokenType.CloseBraces)
                {
                    break;
                }
                list.Add(ReadStatement());
            } while (true);
            GetToken(TokenType.CloseBraces);
            return list.ToArray();
        }
    }
}
