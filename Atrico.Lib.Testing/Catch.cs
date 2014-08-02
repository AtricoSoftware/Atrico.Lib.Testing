using System;
using System.Diagnostics;
using System.ServiceModel;

namespace Atrico.Lib.Testing
{
	public static class Catch
	{
		public static Exception Exception(Action throwingAction)
		{
			try
			{
				throwingAction();
			}
			catch (Exception exception)
			{
				Debug.Print(exception.ToString());
				return exception;
			}

			return null;
		}

		public static T Exception<T>(Action throwingAction) where T : Exception
		{
			try
			{
				throwingAction();
			}
			catch (T exception)
			{
				Debug.Print(exception.ToString());
				return exception;
			}

			return null;
		}

		public static T Fault<T>(Action throwingAction)
		{
			try
			{
				throwingAction();
			}
			catch (FaultException<T> exception)
			{
				Debug.Print(exception.ToString());
				return exception.Detail;
			}

			return default(T);
		}
	}
}