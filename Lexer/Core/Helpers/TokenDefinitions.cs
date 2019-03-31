using Lexer.Enums;
using System.Collections.Generic;

namespace Lexer.Core.Helpers
{
    static class TokenDefinitions
    {
        public static IEnumerable<TokenDefinition> GetTokenDefinitions()
        {
            var tokenDefinitions = new List<TokenDefinition>();

            tokenDefinitions.Add(new TokenDefinition(TokenType.Comma, "^,"));
            tokenDefinitions.Add(new TokenDefinition(TokenType.OpenParenthesis, @"^\("));
            tokenDefinitions.Add(new TokenDefinition(TokenType.CloseParenthesis, @"^\)"));
            tokenDefinitions.Add(new TokenDefinition(TokenType.StringValue, "^\\\"[^\\\"]*\\\""));
            tokenDefinitions.Add(new TokenDefinition(TokenType.Number, @"^\d+"));
            tokenDefinitions.Add(new TokenDefinition(TokenType.If, @"^if"));
            tokenDefinitions.Add(new TokenDefinition(TokenType.For, @"^for"));
            tokenDefinitions.Add(new TokenDefinition(TokenType.Else, @"^else"));
            tokenDefinitions.Add(new TokenDefinition(TokenType.StatementTerminator, @"^;"));
            return tokenDefinitions;
        }
    }
}
