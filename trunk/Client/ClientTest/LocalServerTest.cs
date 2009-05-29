using System;
using System.Text;
using System.Collections.Generic;
using NUnit.Framework;
using Rhino.Mocks;
using Rhino.Mocks.Constraints;
using ClientApp;
using System.Linq;
using ClientApp.Ninject;

namespace ClientTest
{
    /// <summary>
    /// Summary description for LocalServerTest
    /// </summary>
    [TestFixture]
    public class LocalServerTest : ServerTestBase
    {

        [SetUp()]
        public override void SetUp()
        {
            mocks = new MockRepository();
            someEntry = getDefaultCalendarEntry();
            //sut = Factory.Resolve<IServer>();
            sut = new LocalServer();
        }

    }
}
