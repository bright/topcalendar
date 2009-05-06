using System;
using System.Text;
using System.Collections.Generic;
using NUnit.Framework;
using Rhino.Mocks;
using Rhino.Mocks.Constraints;
using ClientApp;

namespace ClientTest
{
    /// <summary>
    /// Summary description for CalendarEntryTest
    /// </summary>
    [TestFixture]
    public class CalendarEntryTest
    {
        private MockRepository mocks;
        private CalendarEntry sut;

        [TestFixtureSetUp]
        public void Init()
        {

        }

        [SetUp()]
        public void SetUp()
        {
            mocks = new MockRepository();
        }

        [TearDown]
        public void TearDown()
        {
            mocks.VerifyAll();
        }

        [Test]
        public void TitleIsSavedProperly()
        {
            string title = "test";
            sut = new CalendarEntry(title);
            Assert.IsTrue(sut.Title == title);
        }
        [Test]
        public void DateIsInitializedProperly()
        {
            DateTime expected = DateTime.Now;
            
            sut = new CalendarEntry(null,expected);

            Assert.AreEqual(0, DateTime.Compare(expected, sut.DateTime));
        }
    }
}
