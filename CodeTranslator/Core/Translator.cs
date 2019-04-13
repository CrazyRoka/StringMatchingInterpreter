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
                using System.Text.RegularExpressions;

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

                        public override string ToString()
                        {
                            return '[' + string.Join(',', new List<char>(_charSet)) + ']';
                        }

                    }

                    public class RokaRegex
                    {
                        private Regex _regex;
                        public RokaRegex(string pattern, bool escape = true)
                        {
                            if (escape)
                            {
                                pattern = Regex.Escape(pattern);
                            }
                            _regex = new Regex(pattern);
                        }

                        public void Add(string value) => _regex = new Regex(_regex.ToString() + Regex.Escape(value));

                        public void Add(CharSet set) => _regex = new Regex(_regex.ToString() + set.ToString().Replace(',', '\0'));

                        public static RokaRegex operator +(RokaRegex regex, CharSet set)
                        {
                            var result = new RokaRegex(regex.ToString(), false);
                            result.Add(set);
                            return result;
                        }

                        public static RokaRegex operator +(RokaRegex regex, string value)
                        {
                            var result = new RokaRegex(regex.ToString(), false);
                            result.Add(value);
                            return result;
                        }

                        public void FindMatch(string value)
                        {
                            var matches = _regex.Matches(value);
                            foreach(Match match in matches)
                            {
                                Console.WriteLine(value);
                                Console.WriteLine(new string(' ', match.Index) + new string('^', match.Length));
                                Console.WriteLine(new string(' ', match.Index) + match.Value);
                            }
                        }

                        public override string ToString() => _regex.ToString();
                    }

                    public class RokaProgram
                    {
                        private static int Length1(string value) => value.Length;

                        private static void Print1(object value) => Console.WriteLine(value);

                        private static void Find1(RokaRegex regex, string value) => regex.FindMatch(value);

                        public static void Main()
                        {
");
            foreach (var token in tokens)
            {
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
                    if (token.Value == "length")
                    {
                        text.Append("Length1");
                    }
                    else if (token.Value == "print")
                    {
                        text.Append("Print1");
                    }
                    else if(token.Value == "find")
                    {
                        text.Append("Find1");
                    }
                    else
                    {
                        text.Append($" {token.Value}1 ");
                    }
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
                    text.Append(" {\n");
                    break;
                case TokenType.OpenParenthesis:
                    text.Append(" ( ");
                    break;
                case TokenType.Plus:
                    text.Append(" + ");
                    break;
                case TokenType.StatementTerminator:
                    text.Append(" ;\n");
                    break;
                case TokenType.StringValue:
                    text.Append(token.Value);
                    break;
                case TokenType.Var:
                    text.Append(" var ");
                    break;
                case TokenType.SequenceTerminator:
                    text.Append(" }\n}\n}");
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
                case TokenType.CharSet:
                    text.Append(token.Value.Replace("[", " new CharSet() { ").Replace("]", " } "));
                    break;
                case TokenType.Char:
                    text.Append(token.Value);
                    break;
                case TokenType.CharCast:
                    text.Append(token.Value);
                    break;
                case TokenType.Regex:
                    text.Append($"new RokaRegex(\"{token.Value.Substring(1, token.Value.Length - 2)}\")");
                    break;
                case TokenType.RegexCast:
                    text.Append("new RokaRegex(\"\") + ");
                    break;
                default:
                    throw new DslParserException("Unknown token type");
            }
        }
    }
}
