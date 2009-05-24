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
        private CalendarEntry someEntry;
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
            someEntry = new CalendarEntry("someEntry");
        }

        [TearDown]
        public void TearDown()
        {
            mocks.VerifyAll();
        }
        
        private CalendarEntry getDefaultCalendarEntry()
        {
            return new CalendarEntry();
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
        [Test]
        public void IfEntryIsRemovedFromEmptyServerCountOfEntriesIsStillEqual_0()
        {
            sut.Remove(someEntry);
            Assert.IsTrue(sut.Count == 0);
        }
        [Test]
        public void If_WeAddEntryToEmptyServerAndThenRemoveThisSameEntry_ThenEntriesListIsStillEmpty()
        {
            sut.Add(someEntry);
            sut.Remove(someEntry);
            Assert.IsTrue(sut.Count == 0);
        }
        [Test]
        public void If_we_remove_entry_then_we_can_not_fetch_this_entry()
        {
            sut.Add(getDefaultCalendarEntry());
            sut.Add(someEntry);
            sut.Add(getDefaultCalendarEntry());

            sut.Remove(someEntry);
            var a = from x in sut where x == someEntry select x;
            Assert.IsTrue(a.Count<CalendarEntry>() == 0);

        }



    }
}
