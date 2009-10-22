using ClientApp.DateTimeExtensions;
using NUnit.Framework;
using System;

namespace ClientTest
{
    
    
    /// <summary>
    ///This is a test class for DateTimeExtesionsTest and is intended
    ///to contain all DateTimeExtesionsTest Unit Tests
    ///</summary>
    [TestFixture]
    public class DateTimeExtesionsTest
    {


        /// <summary>
        ///A test for NextMonth
        ///</summary>
        [Test]
        public void NextMonth()
        {
            DateTime dt = new DateTime(1986, 11, 11);
            DateTime expected = new DateTime(1986, 12, 1);
            DateTime actual = dt.NextMonth();
            Assert.AreEqual(expected, actual);            
        }
        [Test]
        public void NextMonthDecember() {
            DateTime dt = new DateTime(1986, 12, 1);
            DateTime expected = new DateTime(1987, 1, 1);
            DateTime actual = dt.NextMonth();
            Assert.AreEqual(expected, actual);            
        }
        [Test]
        public void PrevMonth() {
            DateTime dt = new DateTime(1986, 11, 1);
            DateTime expected = new DateTime(1986, 10, 1);
            DateTime actual = dt.PrevMonth();
            Assert.AreEqual(expected, actual);                  
        }
        [Test]
        public void PrevMonthJanuary() {
            DateTime dt = new DateTime(2008, 1, 10);
            DateTime expected = new DateTime(2007, 12, 1);
            DateTime actual = dt.PrevMonth();
            Assert.AreEqual(expected, actual);                          
        }
    }
}
