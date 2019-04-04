namespace Lexer.Enums
{
    public enum TokenType
    {
        Invalid,
        NotDefined,
        Comma,
        OpenParenthesis,
        CloseParenthesis,
        OpenBraces,
        CloseBraces,
        If,
        For,
        Else,
        Number,
        StringValue,
        StatementTerminator,
        SequenceTerminator,
        Identifier,
        Var,
        Assign,
        Equals,
        Plus,
        Minus,
        AssignPlus,
    }
}
