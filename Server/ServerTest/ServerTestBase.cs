using System;
using System.Text;
using System.Collections.Generic;
using NUnit.Framework;
using Rhino.Mocks;
using Rhino.Mocks.Constraints;
using ServerLib;
using System.Linq;

namespace ServerTest
{
    /// <summary>
    /// Summary description for LocalServerTest
    /// </summary>
    
    public abstract class ServerTestBase
    {
        protected IServer sut;
        protected CalendarEntry someEntry;
        protected MockRepository mocks;

        [TestFixtureSetUp]
        public void Init()
        {

        }


        [SetUp()]
        public abstract void SetUp();



        [TearDown]
        public void TearDown()
        {
            mocks.VerifyAll();
        }
        
        protected CalendarEntry getDefaultCalendarEntry()
        {
            return new CalendarEntry();
        }

        protected CalendarEntry getCalendarEntryWithTitle(string title)
        {
            CalendarEntry c = new CalendarEntry();
            c.Title = title;
            return c;
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
            sut.Add(getDefaultCalendarEntry());
            var data = from x in sut select x;
            Assert.IsTrue(data.Count<CalendarEntry>() == 1);
        }

        [Test]
        public void IfEntryIsAddedItCanFetched()
        {
            string title = "test";
            sut.Add(getCalendarEntryWithTitle(title));
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
