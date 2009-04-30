using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClientApp;

namespace ClientTest
{
    /// <summary>
    /// Summary description for CalendarEntryTest
    /// </summary>
    [TestClass]
    public class CalendarEntryTest
    {
        private CalendarEntry sut;

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
            //sut = new CalendarEntry();
        }

        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //

        [TestMethod]
        public void TitleIsSavedProperly()
        {
            string title = "test";
            sut = new CalendarEntry(title);
            Assert.IsTrue(sut.Title == title);
        }
    }
}
