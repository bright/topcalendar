using ClientApp.DateTimeExtensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ClientTest
{
    
    
    /// <summary>
    ///This is a test class for DateTimeExtesionsTest and is intended
    ///to contain all DateTimeExtesionsTest Unit Tests
    ///</summary>
    [TestClass()]
    public class DateTimeExtesionsTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for NextMonth
        ///</summary>
        [TestMethod()]
        public void NextMonth()
        {
            DateTime dt = new DateTime(1986, 11, 11);
            DateTime expected = new DateTime(1986, 12, 1);
            DateTime actual = dt.NextMonth();
            Assert.AreEqual(expected, actual);            
        }
        [TestMethod]
        public void NextMonthDecember() {
            DateTime dt = new DateTime(1986, 12, 1);
            DateTime expected = new DateTime(1987, 1, 1);
            DateTime actual = dt.NextMonth();
            Assert.AreEqual(expected, actual);            
        }
        [TestMethod]
        public void PrevMonth() {
            DateTime dt = new DateTime(1986, 11, 1);
            DateTime expected = new DateTime(1986, 10, 1);
            DateTime actual = dt.PrevMonth();
            Assert.AreEqual(expected, actual);                  
        }
        [TestMethod]
        public void PrevMonthJanuary() {
            DateTime dt = new DateTime(2008, 1, 10);
            DateTime expected = new DateTime(2007, 12, 1);
            DateTime actual = dt.PrevMonth();
            Assert.AreEqual(expected, actual);                          
        }
    }
}
