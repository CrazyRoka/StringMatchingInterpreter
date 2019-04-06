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

        //public static void Main()
        //{

        //}
    }
}
