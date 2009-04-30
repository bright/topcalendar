using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClientApp;

namespace ClientTest
{
    /// <summary>
    /// Summary description for LocalServerTest
    /// </summary>
    [TestClass]
    public class LocalServerTest
    {
        private LocalServer sut;

        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        
        // Use TestInitialize to run code before running each test 
        [TestInitialize]
        public void TestInitialize()
        {
            sut = new LocalServer();
        }
        
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //

        [TestMethod]
        public void NewServerShouldHaveNoEntries()
        {
            Assert.IsTrue(sut.Count == 0);
        }

        [TestMethod]
        public void NewItemCanBeAddedAndCounterIncreases()
        {
            sut.Add(new CalendarEntry());
            Assert.IsTrue(sut.Count == 1);
        }

        [TestMethod]
        public void NoEntryCanBeFetchedFromEmptyServer()
        {
            var data = from x in sut select x;
            Assert.IsTrue(data.Count<CalendarEntry>() == 0);
        }

        [TestMethod]
        public void IfOneEntryIsAddedOneCanBeFetched()
        {
            sut.Add(new CalendarEntry());
            var data = from x in sut select x;
            Assert.IsTrue(data.Count<CalendarEntry>() == 1);
        }

        [TestMethod]
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
