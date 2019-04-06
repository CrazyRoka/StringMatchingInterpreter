using Lexer.Core;
using Lexer.Core.Exceptions;
using Lexer.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeTranslator.Core
{
    public class Translator
    {
        public string Translate(List<DslToken> tokens)
        {
            StringBuilder text = new StringBuilder(@"
                using System;
                using System.Collections;
                using System.Collections.Generic;

                namespace RokaProgramming
                {
                    public class CharSet : IEnumerable<char>
                    {
                        private HashSet<char> _charSet = new HashSet<char>();
                        public void Add(char value)
                        {
                            _charSet.Add(value);
                        }

                        public void Remove(char value)
                        {
                            _charSet.Remove(value);
                        }

                        public static CharSet operator +(CharSet first, char second)
                        {
                            var result = new CharSet()
                            {
                                _charSet = new HashSet<char>(first._charSet)
                            };
                            result.Add(second);

                            return result;
                        }

                        public static CharSet operator -(CharSet first, char second)
                        {
                            var result = new CharSet()
                            {
                                _charSet = new HashSet<char>(first._charSet)
                            };
                            result.Remove(second);

                            return result;
                        }

                        public static CharSet operator +(CharSet first, CharSet second)
                        {
                            var result = new CharSet()
                            {
                                _charSet = new HashSet<char>(first._charSet)
                            };
                            result._charSet.UnionWith(second._charSet);

                            return result;
                        }

                        public static CharSet operator -(CharSet first, CharSet second)
                        {
                            var result = new CharSet()
                            {
                                _charSet = new HashSet<char>(first._charSet)
                            };
                            result._charSet.ExceptWith(second._charSet);

                            return result;
                        }

                        public IEnumerator<char> GetEnumerator() => _charSet.GetEnumerator();

                        IEnumerator IEnumerable.GetEnumerator() => _charSet.GetEnumerator();
                    }

                    public class RokaProgram
                    {
                        private static int Length1(string value) => value.Length;

                        private static void Print1(string value) => Console.WriteLine(value);

                        public static void Main()
                        {

            ");
            foreach (var token in tokens)
            {
                Console.WriteLine($"{token.Value} {token.TokenType}");
                TransformToken(text, token);
            }
            return text.ToString();
        }

        private void TransformToken(StringBuilder text, DslToken token)
        {
            switch (token.TokenType)
            {
                case TokenType.Assign:
                    text.Append(" = ");
                    break;
                case TokenType.AssignPlus:
                    text.Append(" += ");
                    break;
                case TokenType.CloseBraces:
                    text.Append(" } ");
                    break;
                case TokenType.CloseParenthesis:
                    text.Append(" ) ");
                    break;
                case TokenType.Comma:
                    text.Append(" , ");
                    break;
                case TokenType.Else:
                    text.Append(" else ");
                    break;
                case TokenType.Equals:
                    text.Append(" == ");
                    break;
                case TokenType.For:
                    text.Append(" for ");
                    break;
                case TokenType.Identifier:
                    text.Append($" {token.Value}1 ");
                    break;
                case TokenType.If:
                    text.Append(" if ");
                    break;
                case TokenType.Minus:
                    text.Append(" - ");
                    break;
                case TokenType.Number:
                    text.Append(token.Value);
                    break;
                case TokenType.OpenBraces:
                    text.Append(" { ");
                    break;
                case TokenType.OpenParenthesis:
                    text.Append(" ( ");
                    break;
                case TokenType.Plus:
                    text.Append(" + ");
                    break;
                case TokenType.StatementTerminator:
                    text.Append(" ; ");
                    break;
                case TokenType.StringValue:
                    text.Append(token.Value);
                    break;
                case TokenType.Var:
                    text.Append(" var ");
                    break;
                case TokenType.SequenceTerminator:
                    text.Append(@" } } }");
                    break;
                case TokenType.Bigger:
                    text.Append(" > ");
                    break;
                case TokenType.BiggerEquals:
                    text.Append(" >= ");
                    break;
                case TokenType.Smaller:
                    text.Append(" < ");
                    break;
                case TokenType.SmallerEquals:
                    text.Append(" <= ");
                    break;
                default:
                    throw new DslParserException("Unknown token type");
            }
        }
    }
}
