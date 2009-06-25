using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;
using ServerLib.Repositories;
using ServerLib.Domain;

namespace ServerLib.Tests
{
    [TestFixture]
    public class BaseCalendarEntryRepository_Fixture
    {
        #region Setup/Teardown

        [SetUp]
        public void SetupContext()
        {
            new SchemaExport(_configuration).Execute(false, true, false, false);
            CreateInitialData();
            _repository = new BaseCalendarEntryRepository();
        }

        #endregion

        private ISessionFactory _sessionFactory;
        private Configuration _configuration;
        private IBaseCalendarEntryRepository _repository;

        private readonly BaseCalendarEntry[] _initialData =
            new[]
                {
                    new BaseCalendarEntry
                        {
                            Title = "Kupić rower",
                            Desc = "Najlepiej taki o dwóch kołach",
                            DateTime = new DateTime(2009, 08, 01)
                        },
                    new BaseCalendarEntry
                        {
                            Title = "Posprzątać dom",
                            Desc = "Kuchnię przede wszystkim",
                            DateTime = new DateTime(2009, 08, 01)
                        },
                    new BaseCalendarEntry
                        {
                            Title = "Siłownia",
                            Desc = "Czwartek o 14",
                            DateTime = new DateTime(2009, 08, 01)
                        },
                    new BaseCalendarEntry
                        {
                            Title = "Zrobić zakupy",
                            Desc = "Nie zapomnij o słodyczach",
                            DateTime = new DateTime(2009, 08, 02)
                        },
                    new BaseCalendarEntry
                        {
                            Title = "Wywiesić pranie",
                            Desc = "Ale to nie musi być dzisiaj",
                            DateTime = new DateTime(2006, 01, 01)
                        },
                    new BaseCalendarEntry
                        {
                            Title = "Pójść do sklepu",
                            Desc = "Rowerowego i kupić pompkę",
                            DateTime = new DateTime(2010, 12, 31)
                        },
                };

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            _configuration = new Configuration();
            _configuration.Configure();
            _configuration.AddAssembly(typeof (BaseCalendarEntry).Assembly);
            _sessionFactory = _configuration.BuildSessionFactory();
        }

        private void CreateInitialData()
        {
            using (ISession session = _sessionFactory.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                foreach (BaseCalendarEntry element in _initialData)
                    session.Save(element);
                transaction.Commit();
            }
        }

        /// <summary>
        /// Sprawdza czy obiekt fromDb zawiera takie same wartosci jak baseCalendarEntry
        /// Sprawdza czy obie referencje nie wskazuja na dokladnie ten sam obiekt
        /// </summary>
        /// <param name="baseCalendarEntry"></param>
        /// <param name="fromDb"></param>
        private void AssertIsSuccessfullyFetchedFromDb(BaseCalendarEntry baseCalendarEntry, BaseCalendarEntry fromDb)
        {
            Assert.IsNotNull(fromDb);
            Assert.AreNotSame(baseCalendarEntry, fromDb);
            Assert.AreEqual(baseCalendarEntry.Id, fromDb.Id);
            Assert.AreEqual(baseCalendarEntry.Title, fromDb.Title);
            Assert.AreEqual(baseCalendarEntry.Desc, fromDb.Desc);
            Assert.AreEqual(baseCalendarEntry.DateTime, fromDb.DateTime);
            //    Assert.AreEqual(baseCalendarEntry, fromDb);
        }

        private bool IsInCollection(BaseCalendarEntry baseCalendarEntry, ICollection<BaseCalendarEntry> fromDb)
        {
            foreach (BaseCalendarEntry item in fromDb)
                if (baseCalendarEntry.Id == item.Id)
                    return true;
            return false;
        }

        [Test]
        public void Can_add_new_BaseCalendarEntry()
        {
            var baseCalendarEntry = new BaseCalendarEntry
                                        {
                                            Title = "Nowe zadanie",
                                            Desc = "Jakis opis",
                                            DateTime = new DateTime(2009, 05, 05, 12, 41, 32)
                                        };

            _repository.Add(baseCalendarEntry);

            // sprobujemy znalezc w bazie zapisany wlasnie rekord
            using (ISession session = _sessionFactory.OpenSession())
            {
                var fromDb = session.Get<BaseCalendarEntry>(baseCalendarEntry.Id);
                // sprawdzenie czy odnaleziony rekord jest taki sam jak przed zapisem
                AssertIsSuccessfullyFetchedFromDb(baseCalendarEntry, fromDb);
            }
        }

        [Test]
        public void test()
        {
            var c = DateTime.Today.DayOfWeek.ToString();
            Console.Write(c);
        }

        [Test]
        public void Can_find_all_exisitng_BaseCalendarEntries()
        {
            var results = _repository.FindAll();

            Assert.That(results.Count == _initialData.Length);
        }

        [Test]
        public void Can_find_exisitng_BaseCalendarEntry_by_Id()
        {

            var fromDb = _repository.FindById(_initialData[0].Id);

                AssertIsSuccessfullyFetchedFromDb(_initialData[0], fromDb);
            
        }

        [Test]
        public void Can_find_existing_BaseCalendarEntries_by_day()
        {
            IList<BaseCalendarEntry> results = _repository.FindByDay(2009, 08, 01);
            Assert.That(results.Count == 3);

            Assert.That(IsInCollection(_initialData[0], results));
            Assert.That(IsInCollection(_initialData[1], results));
            Assert.That(IsInCollection(_initialData[2], results));
        }

        [Test]
        public void Can_update_existing_BaseCalendarEntry()
        {
            BaseCalendarEntry beforeUpdate = _repository.FindById(_initialData[0].Id);

            AssertIsSuccessfullyFetchedFromDb(_initialData[0], beforeUpdate);

            beforeUpdate.Title = "zmieniony tytul";
            beforeUpdate.Desc = "zmieniony opis";

            _repository.Update(beforeUpdate);

            BaseCalendarEntry afterUpdate = _repository.FindById(beforeUpdate.Id);

            AssertIsSuccessfullyFetchedFromDb(beforeUpdate, afterUpdate);
        }
    }
}