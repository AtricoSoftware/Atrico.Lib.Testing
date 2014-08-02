using System.Collections;
using NUnit.Framework;

namespace Atrico.Lib.Testing
{
		/// <summary>
		/// Data sources for data driven tests
		/// </summary>
		public class DataSources
		{
			/// <summary>
			/// Base class for Value List
			/// </summary>
			public abstract class ValuesBase
				: IEnumerable
			{
				protected abstract IEnumerable TheValues { get; }

				public IEnumerator GetEnumerator()
				{
					return TheValues.GetEnumerator();
				}
			}

			/// <summary>
			/// Boolean values
			/// </summary>
			public class BooleanValues
				: ValuesBase
			{
				protected override IEnumerable TheValues
				{
					get
					{
						yield return false;
						yield return true;
					}
				}
			}

			/// <summary>
			/// POD types
			/// </summary>
			public class PodTypes
				: ValuesBase
			{
				protected override IEnumerable TheValues
				{
					get
					{
						yield return 'A'; // char
						yield return ""; // string
						yield return default(byte);
						yield return default(sbyte);
						yield return default(short);
						yield return default(ushort);
						yield return default(int);
						yield return default(uint);
						yield return default(long);
						yield return default(ulong);
						yield return default(float);
						yield return default(double);
					}
				}
			}

			/// <summary>
			/// All permutations of 2 values
			/// </summary>
			public class Permutations<T1, T2>
				: ValuesBase
				where T1 : IEnumerable, new()
				where T2 : IEnumerable, new()
			{
				protected override IEnumerable TheValues
				{
					get
					{
						foreach (var v1 in new T1())
						{
							foreach (var v2 in new T2())	
							{
								yield return new object[] {v1, v2};
							}
						}
					}
				}
			}

			/// <summary>
			/// All permutations of 3 values
			/// </summary>
			public class Permutations<T1, T2, T3>
				: ValuesBase
				where T1 : IEnumerable, new()
				where T2 : IEnumerable, new()
				where T3 : IEnumerable, new()
			{
				protected override IEnumerable TheValues
				{
					get
					{
						foreach (var v1 in new T1())
						{
							foreach (var v2 in new T2())
							{
								foreach (var v3 in new T3())
								{
									yield return new object[] {v1, v2, v3};
								}
							}
						}
					}
				}
			}
		}
	[TestFixture]
	public abstract class TestFixtureBase
	{
		/// <summary>
		/// Random value generator
		/// </summary>
		protected static readonly RandomValueGenerator RandomValues = new RandomValueGenerator();
		


	}
}