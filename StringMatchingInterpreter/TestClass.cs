using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RokaProgramming
{
    public class CharSet : IEnumerable<char>
    {
        private HashSet<char> _charSet = new HashSet<char>();

        public void Add(char value) => _charSet.Add(value);

        public void Remove(char value) => _charSet.Remove(value);

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

        public override string ToString() => '[' + string.Join(',', new List<char>(_charSet)) + ']';
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

        private static void Print1(string value) => Console.WriteLine(value);

        private static void Find1(RokaRegex regex, string value) => regex.FindMatch(value);

        //public static void Main()
        //{

        //}
    }
}
