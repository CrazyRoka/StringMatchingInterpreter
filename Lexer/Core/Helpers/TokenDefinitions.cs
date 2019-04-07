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
            tokenDefinitions.Add(new TokenDefinition(TokenType.CharCast, @"^\(char\)"));
            tokenDefinitions.Add(new TokenDefinition(TokenType.RegexCast, @"^\(Regex\)"));
            tokenDefinitions.Add(new TokenDefinition(TokenType.Regex, @"^`.*?`"));
            tokenDefinitions.Add(new TokenDefinition(TokenType.OpenParenthesis, @"^\("));
            tokenDefinitions.Add(new TokenDefinition(TokenType.CloseParenthesis, @"^\)"));
            tokenDefinitions.Add(new TokenDefinition(TokenType.OpenBraces, @"^{"));
            tokenDefinitions.Add(new TokenDefinition(TokenType.CloseBraces, @"^}"));
            tokenDefinitions.Add(new TokenDefinition(TokenType.StringValue, "^\\\"[^\\\"]*\\\""));
            tokenDefinitions.Add(new TokenDefinition(TokenType.Number, @"^\d+"));
            tokenDefinitions.Add(new TokenDefinition(TokenType.If, @"^if(?=\b)"));
            tokenDefinitions.Add(new TokenDefinition(TokenType.For, @"^for(?=\b)"));
            tokenDefinitions.Add(new TokenDefinition(TokenType.Else, @"^else(?=\b)"));
            tokenDefinitions.Add(new TokenDefinition(TokenType.StatementTerminator, @"^;"));
            tokenDefinitions.Add(new TokenDefinition(TokenType.Var, @"^var(?=\b)"));
            tokenDefinitions.Add(new TokenDefinition(TokenType.Identifier, @"^[a-zA-Z_][a-zA-Z0-9_]*"));
            tokenDefinitions.Add(new TokenDefinition(TokenType.AssignPlus, @"^\+="));
            tokenDefinitions.Add(new TokenDefinition(TokenType.Equals, @"^=="));
            tokenDefinitions.Add(new TokenDefinition(TokenType.Assign, @"^="));
            tokenDefinitions.Add(new TokenDefinition(TokenType.Plus, @"^\+"));
            tokenDefinitions.Add(new TokenDefinition(TokenType.Minus, @"^\-"));
            tokenDefinitions.Add(new TokenDefinition(TokenType.Bigger, @"^>"));
            tokenDefinitions.Add(new TokenDefinition(TokenType.BiggerEquals, @"^>="));
            tokenDefinitions.Add(new TokenDefinition(TokenType.Smaller, @"^<"));
            tokenDefinitions.Add(new TokenDefinition(TokenType.SmallerEquals, @"^<="));
            tokenDefinitions.Add(new TokenDefinition(TokenType.CharSet, @"^\[.*?\]"));
            tokenDefinitions.Add(new TokenDefinition(TokenType.Char, @"^'.'"));

            return tokenDefinitions;
        }
    }
}
