using System.Text.RegularExpressions;
using Lexer.Enums;

namespace Lexer.Core
{
    class TokenDefinition
    {
        private Regex _regex;
        private readonly TokenType _tokenType;
        public TokenDefinition(TokenType tokenType, Regex regex)
        {
            _regex = regex;
            _tokenType = tokenType;
        }

        public TokenDefinition(TokenType tokenType, string regexPattern)
        {
            _regex = new Regex(regexPattern, RegexOptions.IgnoreCase);
            _tokenType = tokenType;
        }

        public TokenMatch Match(string inputString)
        {
            var match = _regex.Match(inputString);
            if (match.Success)
            {
                string remainingText = inputString.Substring(match.Length);
                return new TokenMatch
                {
                    IsMatch = true,
                    RemainingText = remainingText,
                    TokenType = _tokenType,
                    Value = match.Value
                };
            }
            else
            {
                return new TokenMatch { IsMatch = false };
            }
        }
    }
}
