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
    [TestFixture]
    public class RemoteServerTest : ServerTestBase
    {

        [SetUp()]
        public override void SetUp()
        {
            mocks = new MockRepository();
            someEntry = getDefaultCalendarEntry();

            sut = new RemoteServer();
        }

    }
}
