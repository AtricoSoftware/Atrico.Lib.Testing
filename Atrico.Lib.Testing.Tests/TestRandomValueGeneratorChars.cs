using System;
using System.Collections.Generic;
using System.Linq;
using Atrico.Lib.Assertions;
using Atrico.Lib.Testing.NUnitAttributes;

namespace Atrico.Lib.Testing.Tests
{
	[TestFixture]
	public class TestRandomValueGeneratorChars
	{
		#region Helpers

		private const int _iterations = 1000;
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
			private bool? Space { get; set; }

			private bool? Digit { get; set; }

			private bool? Upper { get; set; }

			private bool? Lower { get; set; }

			private bool? Symbl { get; set; }

			public ValidateResults(CharTypesAllowed allowed)
			{
				if ((allowed & CharTypesAllowed.Space) != 0)
					Space = false;
				if ((allowed & CharTypesAllowed.Digit) != 0)
					Digit = false;
				if ((allowed & CharTypesAllowed.Upper) != 0)
					Upper = false;
				if ((allowed & CharTypesAllowed.Lower) != 0)
					Lower = false;
				if ((allowed & CharTypesAllowed.Symbol) != 0)
					Symbl = false;
			}

			public void AddChar(char ch)
			{
				if (Space.HasValue)
					Space |= ch.IsSpace();
				else
					Assert.That(ch.IsSpace(), Is.False, "Space");
				if (Digit.HasValue)
					Digit |= ch.IsDigit();
				else
					Assert.That(ch.IsDigit(), Is.False, ch.ToString());
				if (Upper.HasValue)
					Upper |= ch.IsUppercase();
				else
					Assert.That(ch.IsUppercase(), Is.False, ch.ToString());
				if (Lower.HasValue)
					Lower |= ch.IsLowercase();
				else
					Assert.That(ch.IsLowercase(), Is.False, ch.ToString());
				if (Symbl.HasValue)
					Symbl |= ch.IsSymbol();
				else
					Assert.That(ch.IsSymbol(), Is.False, ch.ToString());
			}

			public void Validate()
			{
				if (Space.HasValue)
					Assert.That(Space.Value, Is.True, "Space");
				if (Digit.HasValue)
					Assert.That(Digit.Value, Is.True, "Digit");
				if (Upper.HasValue)
					Assert.That(Upper.Value, Is.True, "Uppercase");
				if (Lower.HasValue)
					Assert.That(Lower.Value, Is.True, "Lowercase");
				if (Symbl.HasValue)
					Assert.That(Symbl.Value, Is.True, "Symbol");
			}
		}

		private static void ValidateChars(IEnumerable<char> list, CharTypesAllowed allowed)
		{
			var results = new ValidateResults(allowed);
			foreach (var ch in list)
			{
				results.AddChar(ch);
			}
			results.Validate();
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
				var obj = generator.Char();
				Assert.That(obj, Is.TypeOf<char>());
			}

			[Test]
			public void IncludeSpaces()
			{
				var generator = new RandomValueGenerator();
				for (int i = 0; i < _iterations; ++i)
				{
					char ch = generator.Char(RandomValueGenerator.CharsToInclude.Space);
					bool valid = ch.IsSpace();
					Assert.That(valid, Is.True, ch.ToString());
				}
			}

			[Test]
			public void IncludeDigits()
			{
				var generator = new RandomValueGenerator();
				for (int i = 0; i < _iterations; ++i)
				{
					char ch = generator.Char(RandomValueGenerator.CharsToInclude.Digits);
					bool valid = ch.IsDigit();
					Assert.That(valid, Is.True, ch.ToString());
				}
			}

			[Test]
			public void IncludeUpper()
			{
				var generator = new RandomValueGenerator();
				for (int i = 0; i < _iterations; ++i)
				{
					char ch = generator.Char(RandomValueGenerator.CharsToInclude.Uppercase);
					bool valid = ch.IsUppercase();
					Assert.That(valid, Is.True, ch.ToString());
				}
			}

			[Test]
			public void IncludeLower()
			{
				var generator = new RandomValueGenerator();
				for (int i = 0; i < _iterations; ++i)
				{
					char ch = generator.Char(RandomValueGenerator.CharsToInclude.LowerCase);
					bool valid = ch.IsLowercase();
					Assert.That(valid, Is.True, ch.ToString());
				}
			}

			[Test]
			public void IncludeSymbol()
			{
				var generator = new RandomValueGenerator();
				for (int i = 0; i < _iterations; ++i)
				{
					char ch = generator.Char(RandomValueGenerator.CharsToInclude.Symbols);
					bool valid = ch.IsSymbol();
					Assert.That(valid, Is.True, ch.ToString());
				}
			}

			[Test]
			public void IncludeLetters()
			{
				var generator = new RandomValueGenerator();
				bool upper = false;
				bool lower = false;
				for (int i = 0; i < _iterations; ++i)
				{
					char ch = generator.Char(RandomValueGenerator.CharsToInclude.Letters);
					Assert.That(ch.IsSpace(), Is.False, "Space");
					Assert.That(ch.IsDigit(), Is.False, ch.ToString());
					Assert.That(ch.IsSymbol(), Is.False, ch.ToString());
					upper |= ch.IsUppercase();
					lower |= ch.IsLowercase();
				}
				Assert.That(upper, Is.True, "Uppercase");
				Assert.That(lower, Is.True, "Lowercase");
			}

			[Test]
			public void IncludeAlphanumeric()
			{
				var generator = new RandomValueGenerator();
				bool digit = false;
				bool upper = false;
				bool lower = false;
				for (int i = 0; i < _iterations; ++i)
				{
					char ch = generator.Char(RandomValueGenerator.CharsToInclude.AlphaNumeric);
					Assert.That(ch.IsSpace(), Is.False, "Space");
					Assert.That(ch.IsSymbol(), Is.False, ch.ToString());
					digit |= ch.IsDigit();
					upper |= ch.IsUppercase();
					lower |= ch.IsLowercase();
				}
				Assert.That(upper, Is.True, "Uppercase");
				Assert.That(lower, Is.True, "Lowercase");
				Assert.That(digit, Is.True, "Digit");
			}

			[Test]
			public void IncludeAll()
			{
				var generator = new RandomValueGenerator();
				bool space = false;
				bool digit = false;
				bool upper = false;
				bool lower = false;
				bool symbl = false;
				for (int i = 0; i < _iterations; ++i)
				{
					char ch = generator.Char(RandomValueGenerator.CharsToInclude.All);
					space |= ch.IsSpace();
					digit |= ch.IsDigit();
					upper |= ch.IsUppercase();
					lower |= ch.IsLowercase();
					symbl |= ch.IsSymbol();
				}
				Assert.That(upper, Is.True, "Uppercase");
				Assert.That(lower, Is.True, "Lowercase");
				Assert.That(digit, Is.True, "Digit");
				Assert.That(space, Is.True, "Space");
				Assert.That(symbl, Is.True, "Symbol");
			}

			[Test]
			public void IncludeDefault()
			{
				var generator = new RandomValueGenerator();
				bool space = false;
				bool digit = false;
				bool upper = false;
				bool lower = false;
				bool symbl = false;
				for (int i = 0; i < _iterations; ++i)
				{
					char ch = generator.Char();
					space |= ch.IsSpace();
					digit |= ch.IsDigit();
					upper |= ch.IsUppercase();
					lower |= ch.IsLowercase();
					symbl |= ch.IsSymbol();
				}
				Assert.That(upper, Is.True, "Uppercase");
				Assert.That(lower, Is.True, "Lowercase");
				Assert.That(digit, Is.True, "Digit");
				Assert.That(space, Is.True, "Space");
				Assert.That(symbl, Is.True, "Symbol");
			}
		}

		[TestFixture]
		public class Lists
		{
			[Test]
			public void Default()
			{
				var generator = new RandomValueGenerator();
				var obj = generator.Chars();
				Assert.That(obj, Is.TypeOf<char[]>());
			}

			[Test]
			public void Count()
			{
				var generator = new RandomValueGenerator();
				char[] list = generator.Chars(_count);
				Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(_count, (uint)list.Length);
			}

			[Test]
			public void IncludeSpaces()
			{
				var generator = new RandomValueGenerator();
				char[] list = generator.Chars(RandomValueGenerator.CharsToInclude.Space, _count);
				Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(_count, (uint)list.Length);
				ValidateChars(list, CharTypesAllowed.Space);
			}

			[Test]
			public void IncludeDigits()
			{
				var generator = new RandomValueGenerator();
				char[] list = generator.Chars(RandomValueGenerator.CharsToInclude.Digits, _count);
				Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(_count, (uint)list.Length);
				ValidateChars(list, CharTypesAllowed.Digit);
			}

			[Test]
			public void IncludeUpper()
			{
				var generator = new RandomValueGenerator();
				char[] list = generator.Chars(RandomValueGenerator.CharsToInclude.Uppercase, _count);
				Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(_count, (uint)list.Length);
				ValidateChars(list, CharTypesAllowed.Upper);
			}

			[Test]
			public void IncludeLower()
			{
				var generator = new RandomValueGenerator();
				char[] list = generator.Chars(RandomValueGenerator.CharsToInclude.LowerCase, _count);
				Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(_count, (uint)list.Length);
				ValidateChars(list, CharTypesAllowed.Lower);
			}

			[Test]
			public void IncludeSymbol()
			{
				var generator = new RandomValueGenerator();
				char[] list = generator.Chars(RandomValueGenerator.CharsToInclude.Symbols, _count);
				Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(_count, (uint)list.Length);
				ValidateChars(list, CharTypesAllowed.Symbol);
			}

			[Test]
			public void IncludeLetters()
			{
				var generator = new RandomValueGenerator();
				char[] list = generator.Chars(RandomValueGenerator.CharsToInclude.Letters, _count);
				Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(_count, (uint)list.Length);
				ValidateChars(list, CharTypesAllowed.Upper | CharTypesAllowed.Lower);
			}

			[Test]
			public void IncludeAlphanumeric()
			{
				var generator = new RandomValueGenerator();
				char[] list = generator.Chars(RandomValueGenerator.CharsToInclude.AlphaNumeric, _count);
				Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(_count, (uint)list.Length);
				ValidateChars(list, CharTypesAllowed.Upper | CharTypesAllowed.Lower | CharTypesAllowed.Digit);
			}

			[Test]
			public void IncludeAll()
			{
				var generator = new RandomValueGenerator();
				char[] list = generator.Chars(RandomValueGenerator.CharsToInclude.All, _count);
				Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(_count, (uint)list.Length);
				ValidateChars(list,
					CharTypesAllowed.Upper | CharTypesAllowed.Lower | CharTypesAllowed.Digit | CharTypesAllowed.Symbol | CharTypesAllowed.Space);
			}

			[Test]
			public void IncludeDefault()
			{
				var generator = new RandomValueGenerator();
				char[] list = generator.Chars(_count);
				Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(_count, (uint)list.Length);
				ValidateChars(list,
					CharTypesAllowed.Upper | CharTypesAllowed.Lower | CharTypesAllowed.Digit | CharTypesAllowed.Symbol | CharTypesAllowed.Space);
			}
		}

		[TestFixture]
		public class Defaults
		{
			[Test]
			public void Include()
			{
				var generator = new RandomValueGenerator();
				{
					generator.DefaultCharsToInclude = RandomValueGenerator.CharsToInclude.Space;
					char[] list = generator.Chars(_count);
					ValidateChars(list, CharTypesAllowed.Space);
				}
				{
					generator.DefaultCharsToInclude = RandomValueGenerator.CharsToInclude.Digits;
					char[] list = generator.Chars(_count);
					ValidateChars(list, CharTypesAllowed.Digit);
				}
				{
					generator.DefaultCharsToInclude = RandomValueGenerator.CharsToInclude.Uppercase;
					char[] list = generator.Chars(_count);
					ValidateChars(list, CharTypesAllowed.Upper);
				}
				{
					generator.DefaultCharsToInclude = RandomValueGenerator.CharsToInclude.LowerCase;
					char[] list = generator.Chars(_count);
					ValidateChars(list, CharTypesAllowed.Lower);
				}
				{
					generator.DefaultCharsToInclude = RandomValueGenerator.CharsToInclude.Symbols;
					char[] list = generator.Chars(_count);
					ValidateChars(list, CharTypesAllowed.Symbol);
				}
				{
					generator.DefaultCharsToInclude = RandomValueGenerator.CharsToInclude.Letters;
					char[] list = generator.Chars(_count);
					ValidateChars(list, CharTypesAllowed.Upper | CharTypesAllowed.Lower);
				}
				{
					generator.DefaultCharsToInclude = RandomValueGenerator.CharsToInclude.AlphaNumeric;
					char[] list = generator.Chars(_count);
					ValidateChars(list, CharTypesAllowed.Upper | CharTypesAllowed.Lower | CharTypesAllowed.Digit);
				}
				{
					generator.DefaultCharsToInclude = RandomValueGenerator.CharsToInclude.All;
					char[] list = generator.Chars(_count);
					ValidateChars(list,
						CharTypesAllowed.Upper | CharTypesAllowed.Lower | CharTypesAllowed.Digit | CharTypesAllowed.Symbol | CharTypesAllowed.Space);
				}
			}

			[Test]
			public void Count()
			{
				var generator = new RandomValueGenerator();
				for (uint count = 0; count < 100; ++count)
				{
					generator.DefaultCharListCount = count;
					char[] list = generator.Chars(RandomValueGenerator.CharsToInclude.All);
					Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(count, (uint)list.Length, count.ToString());
				}
			}

			[Test]
			public void IncludeAndCount()
			{
				var generator = new RandomValueGenerator();
				{
					generator.DefaultCharsToInclude = RandomValueGenerator.CharsToInclude.Space;
					for (uint count = 100; count < 400; ++count)
					{
						generator.DefaultCharListCount = count;
						char[] list = generator.Chars();
						Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(count, (uint)list.Length, count.ToString());
						ValidateChars(list, CharTypesAllowed.Space);
					}
				}
				{
					generator.DefaultCharsToInclude = RandomValueGenerator.CharsToInclude.AlphaNumeric;
					for (uint count = 100; count < 400; ++count)
					{
						generator.DefaultCharListCount = count;
						char[] list = generator.Chars();
						Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(count, (uint)list.Length, count.ToString());
						ValidateChars(list, CharTypesAllowed.Upper | CharTypesAllowed.Lower | CharTypesAllowed.Digit);
					}
				}
			}
		}
	}
}