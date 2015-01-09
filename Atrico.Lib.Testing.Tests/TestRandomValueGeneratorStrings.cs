﻿using System;
using System.Collections.Generic;
using System.Linq;
using Atrico.Lib.Assertions;
using Atrico.Lib.Testing.NUnitAttributes;

namespace Atrico.Lib.Testing.Tests
{
	[TestFixture]
	public class TestRandomValueGeneratorStrings
	{
		#region Helpers

		private const int _length = 900;
		private const uint _count = 1000;

		[Flags]
		private enum CharTypesAllowed
		{
			Space = 0x01,
			Digit = 0x02,
			Upper = 0x04,
			Lower = 0x08,
			Symbol = 0x10
		}

		private class ValidateResults
		{
			private bool? _space { get; set; }

			private bool? _digit { get; set; }

			private bool? _upper { get; set; }

			private bool? _lower { get; set; }

			private bool? _symbl { get; set; }

			public ValidateResults(CharTypesAllowed allowed)
			{
				if ((allowed & CharTypesAllowed.Space) != 0)
					_space = false;
				if ((allowed & CharTypesAllowed.Digit) != 0)
					_digit = false;
				if ((allowed & CharTypesAllowed.Upper) != 0)
					_upper = false;
				if ((allowed & CharTypesAllowed.Lower) != 0)
					_lower = false;
				if ((allowed & CharTypesAllowed.Symbol) != 0)
					_symbl = false;
			}

			public void AddChar(char ch)
			{
				if (_space.HasValue)
					_space |= ch.IsSpace();
				else
					Assert.That(ch.IsSpace(), Is.False, "Space");
				if (_digit.HasValue)
					_digit |= ch.IsDigit();
				else
					Assert.That(ch.IsDigit(), Is.False, ch.ToString());
				if (_upper.HasValue)
					_upper |= ch.IsUppercase();
				else
					Assert.That(ch.IsUppercase(), Is.False, ch.ToString());
				if (_lower.HasValue)
					_lower |= ch.IsLowercase();
				else
					Assert.That(ch.IsLowercase(), Is.False, ch.ToString());
				if (_symbl.HasValue)
					_symbl |= ch.IsSymbol();
				else
					Assert.That(ch.IsSymbol(), Is.False, ch.ToString());
			}

			public void Validate()
			{
				if (_space.HasValue)
					Assert.That(_space.Value, Is.True, "Space");
				if (_digit.HasValue)
					Assert.That(_digit.Value, Is.True, "Digit");
				if (_upper.HasValue)
					Assert.That(_upper.Value, Is.True, "Uppercase");
				if (_lower.HasValue)
					Assert.That(_lower.Value, Is.True, "Lowercase");
				if (_symbl.HasValue)
					Assert.That(_symbl.Value, Is.True, "Symbol");
			}
		}

		private static void ValidateString(string str, CharTypesAllowed allowed)
		{
			var results = new ValidateResults(allowed);
			foreach (char ch in str)
			{
				results.AddChar(ch);
			}
			results.Validate();
		}

		private static void ValidateStrings(IEnumerable<string> list, CharTypesAllowed allowed)
		{
			var results = new ValidateResults(allowed);
			foreach (var ch in list.SelectMany(str => str))
			{
				results.AddChar(ch);
			}
			results.Validate();
		}

		#endregion Helpers

		[Test]
		public void Creation()
		{
			var obj = new RandomValueGenerator();
			Assert.That(obj, Is.Not.Null);
		}

		[TestFixture]
		public class Single
		{
			[Test]
			public void Default()
			{
				var generator = new RandomValueGenerator();
				var obj = generator.String();
				Assert.That(obj, Is.TypeOf<string>());
			}

			[Test]
			public void Length()
			{
				var generator = new RandomValueGenerator();
				for (uint length = 0; length < _length; ++length)
				{
					var str = generator.String(length);
					Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(length, (uint)str.Length, length.ToString());
				}
			}

			[Test]
			public void IncludeSpaces()
			{
				var generator = new RandomValueGenerator();
				string str = generator.String(RandomValueGenerator.CharsToInclude.Space, _length);
				ValidateString(str, CharTypesAllowed.Space);
			}

			[Test]
			public void IncludeDigits()
			{
				var generator = new RandomValueGenerator();
				string str = generator.String(RandomValueGenerator.CharsToInclude.Digits, _length);
				ValidateString(str, CharTypesAllowed.Digit);
			}

			[Test]
			public void IncludeUpper()
			{
				var generator = new RandomValueGenerator();
				string str = generator.String(RandomValueGenerator.CharsToInclude.Uppercase, _length);
				ValidateString(str, CharTypesAllowed.Upper);
			}

			[Test]
			public void IncludeLower()
			{
				var generator = new RandomValueGenerator();
				string str = generator.String(RandomValueGenerator.CharsToInclude.LowerCase, _length);
				ValidateString(str, CharTypesAllowed.Lower);
			}

			[Test]
			public void IncludeSymbol()
			{
				var generator = new RandomValueGenerator();
				string str = generator.String(RandomValueGenerator.CharsToInclude.Symbols, _length);
				ValidateString(str, CharTypesAllowed.Symbol);
			}

			[Test]
			public void IncludeLetters()
			{
				var generator = new RandomValueGenerator();
				string str = generator.String(RandomValueGenerator.CharsToInclude.Letters, _length);
				ValidateString(str, CharTypesAllowed.Upper | CharTypesAllowed.Lower);
			}

			[Test]
			public void IncludeAlphanumeric()
			{
				var generator = new RandomValueGenerator();
				string str = generator.String(RandomValueGenerator.CharsToInclude.AlphaNumeric, _length);
				ValidateString(str, CharTypesAllowed.Upper | CharTypesAllowed.Lower | CharTypesAllowed.Digit);
			}

			[Test]
			public void IncludeAll()
			{
				var generator = new RandomValueGenerator();
				string str = generator.String(RandomValueGenerator.CharsToInclude.All, _length);
				ValidateString(str,
					CharTypesAllowed.Upper | CharTypesAllowed.Lower | CharTypesAllowed.Digit | CharTypesAllowed.Space | CharTypesAllowed.Symbol);
			}

			[Test]
			public void IncludeDefault()
			{
				var generator = new RandomValueGenerator();
				string str = generator.String(_length);
				ValidateString(str,
					CharTypesAllowed.Upper | CharTypesAllowed.Lower | CharTypesAllowed.Digit | CharTypesAllowed.Space | CharTypesAllowed.Symbol);
			}
		}

		[TestFixture]
		public class Lists
		{
			[Test]
			public void Default()
			{
				var generator = new RandomValueGenerator();
				var obj = generator.Strings();
				Assert.That(obj, Is.TypeOf<string[]>());
			}

			[Test]
			public void Count()
			{
				var generator = new RandomValueGenerator();
				string[] list = generator.Strings(_count);
				Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(_count, (uint)list.Length);
			}

			[Test]
			public void IncludeSpaces()
			{
				var generator = new RandomValueGenerator();
				string[] list = generator.Strings(RandomValueGenerator.CharsToInclude.Space, _count);
				Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(_count, (uint)list.Length);
				ValidateStrings(list, CharTypesAllowed.Space);
			}

			[Test]
			public void IncludeDigits()
			{
				var generator = new RandomValueGenerator();
				string[] list = generator.Strings(RandomValueGenerator.CharsToInclude.Digits, _count);
				Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(_count, (uint)list.Length);
				ValidateStrings(list, CharTypesAllowed.Digit);
			}

			[Test]
			public void IncludeUpper()
			{
				var generator = new RandomValueGenerator();
				string[] list = generator.Strings(RandomValueGenerator.CharsToInclude.Uppercase, _count);
				Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(_count, (uint)list.Length);
				ValidateStrings(list, CharTypesAllowed.Upper);
			}

			[Test]
			public void IncludeLower()
			{
				var generator = new RandomValueGenerator();
				string[] list = generator.Strings(RandomValueGenerator.CharsToInclude.LowerCase, _count);
				Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(_count, (uint)list.Length);
				ValidateStrings(list, CharTypesAllowed.Lower);
			}

			[Test]
			public void IncludeSymbol()
			{
				var generator = new RandomValueGenerator();
				string[] list = generator.Strings(RandomValueGenerator.CharsToInclude.Symbols, _count);
				Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(_count, (uint)list.Length);
				ValidateStrings(list, CharTypesAllowed.Symbol);
			}

			[Test]
			public void IncludeLetters()
			{
				var generator = new RandomValueGenerator();
				string[] list = generator.Strings(RandomValueGenerator.CharsToInclude.Letters, _count);
				Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(_count, (uint)list.Length);
				ValidateStrings(list, CharTypesAllowed.Upper | CharTypesAllowed.Lower);
			}

			[Test]
			public void IncludeAlphanumeric()
			{
				var generator = new RandomValueGenerator();
				string[] list = generator.Strings(RandomValueGenerator.CharsToInclude.AlphaNumeric, _count);
				Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(_count, (uint)list.Length);
				ValidateStrings(list, CharTypesAllowed.Upper | CharTypesAllowed.Lower | CharTypesAllowed.Digit);
			}

			[Test]
			public void IncludeAll()
			{
				var generator = new RandomValueGenerator();
				string[] list = generator.Strings(RandomValueGenerator.CharsToInclude.All, _count);
				Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(_count, (uint)list.Length);
				ValidateStrings(list,
					CharTypesAllowed.Upper | CharTypesAllowed.Lower | CharTypesAllowed.Digit | CharTypesAllowed.Symbol | CharTypesAllowed.Space);
			}

			[Test]
			public void IncludeDefault()
			{
				var generator = new RandomValueGenerator();
				string[] list = generator.Strings(_count);
				Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(_count, (uint)list.Length);
				ValidateStrings(list,
					CharTypesAllowed.Upper | CharTypesAllowed.Lower | CharTypesAllowed.Digit | CharTypesAllowed.Symbol | CharTypesAllowed.Space);
			}
		}

		[TestFixture]
		public class Defaults
		{
			[Test]
			public void IncludeSingle()
			{
				var generator = new RandomValueGenerator();
				{
					generator.DefaultCharsToInclude = RandomValueGenerator.CharsToInclude.Space;
					string str = generator.String(_length);
					ValidateString(str, CharTypesAllowed.Space);
				}
				{
					generator.DefaultCharsToInclude = RandomValueGenerator.CharsToInclude.Digits;
					string str = generator.String(_length);
					ValidateString(str, CharTypesAllowed.Digit);
				}
				{
					generator.DefaultCharsToInclude = RandomValueGenerator.CharsToInclude.Uppercase;
					string str = generator.String(_length);
					ValidateString(str, CharTypesAllowed.Upper);
				}
				{
					generator.DefaultCharsToInclude = RandomValueGenerator.CharsToInclude.LowerCase;
					string str = generator.String(_length);
					ValidateString(str, CharTypesAllowed.Lower);
				}
				{
					generator.DefaultCharsToInclude = RandomValueGenerator.CharsToInclude.Symbols;
					string str = generator.String(_length);
					ValidateString(str, CharTypesAllowed.Symbol);
				}
				{
					generator.DefaultCharsToInclude = RandomValueGenerator.CharsToInclude.Letters;
					string str = generator.String(_length);
					ValidateString(str, CharTypesAllowed.Upper | CharTypesAllowed.Lower);
				}
				{
					generator.DefaultCharsToInclude = RandomValueGenerator.CharsToInclude.AlphaNumeric;
					string str = generator.String(_length);
					ValidateString(str, CharTypesAllowed.Upper | CharTypesAllowed.Lower | CharTypesAllowed.Digit);
				}
				{
					generator.DefaultCharsToInclude = RandomValueGenerator.CharsToInclude.All;
					string str = generator.String(_length);
					ValidateString(str,
						CharTypesAllowed.Upper | CharTypesAllowed.Lower | CharTypesAllowed.Digit | CharTypesAllowed.Symbol | CharTypesAllowed.Space);
				}
			}

			[Test]
			public void Length()
			{
				var generator = new RandomValueGenerator();
				for (uint length = 200; length < 400; ++length)
				{
					generator.DefaultStringLength = length;
					string str = generator.String(RandomValueGenerator.CharsToInclude.All);
					Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(length, (uint)str.Length, length.ToString());
				}
			}

			[Test]
			public void IncludeAndLength()
			{
				var generator = new RandomValueGenerator();
				{
					generator.DefaultCharsToInclude = RandomValueGenerator.CharsToInclude.Space;
					for (uint length = 100; length < 200; ++length)
					{
						generator.DefaultStringLength = length;
						string str = generator.String();
						Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(length, (uint)str.Length, length.ToString());
						ValidateString(str, CharTypesAllowed.Space);
					}
				}
				{
					generator.DefaultCharsToInclude = RandomValueGenerator.CharsToInclude.AlphaNumeric;
					for (uint length = 100; length < 200; ++length)
					{
						generator.DefaultStringLength = length;
						string str = generator.String();
						Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(length, (uint)str.Length, length.ToString());
						ValidateString(str, CharTypesAllowed.Upper | CharTypesAllowed.Lower | CharTypesAllowed.Digit);
					}
				}
			}

			[Test]
			public void IncludeList()
			{
				var generator = new RandomValueGenerator();
				{
					generator.DefaultCharsToInclude = RandomValueGenerator.CharsToInclude.Space;
					var list = generator.Strings(_count, _length);
					ValidateStrings(list, CharTypesAllowed.Space);
				}
				{
					generator.DefaultCharsToInclude = RandomValueGenerator.CharsToInclude.Digits;
					var list = generator.Strings(_count, _length);
					ValidateStrings(list, CharTypesAllowed.Digit);
				}
				{
					generator.DefaultCharsToInclude = RandomValueGenerator.CharsToInclude.Uppercase;
					var list = generator.Strings(_count, _length);
					ValidateStrings(list, CharTypesAllowed.Upper);
				}
				{
					generator.DefaultCharsToInclude = RandomValueGenerator.CharsToInclude.LowerCase;
					var list = generator.Strings(_count, _length);
					ValidateStrings(list, CharTypesAllowed.Lower);
				}
				{
					generator.DefaultCharsToInclude = RandomValueGenerator.CharsToInclude.Symbols;
					var list = generator.Strings(_count, _length);
					ValidateStrings(list, CharTypesAllowed.Symbol);
				}
				{
					generator.DefaultCharsToInclude = RandomValueGenerator.CharsToInclude.Letters;
					var list = generator.Strings(_count, _length);
					ValidateStrings(list, CharTypesAllowed.Upper | CharTypesAllowed.Lower);
				}
				{
					generator.DefaultCharsToInclude = RandomValueGenerator.CharsToInclude.AlphaNumeric;
					var list = generator.Strings(_count, _length);
					ValidateStrings(list, CharTypesAllowed.Upper | CharTypesAllowed.Lower | CharTypesAllowed.Digit);
				}
				{
					generator.DefaultCharsToInclude = RandomValueGenerator.CharsToInclude.All;
					var list = generator.Strings(_count, _length);
					ValidateStrings(list,
						CharTypesAllowed.Upper | CharTypesAllowed.Lower | CharTypesAllowed.Digit | CharTypesAllowed.Symbol | CharTypesAllowed.Space);
				}
			}

			[Test]
			public void Count()
			{
				var generator = new RandomValueGenerator();
				for (uint count = 0; count < 100; ++count)
				{
					generator.DefaultStringListCount = count;
					string[] list = generator.Strings(RandomValueGenerator.CharsToInclude.All);
					Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(count, (uint)list.Length, count.ToString());
				}
			}

			[Test]
			public void IncludeAndCount()
			{
				var generator = new RandomValueGenerator();
				{
					generator.DefaultCharsToInclude = RandomValueGenerator.CharsToInclude.Space;
					for (uint count = 100; count < 200; ++count)
					{
						generator.DefaultStringListCount = count;
						string[] list = generator.Strings();
						Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(count, (uint)list.Length, count.ToString());
						ValidateStrings(list, CharTypesAllowed.Space);
					}
				}
				{
					generator.DefaultCharsToInclude = RandomValueGenerator.CharsToInclude.AlphaNumeric;
					for (uint count = 100; count < 200; ++count)
					{
						generator.DefaultStringListCount = count;
						string[] list = generator.Strings();
						Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(count, (uint)list.Length, count.ToString());
						ValidateStrings(list, CharTypesAllowed.Upper | CharTypesAllowed.Lower | CharTypesAllowed.Digit);
					}
				}
			}
		}
	}
}