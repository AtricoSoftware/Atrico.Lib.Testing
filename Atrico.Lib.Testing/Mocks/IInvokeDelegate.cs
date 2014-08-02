namespace Atrico.Lib.Testing.Mocks
{
	/// <summary>
	/// Interface for mocking delegates
	/// </summary>
	public interface IInvokeDelegate
	{
		void Action<T>(T subject);
		bool Predicate<T>(T candidate);
	}
}