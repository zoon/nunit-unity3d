#region Usings

using System;
using System.Collections.Generic;
using NUnit.Framework;

#endregion

namespace Test
{
    [TestFixture]
    public class TestPrimer
    {
        #region Setup/Teardown

        [SetUp]
        public void InitSources()
        {
            source01 = new[] {1, 2, 3};
            source02 = new List<int>(source01);
        }

        #endregion

        private int[] source01;
        private List<int> source02;


        [Test]
        public void Add()
        {
            var result = source01.Length + source02.Count;
            Assert.That(result, Is.EqualTo(6));
        }


        [Test]
        [ExpectedException(typeof (InvalidCastException))]
        public void ExpectAnException()
        {
            throw new InvalidCastException();
        }


        [Test]
        [Ignore("ignored test")]
        public void IgnoredTest()
        {
            throw new Exception();
        }


        [Test]
        public void SourcesAreEqual()
        {
            Assert.That(source02, Is.EquivalentTo(source01));
            Assert.That(source01, Is.EqualTo(source02));
            Assert.That(source01, Is.EqualTo(source02).AsCollection);
        }


        [Test]
        public void SourcesAreSubsetOfEachOther()
        {
            Assert.That(source01, Is.SubsetOf(source02));
            Assert.That(source02, Is.SubsetOf(source01));
        }

        [Test]
        public void AllSource01MembersLessThen5()
        {
            Assert.That(source01, Is.All.LessThan(5) & Is.Unique);
            Assert.That(source01, Is.All.InRange(1,3));
            Assert.That(source02,
                        Is.All.InRange(1,3).Using(
                            System.Collections.Comparer.Default));
        }
    }
}
