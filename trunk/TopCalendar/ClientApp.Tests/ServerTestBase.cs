using System;
using System.Text;
using System.Collections.Generic;
using NUnit.Framework;
using Rhino.Mocks;
using Rhino.Mocks.Constraints;
using ClientApp;
using System.Linq;
using ClientApp.RemoteServerRef;

namespace ClientTest
{
    /// <summary>
    /// Summary description for LocalServerTest
    /// </summary>
    
    public abstract class ServerTestBase
    {
        protected IServer sut;
        protected BaseCalendarEntry someEntry;
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

        protected BaseCalendarEntry getDefaultCalendarEntry()
        {
            return new BaseCalendarEntry();
        }

        protected BaseCalendarEntry getCalendarEntryWithTitle(string title)
        {
            BaseCalendarEntry c = new BaseCalendarEntry();
            c.Title = title;
            return c;
        }

        [Test]
        public void NewServerShouldHaveNoEntries()
        {
            Assert.IsTrue(sut.get_Count() == 0);
        }

        [Test]
        public void NewItemCanBeAddedAndCounterIncreases()
        {
            sut.Add(new BaseCalendarEntry());
            Assert.IsTrue(sut.get_Count() == 1);
        }

        [Test]
        public void NoEntryCanBeFetchedFromEmptyServer()
        {
            Assert.IsTrue(sut.get_Count() == 0);
        }

        [Test]
        public void IfOneEntryIsAddedOneCanBeFetched()
        {
            sut.Add(new BaseCalendarEntry());
            Assert.IsTrue(sut.get_Count() == 1);
        }

        [Test]
        public void IfEntryIsAddedItCanFetched()
        {
            string title = "test";
            sut.Add(getCalendarEntryWithTitle(title));
            Assert.IsTrue(sut.get_Count() == 1);
          //  Assert.IsTrue(data.First<BaseCalendarEntry>().Title == title);
        }
        [Test]
        public void IfEntryIsRemovedFromEmptyServerCountOfEntriesIsStillEqual_0()
        {
            sut.Remove(someEntry);
            Assert.IsTrue(sut.get_Count() == 0);
        }
        [Test]
        public void If_WeAddEntryToEmptyServerAndThenRemoveThisSameEntry_ThenEntriesListIsStillEmpty()
        {
            sut.Add(someEntry);
            sut.Remove(someEntry);
            Assert.IsTrue(sut.get_Count() == 0);
        }
        [Test]
        public void If_we_remove_entry_then_we_can_not_fetch_this_entry()
        {
            sut.Add(getDefaultCalendarEntry());
            sut.Add(someEntry);
            sut.Add(getDefaultCalendarEntry());

            sut.Remove(someEntry);
            Assert.IsTrue(sut.get_Count() == 2);

        }



    }
}
