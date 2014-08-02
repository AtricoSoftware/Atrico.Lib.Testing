using System;
using System.Collections.Generic;
using System.Linq;

namespace Atrico.Lib.Testing
{
	/// <summary>
	/// Generate test values
	/// </summary>
	public class RandomValueGenerator
	{
		[Flags]
		public enum CharsToInclude
		{
			Digits = 0x01,
			Uppercase = 0x02,
			LowerCase = 0x04,
			Space = 0x08,
			Symbols = 0x10,

			Letters = Uppercase | LowerCase,
			AlphaNumeric = Letters | Digits,
			All = Symbols | Digits | Uppercase | LowerCase | Space
		}

		#region chars

		public CharsToInclude DefaultCharsToInclude { get; set; }

		public uint DefaultCharListCount { get; set; }

		public char Char()
		{
			return Char(DefaultCharsToInclude);
		}

		public char Char(CharsToInclude include)
		{
			char ch;
			do
			{
				ch = (char)_random.Next(32, 128);
			} while (CharNotValid(ch, include));
			return ch;
		}

		private bool CharNotValid(char ch, CharsToInclude include)
		{
			if (ch == ' ')
				return !include.Contains(CharsToInclude.Space);
			if ('0' <= ch && ch <= '9')
				return !include.Contains(CharsToInclude.Digits);
			if ('a' <= ch && ch <= 'z')
				return !include.Contains(CharsToInclude.LowerCase);
			if ('A' <= ch && ch <= 'Z')
				return !include.Contains(CharsToInclude.Uppercase);
			return !include.Contains(CharsToInclude.Symbols);
		}

		public char[] Chars()
		{
			return Chars(DefaultCharsToInclude, DefaultCharListCount);
		}

		public char[] Chars(uint count)
		{
			return Chars(DefaultCharsToInclude, count);
		}

		public char[] Chars(CharsToInclude include)
		{
			return Chars(include, DefaultCharListCount);
		}

		public char[] Chars(CharsToInclude include, uint count)
		{
			var chars = new char[count];
			for (uint i = 0; i < count; ++i)
			{
				chars[i] = Char(include);
			}
			return chars;
		}

		#endregion chars

		#region strings

		public uint DefaultStringLength { get; set; }

		public uint DefaultStringListCount { get; set; }

		public string String()
		{
			return String(DefaultCharsToInclude, DefaultStringLength);
		}

		public string String(CharsToInclude include)
		{
			return String(include, DefaultStringLength);
		}

		public string String(uint length)
		{
			return String(DefaultCharsToInclude, length);
		}

		public string String(CharsToInclude include, uint length)
		{
			return new string(Chars(include, length));
		}

		public string[] Strings()
		{
			return Strings(DefaultCharsToInclude, DefaultStringListCount, DefaultStringLength);
		}

		public string[] Strings(CharsToInclude include)
		{
			return Strings(include, DefaultStringListCount, DefaultStringLength);
		}

		public string[] Strings(uint count)
		{
			return Strings(DefaultCharsToInclude, count, DefaultStringLength);
		}

		public string[] Strings(CharsToInclude include, uint count)
		{
			return Strings(include, count, DefaultStringLength);
		}

		public string[] Strings(uint count, uint length)
		{
			return Strings(DefaultCharsToInclude, count, length);
		}

		public string[] Strings(CharsToInclude include, uint count, uint length)
		{
			var strings = new string[count];
			for (uint i = 0; i < count; ++i)
			{
				strings[i] = String(include, length);
			}
			return strings;
		}

		public string[] UniqueStrings()
		{
			return UniqueStrings(DefaultCharsToInclude, DefaultStringListCount, DefaultStringLength);
		}

		public string[] UniqueStrings(CharsToInclude include)
		{
			return UniqueStrings(include, DefaultStringListCount, DefaultStringLength);
		}

		public string[] UniqueStrings(uint count)
		{
			return UniqueStrings(DefaultCharsToInclude, count, DefaultStringLength);
		}

		public string[] UniqueStrings(CharsToInclude include, uint count)
		{
			return UniqueStrings(include, count, DefaultStringLength);
		}

		public string[] UniqueStrings(uint count, uint length)
		{
			return UniqueStrings(DefaultCharsToInclude, count, length);
		}

		public string[] UniqueStrings(CharsToInclude include, uint count, uint length)
		{
			var set = new HashSet<string>(Strings(include, count, length));

			// Fill removed duplicates
			while (set.Count < count)
			{
				set.UnionWith(Strings(include, (uint)(count-set.Count), length));
			}
			return set.ToArray();
		}

		#endregion strings

		#region objects

		private enum ObjectTypes
		{
			Int,
			Double,
			Char,
			String
		}

		private static readonly IList<ObjectTypes> _objectTypeValues;

		public object Object()
		{
			// TODO - Implement better
			object obj = null;
			switch (_objectTypeValues[_random.Next(_objectTypeValues.Count)])
			{
				case ObjectTypes.Int:
					obj = _random.Next();
					break;

				case ObjectTypes.Double:
					obj = _random.NextDouble();
					break;

				case ObjectTypes.Char:
					obj = Char();
					break;

				case ObjectTypes.String:
					obj = String();
					break;
			}
			return obj;
		}

		#endregion objects

		public int Integer()
		{
			// TODO - Test
			return _random.Next();
		}

		public double Double()
		{
			// TODO - Test
			return _random.NextDouble();
		}

		public T Value<T>()
		{
			var map = new Dictionary<Type, Func<object>>
			          {
				          {typeof(char), () => Char()},
				          {typeof(string), String},
				          {typeof(int), () => Integer()},
				          {typeof(double), () => Double()}
			          };
			return (T)(map.ContainsKey(typeof(T)) ? map[typeof(T)].Invoke() : default(T));
		}

		public IEnumerable<T> Values<T>(int count)
		{
			var values = new List<T>(count);
			for (int i = 0; i < count; ++i)
			{
				values.Add(Value<T>());
			}
			return values;
		}

		public IEnumerable<T> UniqueValues<T>(int count)
		{
			var set = new HashSet<T>(Values<T>(count));

			// Fill removed duplicates
			while (set.Count < count)
			{
				set.UnionWith(Values<T>(count-set.Count));
			}
			return set.ToArray();
		}

		public RandomValueGenerator()
		{
			DefaultCharsToInclude = CharsToInclude.All;
			DefaultCharListCount = 100;
			DefaultStringLength = 5;
			DefaultStringListCount = 100;
		}

		static RandomValueGenerator()
		{
			_objectTypeValues = new List<ObjectTypes>();
			foreach (var objType in Enum.GetValues(typeof(ObjectTypes)))
			{
				_objectTypeValues.Add((ObjectTypes)objType);
			}
		}

		private readonly Random _random = new Random();
	}

	internal static class CharsToIncludeHelper
	{
		public static bool Contains(this RandomValueGenerator.CharsToInclude flags,
			RandomValueGenerator.CharsToInclude test)
		{
			return (flags & test) != 0;
		}
	}
}