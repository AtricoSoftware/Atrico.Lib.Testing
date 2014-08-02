namespace Atrico.Lib.Testing.Tests
{
	internal static class CharHelper
	{
		public static bool IsSpace(this char ch)
		{
			return ch == ' ';
		}

		public static bool IsDigit(this char ch)
		{
			return '0' <= ch && ch <= '9';
		}

		public static bool IsUppercase(this char ch)
		{
			return 'A' <= ch && ch <= 'Z';
		}

		public static bool IsLowercase(this char ch)
		{
			return 'a' <= ch && ch <= 'z';
		}

		public static bool IsSymbol(this char ch)
		{
			return (' ' < ch && ch < '0')
			       || ('9' < ch && ch < 'A')
			       || ('Z' < ch && ch < 'a')
			       || ('z' < ch);
		}
	}
}