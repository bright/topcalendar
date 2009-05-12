using System;
using System.Text;
using System.Collections.Generic;
using NUnit.Framework;
using Rhino.Mocks;
using Rhino.Mocks.Constraints;
using ClientApp;
using System.Linq;

namespace ClientTest
{
    /// <summary>
    /// Summary description for LocalServerTest
    /// </summary>
    [TestFixture]
    public class LocalServerTest
    {
        private LocalServer sut;

        private MockRepository mocks;

        [TestFixtureSetUp]
        public void Init()
        {

        }

        [SetUp()]
        public void SetUp()
        {
            mocks = new MockRepository();
            sut = new LocalServer();
        }

        [TearDown]
        public void TearDown()
        {
            mocks.VerifyAll();
        }
        
       

        [Test]
        public void NewServerShouldHaveNoEntries()
        {
            Assert.IsTrue(sut.Count == 0);
        }

        [Test]
        public void NewItemCanBeAddedAndCounterIncreases()
        {
            sut.Add(new CalendarEntry());
            Assert.IsTrue(sut.Count == 1);
        }

        [Test]
        public void NoEntryCanBeFetchedFromEmptyServer()
        {
            var data = from x in sut select x;
            Assert.IsTrue(data.Count<CalendarEntry>() == 0);
        }

        [Test]
        public void IfOneEntryIsAddedOneCanBeFetched()
        {
            sut.Add(new CalendarEntry());
            var data = from x in sut select x;
            Assert.IsTrue(data.Count<CalendarEntry>() == 1);
        }

        [Test]
        public void IfEntryIsAddedItCanFetched()
        {
            string title = "test";
            sut.Add(new CalendarEntry(title));
            var data = from x in sut where x.Title == title select x;
            Assert.IsTrue(data.Count<CalendarEntry>() == 1);
            Assert.IsTrue(data.First<CalendarEntry>().Title == title);
        }
        
        

    }
}
