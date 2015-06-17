using System;
using System.Collections.Generic;

namespace Atrico.Lib.Testing.TestAttributes
{
    public class RangeAttribute : global::NUnit.Framework.ValuesAttribute
    {
        /// <summary>
        ///     Constructor for <cref>char</cref> values
        /// </summary>
        /// <param name="from">Inclusive from</param>
        /// <param name="to">Inclusive to</param>
        public RangeAttribute(char from, char to)
            : this(from, to, 1)

        {
        }

        /// <summary>
        ///     Constructor for <cref>char</cref> values with step
        /// </summary>
        /// <param name="from">Inclusive from</param>
        /// <param name="to">Inclusive to</param>
        /// <param name="step">Increment between values</param>
        public RangeAttribute(char from, char to, int step)
            : base(CreateRange(from, to, step, typeof (char)))

        {
        }

        /// <summary>
        ///     Constructor for <cref>byte</cref> values
        /// </summary>
        /// <param name="from">Inclusive from</param>
        /// <param name="to">Inclusive to</param>
        public RangeAttribute(byte from, byte to)
            : this(from, to, (byte)1)

        {
        }

        /// <summary>
        ///     Constructor for <cref>byte</cref> values with step
        /// </summary>
        /// <param name="from">Inclusive from</param>
        /// <param name="to">Inclusive to</param>
        /// <param name="step">Increment between values</param>
        public RangeAttribute(byte from, byte to, byte step)
            : base(CreateRange(from, to, step, typeof (byte)))

        {
        }

        /// <summary>
        ///     Constructor for <cref>sbyte</cref> values
        /// </summary>
        /// <param name="from">Inclusive from</param>
        /// <param name="to">Inclusive to</param>
        public RangeAttribute(sbyte from, sbyte to)
            : this(from, to, (sbyte)1)

        {
        }

        /// <summary>
        ///     Constructor for <cref>sbyte</cref> values with step
        /// </summary>
        /// <param name="from">Inclusive from</param>
        /// <param name="to">Inclusive to</param>
        /// <param name="step">Increment between values</param>
        public RangeAttribute(sbyte from, sbyte to, sbyte step)
            : base(CreateRange(from, to, step, typeof (sbyte)))

        {
        }

        /// <summary>
        ///     Constructor for <cref>int</cref> values
        /// </summary>
        /// <param name="from">Inclusive from</param>
        /// <param name="to">Inclusive to</param>
        public RangeAttribute(int from, int to)
            : this(from, to, 1)

        {
        }

        /// <summary>
        ///     Constructor for <cref>int</cref> values with step
        /// </summary>
        /// <param name="from">Inclusive from</param>
        /// <param name="to">Inclusive to</param>
        /// <param name="step">Increment between values</param>
        public RangeAttribute(int from, int to, int step)
            : base(CreateRange(from, to, step, typeof (int)))

        {
        }

        /// <summary>
        ///     Constructor for <cref>uint</cref> values
        /// </summary>
        /// <param name="from">Inclusive from</param>
        /// <param name="to">Inclusive to</param>
        public RangeAttribute(uint from, uint to)
            : this(from, to, 1u)

        {
        }

        /// <summary>
        ///     Constructor for <cref>uint</cref> values with step
        /// </summary>
        /// <param name="from">Inclusive from</param>
        /// <param name="to">Inclusive to</param>
        /// <param name="step">Increment between values</param>
        public RangeAttribute(uint from, uint to, uint step)
            : base(CreateRange(from, to, step, typeof (uint)))

        {
        }

        /// <summary>
        ///     Constructor for <cref>long</cref> values
        /// </summary>
        /// <param name="from">Inclusive from</param>
        /// <param name="to">Inclusive to</param>
        public RangeAttribute(long from, long to)
            : this(from, to, 1L)

        {
        }

        /// <summary>
        ///     Constructor for <cref>long</cref> values with step
        /// </summary>
        /// <param name="from">Inclusive from</param>
        /// <param name="to">Inclusive to</param>
        /// <param name="step">Increment between values</param>
        public RangeAttribute(long from, long to, long step)
            : base(CreateRange(from, to, step, typeof (long)))

        {
        }

        /// <summary>
        ///     Constructor for <cref>ulong</cref> values
        /// </summary>
        /// <param name="from">Inclusive from</param>
        /// <param name="to">Inclusive to</param>
        public RangeAttribute(ulong from, ulong to)
            : this(from, to, 1UL)

        {
        }

        /// <summary>
        ///     Constructor for <cref>ulong</cref> values with step
        /// </summary>
        /// <param name="from">Inclusive from</param>
        /// <param name="to">Inclusive to</param>
        /// <param name="step">Increment between values</param>
        public RangeAttribute(ulong from, ulong to, ulong step)
            : base(CreateRange(from, to, step, typeof (ulong)))

        {
        }

        /// <summary>
        ///     Constructor for <cref>float</cref> values
        /// </summary>
        /// <param name="from">Inclusive from</param>
        /// <param name="to">Inclusive to</param>
        public RangeAttribute(float from, float to)
            : this(from, to, 1.0f)

        {
        }

        /// <summary>
        ///     Constructor for <cref>float</cref> values with step
        /// </summary>
        /// <param name="from">Inclusive from</param>
        /// <param name="to">Inclusive to</param>
        /// <param name="step">Increment between values</param>
        public RangeAttribute(float from, float to, float step)
            : base(CreateRange(from, to, step, typeof (float)))

        {
        }

        /// <summary>
        ///     Constructor for <cref>double</cref> values
        /// </summary>
        /// <param name="from">Inclusive from</param>
        /// <param name="to">Inclusive to</param>
        public RangeAttribute(double from, double to)
            : this(from, to, 1.0)
        {
        }

        /// <summary>
        ///     Constructor for <cref>double</cref> values with step
        /// </summary>
        /// <param name="from">Inclusive from</param>
        /// <param name="to">Inclusive to</param>
        /// <param name="step">Increment between values</param>
        public RangeAttribute(double from, double to, double step)
            : base(CreateRange(from, to, step, typeof (double)))
        {
        }

        private static object[] CreateRange(long @from, long to, long step, Type type)
        {
            var data = new List<object>();
            var num = from;
            while (num <= to)
            {
                data.Add(Convert.ChangeType(num, type));
                num += step;
            }
            return data.ToArray();
        }

        private static object[] CreateRange(double @from, double to, double step, Type type)
        {
            var data = new List<object>();
            var num = from;
            while (num <= to)
            {
                data.Add(Convert.ChangeType(num, type));
                num += step;
            }
            return data.ToArray();
        }
    }
}