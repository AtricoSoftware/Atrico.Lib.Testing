namespace Atrico.Lib.Testing.NUnitAttributes
{
    public class RangeAttribute : NUnit.Framework.RangeAttribute
    {
        public RangeAttribute(int @from, int to)
            : base(@from, to)
        {
        }

        public RangeAttribute(int @from, int to, int step)
            : base(@from, to, step)
        {
        }

        public RangeAttribute(long @from, long to, long step)
            : base(@from, to, step)
        {
        }

        public RangeAttribute(double @from, double to, double step)
            : base(@from, to, step)
        {
        }

        public RangeAttribute(float @from, float to, float step)
            : base(@from, to, step)
        {
        }
    }
}
