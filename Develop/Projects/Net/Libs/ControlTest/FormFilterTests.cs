using NUnit.Framework;
using Savchin.Comparer;
using Savchin.Forms.Comparer;

namespace ControlTest
{
    [TestFixture]
    public class FormFilterTests
    {
        [Test]
        public void Test()
        {
            Filter filter = new Filter();
            filter.PrimitivesKeys.Add(new KeyMatcher("Item1"));
            filter.PrimitivesKeys.Add(new KeyMatcher("Item2"));
            FormFilter.EditFilter(filter);

        }
    }
}

