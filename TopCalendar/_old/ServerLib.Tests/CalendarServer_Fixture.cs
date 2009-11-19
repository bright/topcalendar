using System;
using NUnit.Framework;
using Rhino.Mocks;
using ServerLib.Domain;

namespace ServerLib.Tests
{
    [TestFixture]
    public class CalendarServer_Fixture
    {
        private readonly MockRepository _mocks = new MockRepository();
        private IBaseCalendarEntryRepository _baseCalendarEntryRepositoryMock;
        private CalendarServer _sut;

        #region Setup/Teardown

        private BaseCalendarEntry _testBaseCalendarEntry =
            new BaseCalendarEntry
                {
                    Title = "Test Title",
                    Desc = "Test description",
                    DateTime = new DateTime(2009,01,01)
                };

        [SetUp]
        public void SetupContext()
        {
            _baseCalendarEntryRepositoryMock = _mocks.DynamicMock<IBaseCalendarEntryRepository>();
            _sut = new CalendarServer(_baseCalendarEntryRepositoryMock);
        }

        #endregion

        [Test]
        public void When_adding_new_element_repository_add_method_should_be_fired()
        {
            using (_mocks.Record())
            {
                Expect.Call(() => _baseCalendarEntryRepositoryMock.Add(null)).IgnoreArguments();
            }
            using (_mocks.Playback())
            {
                _sut.Add(_testBaseCalendarEntry);
            }
        }

        [Test]
        public void When_updating_existing_element_repository_update_method_should_be_fired()
        {
            using (_mocks.Record())
            {
                Expect.Call(() => _baseCalendarEntryRepositoryMock.Update(null)).IgnoreArguments();
            }
            using (_mocks.Playback())
            {
                _sut.EntryEdited(_testBaseCalendarEntry);
            }
        }

        [Test]
        public void When_removing_existing_element_repository_remove_method_should_be_fired()
        {
            using (_mocks.Record())
            {
                Expect.Call(() => _baseCalendarEntryRepositoryMock.Remove(null)).IgnoreArguments();
            }
            using (_mocks.Playback())
            {
                _sut.Remove(_testBaseCalendarEntry);
            }
        }
    }
}