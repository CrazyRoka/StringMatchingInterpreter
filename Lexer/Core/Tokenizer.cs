using System.Collections.Generic;
using System.Text.RegularExpressions;
using Lexer.Core.Exceptions;
using Lexer.Core.Helpers;
using Lexer.Enums;

namespace Lexer.Core
{
    public class Tokenizer
    {
        private IEnumerable<TokenDefinition> _tokenDefinitions;

        public Tokenizer()
        {
            _tokenDefinitions = TokenDefinitions.GetTokenDefinitions();
        }

        public List<DslToken> Tokenize(string programText)
        {
            var tokens = new List<DslToken>();
            string remainingText = programText;

            while (!string.IsNullOrWhiteSpace(remainingText))
            {
                var match = FindMatch(remainingText);
                if (match.IsMatch)
                {
                    tokens.Add(new DslToken(match.TokenType, match.Value));
                    remainingText = match.RemainingText;
                }
                else
                {
                    if (IsWhitespace(remainingText))
                    {
                        remainingText = remainingText.Substring(1);
                    }
                    else
                    {
                        var invalidTokenMatch = CreateInvalidTokenMatch(remainingText);
                        tokens.Add(new DslToken(invalidTokenMatch.TokenType, invalidTokenMatch.Value));
                        remainingText = invalidTokenMatch.RemainingText;
                    }
                }
            }

            tokens.Add(new DslToken(TokenType.SequenceTerminator, string.Empty));
            return tokens;
        }

        private bool IsWhitespace(string programText)
        {
            return Regex.IsMatch(programText, @"^\s+");
        }

        private TokenMatch CreateInvalidTokenMatch(string programText)
        {
            var match = Regex.Match(programText, "(^\\S+\\s)|^\\S+");
            if (match.Success)
            {
                return new TokenMatch()
                {
                    IsMatch = true,
                    RemainingText = programText.Substring(match.Length),
                    TokenType = TokenType.Invalid,
                    Value = match.Value.Trim()
                };
            }

            throw new DslParserException("Failed to generate invalid token");
        }

        private TokenMatch FindMatch(string programText)
        {
            foreach(var tokenDefinition in _tokenDefinitions)
            {
                var match = tokenDefinition.Match(programText);
                if (match.IsMatch)
                {
                    return match;
                }
            }
            return new TokenMatch() { IsMatch = false };
        }
    }
}
