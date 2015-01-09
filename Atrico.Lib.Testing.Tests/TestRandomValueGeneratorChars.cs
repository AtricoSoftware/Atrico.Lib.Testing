using System;
using System.Collections.Generic;
using System.Globalization;
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
			private bool? _space { get; set; }

			private bool? _digit { get; set; }

			private bool? _upper { get; set; }

			private bool? _lower { get; set; }

			private bool? _symbol { get; set; }

			public ValidateResults(CharTypesAllowed allowed)
			{
				if ((allowed & CharTypesAllowed.Space) != 0)
				{
					_space = false;
				}
				if ((allowed & CharTypesAllowed.Digit) != 0)
				{
					_digit = false;
				}
				if ((allowed & CharTypesAllowed.Upper) != 0)
				{
					_upper = false;
				}
				if ((allowed & CharTypesAllowed.Lower) != 0)
				{
					_lower = false;
				}
				if ((allowed & CharTypesAllowed.Symbol) != 0)
				{
					_symbol = false;
				}
			}

			public void AddChar(char ch)
			{
				if (_space.HasValue)
				{
					_space |= ch.IsSpace();
				}
				else
				{
					Assert.That(ch.IsSpace(), Is.False, "Space");
				}
				if (_digit.HasValue)
				{
					_digit |= ch.IsDigit();
				}
				else
				{
					Assert.That(ch.IsDigit(), Is.False, ch.ToString(CultureInfo.InvariantCulture));
				}
				if (_upper.HasValue)
				{
					_upper |= ch.IsUppercase();
				}
				else
				{
					Assert.That(ch.IsUppercase(), Is.False, ch.ToString(CultureInfo.InvariantCulture));
				}
				if (_lower.HasValue)
				{
					_lower |= ch.IsLowercase();
				}
				else
				{
					Assert.That(ch.IsLowercase(), Is.False, ch.ToString(CultureInfo.InvariantCulture));
				}
				if (_symbol.HasValue)
				{
					_symbol |= ch.IsSymbol();
				}
				else
				{
					Assert.That(ch.IsSymbol(), Is.False, ch.ToString(CultureInfo.InvariantCulture));
				}
			}

			public void Validate()
			{
				if (_space.HasValue)
				{
					Assert.That(_space.Value, Is.True, "Space");
				}
				if (_digit.HasValue)
				{
					Assert.That(_digit.Value, Is.True, "Digit");
				}
				if (_upper.HasValue)
				{
					Assert.That(_upper.Value, Is.True, "Uppercase");
				}
				if (_lower.HasValue)
				{
					Assert.That(_lower.Value, Is.True, "Lowercase");
				}
				if (_symbol.HasValue)
				{
					Assert.That(_symbol.Value, Is.True, "Symbol");
				}
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
				for (var i = 0; i < _iterations; ++i)
				{
					var ch = generator.Char(RandomValueGenerator.CharsToInclude.Space);
					var valid = ch.IsSpace();
					Assert.That(valid, Is.True, ch.ToString(CultureInfo.InvariantCulture));
				}
			}

			[Test]
			public void IncludeDigits()
			{
				var generator = new RandomValueGenerator();
				for (var i = 0; i < _iterations; ++i)
				{
					var ch = generator.Char(RandomValueGenerator.CharsToInclude.Digits);
					var valid = ch.IsDigit();
					Assert.That(valid, Is.True, ch.ToString(CultureInfo.InvariantCulture));
				}
			}

			[Test]
			public void IncludeUpper()
			{
				var generator = new RandomValueGenerator();
				for (var i = 0; i < _iterations; ++i)
				{
					var ch = generator.Char(RandomValueGenerator.CharsToInclude.Uppercase);
					var valid = ch.IsUppercase();
					Assert.That(valid, Is.True, ch.ToString(CultureInfo.InvariantCulture));
				}
			}

			[Test]
			public void IncludeLower()
			{
				var generator = new RandomValueGenerator();
				for (var i = 0; i < _iterations; ++i)
				{
					var ch = generator.Char(RandomValueGenerator.CharsToInclude.LowerCase);
					var valid = ch.IsLowercase();
					Assert.That(valid, Is.True, ch.ToString(CultureInfo.InvariantCulture));
				}
			}

			[Test]
			public void IncludeSymbol()
			{
				var generator = new RandomValueGenerator();
				for (var i = 0; i < _iterations; ++i)
				{
					var ch = generator.Char(RandomValueGenerator.CharsToInclude.Symbols);
					var valid = ch.IsSymbol();
					Assert.That(valid, Is.True, ch.ToString(CultureInfo.InvariantCulture));
				}
			}

			[Test]
			public void IncludeLetters()
			{
				var generator = new RandomValueGenerator();
				var upper = false;
				var lower = false;
				for (var i = 0; i < _iterations; ++i)
				{
					var ch = generator.Char(RandomValueGenerator.CharsToInclude.Letters);
					Assert.That(ch.IsSpace(), Is.False, "Space");
					Assert.That(ch.IsDigit(), Is.False, ch.ToString(CultureInfo.InvariantCulture));
					Assert.That(ch.IsSymbol(), Is.False, ch.ToString(CultureInfo.InvariantCulture));
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
				var digit = false;
				var upper = false;
				var lower = false;
				for (var i = 0; i < _iterations; ++i)
				{
					var ch = generator.Char(RandomValueGenerator.CharsToInclude.AlphaNumeric);
					Assert.That(ch.IsSpace(), Is.False, "Space");
					Assert.That(ch.IsSymbol(), Is.False, ch.ToString(CultureInfo.InvariantCulture));
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
				var space = false;
				var digit = false;
				var upper = false;
				var lower = false;
				var symbl = false;
				for (var i = 0; i < _iterations; ++i)
				{
					var ch = generator.Char(RandomValueGenerator.CharsToInclude.All);
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
				var space = false;
				var digit = false;
				var upper = false;
				var lower = false;
				var symbl = false;
				for (var i = 0; i < _iterations; ++i)
				{
					var ch = generator.Char();
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
				var list = generator.Chars(_count);
				Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(_count, (uint) list.Length);
			}

			[Test]
			public void IncludeSpaces()
			{
				var generator = new RandomValueGenerator();
				var list = generator.Chars(RandomValueGenerator.CharsToInclude.Space, _count);
				Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(_count, (uint) list.Length);
				ValidateChars(list, CharTypesAllowed.Space);
			}

			[Test]
			public void IncludeDigits()
			{
				var generator = new RandomValueGenerator();
				var list = generator.Chars(RandomValueGenerator.CharsToInclude.Digits, _count);
				Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(_count, (uint) list.Length);
				ValidateChars(list, CharTypesAllowed.Digit);
			}

			[Test]
			public void IncludeUpper()
			{
				var generator = new RandomValueGenerator();
				var list = generator.Chars(RandomValueGenerator.CharsToInclude.Uppercase, _count);
				Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(_count, (uint) list.Length);
				ValidateChars(list, CharTypesAllowed.Upper);
			}

			[Test]
			public void IncludeLower()
			{
				var generator = new RandomValueGenerator();
				var list = generator.Chars(RandomValueGenerator.CharsToInclude.LowerCase, _count);
				Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(_count, (uint) list.Length);
				ValidateChars(list, CharTypesAllowed.Lower);
			}

			[Test]
			public void IncludeSymbol()
			{
				var generator = new RandomValueGenerator();
				var list = generator.Chars(RandomValueGenerator.CharsToInclude.Symbols, _count);
				Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(_count, (uint) list.Length);
				ValidateChars(list, CharTypesAllowed.Symbol);
			}

			[Test]
			public void IncludeLetters()
			{
				var generator = new RandomValueGenerator();
				var list = generator.Chars(RandomValueGenerator.CharsToInclude.Letters, _count);
				Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(_count, (uint) list.Length);
				ValidateChars(list, CharTypesAllowed.Upper | CharTypesAllowed.Lower);
			}

			[Test]
			public void IncludeAlphanumeric()
			{
				var generator = new RandomValueGenerator();
				var list = generator.Chars(RandomValueGenerator.CharsToInclude.AlphaNumeric, _count);
				Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(_count, (uint) list.Length);
				ValidateChars(list, CharTypesAllowed.Upper | CharTypesAllowed.Lower | CharTypesAllowed.Digit);
			}

			[Test]
			public void IncludeAll()
			{
				var generator = new RandomValueGenerator();
				var list = generator.Chars(RandomValueGenerator.CharsToInclude.All, _count);
				Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(_count, (uint) list.Length);
				ValidateChars(list,
				              CharTypesAllowed.Upper | CharTypesAllowed.Lower | CharTypesAllowed.Digit | CharTypesAllowed.Symbol | CharTypesAllowed.Space);
			}

			[Test]
			public void IncludeDefault()
			{
				var generator = new RandomValueGenerator();
				var list = generator.Chars(_count);
				Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(_count, (uint) list.Length);
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
					var list = generator.Chars(_count);
					ValidateChars(list, CharTypesAllowed.Space);
				}
				{
					generator.DefaultCharsToInclude = RandomValueGenerator.CharsToInclude.Digits;
					var list = generator.Chars(_count);
					ValidateChars(list, CharTypesAllowed.Digit);
				}
				{
					generator.DefaultCharsToInclude = RandomValueGenerator.CharsToInclude.Uppercase;
					var list = generator.Chars(_count);
					ValidateChars(list, CharTypesAllowed.Upper);
				}
				{
					generator.DefaultCharsToInclude = RandomValueGenerator.CharsToInclude.LowerCase;
					var list = generator.Chars(_count);
					ValidateChars(list, CharTypesAllowed.Lower);
				}
				{
					generator.DefaultCharsToInclude = RandomValueGenerator.CharsToInclude.Symbols;
					var list = generator.Chars(_count);
					ValidateChars(list, CharTypesAllowed.Symbol);
				}
				{
					generator.DefaultCharsToInclude = RandomValueGenerator.CharsToInclude.Letters;
					var list = generator.Chars(_count);
					ValidateChars(list, CharTypesAllowed.Upper | CharTypesAllowed.Lower);
				}
				{
					generator.DefaultCharsToInclude = RandomValueGenerator.CharsToInclude.AlphaNumeric;
					var list = generator.Chars(_count);
					ValidateChars(list, CharTypesAllowed.Upper | CharTypesAllowed.Lower | CharTypesAllowed.Digit);
				}
				{
					generator.DefaultCharsToInclude = RandomValueGenerator.CharsToInclude.All;
					var list = generator.Chars(_count);
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
					var list = generator.Chars(RandomValueGenerator.CharsToInclude.All);
					Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(count, (uint) list.Length, count.ToString(CultureInfo.InvariantCulture));
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
						var list = generator.Chars();
						Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(count, (uint) list.Length, count.ToString(CultureInfo.InvariantCulture));
						ValidateChars(list, CharTypesAllowed.Space);
					}
				}
				{
					generator.DefaultCharsToInclude = RandomValueGenerator.CharsToInclude.AlphaNumeric;
					for (uint count = 100; count < 400; ++count)
					{
						generator.DefaultCharListCount = count;
						var list = generator.Chars();
						Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(count, (uint) list.Length, count.ToString(CultureInfo.InvariantCulture));
						ValidateChars(list, CharTypesAllowed.Upper | CharTypesAllowed.Lower | CharTypesAllowed.Digit);
					}
				}
			}
		}
	}
}
