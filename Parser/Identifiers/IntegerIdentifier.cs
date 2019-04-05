namespace SyntaxAnalysis.Identifiers
{
    public class IntegerIdentifier : IIdentifier
    {
        private int _value;

        public IntegerIdentifier(int value)
        {
            _value = value;
        }

        public string Value
        {
            get => _value + "";
            set => _value = int.Parse(value);
        }
    }
}
