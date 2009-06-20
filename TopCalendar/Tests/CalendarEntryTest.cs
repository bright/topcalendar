using System;
using System.Text;
using System.Collections.Generic;
using ClientApp.RemoteServerRef;
using NUnit.Framework;
using Rhino.Mocks;
using Rhino.Mocks.Constraints;
using ClientApp;
using ServerLib;
using ServerLib.Data;

namespace ClientTest
{
    /// <summary>
    /// Summary description for CalendarEntryTest
    /// </summary>
    [TestFixture]
    public class CalendarEntryTest
    {
      /*  private MockRepository mocks;
        private BaseCalendarEntry sut;

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
            sut = new BaseCalendarEntry(title);
            Assert.IsTrue(sut.Title == title);
        }
        [Test]
        public void DateIsInitializedProperly()
        {
            DateTime expected = DateTime.Now;
            
            sut = new BaseCalendarEntry(null,expected);

            Assert.AreEqual(0, DateTime.Compare(expected, sut.DateTime));
        }

        [Test]
        public void ConversionToDbEventIsValid()
        {
            sut = new BaseCalendarEntry("title", DateTime.Now);

            DbEntry db = sut;

            Assert.AreEqual(sut.Title, db.Title);
            Assert.AreEqual(sut.DateTime, db.DateFrom);
            Assert.AreEqual(sut.Id, db.Id);
        }

        [Test]
        public void ConversionFromDbEventIsValid()
        {
            DbEntry db = DbEntry.CreateDbEntry(5, "title", DateTime.Now, 6);

            sut = db;

            Assert.AreEqual(sut.Title, db.Title);
            Assert.AreEqual(sut.DateTime, db.DateFrom);
            Assert.AreEqual(sut.Id, db.Id);
        }*/
    }
}
