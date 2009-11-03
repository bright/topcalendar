using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using TopCalendar.Server.DataLayer.Entities;

namespace TopCalendar.Server.DataLayer
{
    public class NHibernateSessionFactory
    {
        private const string DbFile = "TopCalendarDataBase.db";
        private static bool DropDataBaseOnStart = false;

        public static ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
              .Database(
                SQLiteConfiguration.Standard
                  .UsingFile(DbFile)
              )
              .Mappings(m =>
                m.FluentMappings.AddFromAssemblyOf<User>())
              .ExposeConfiguration(BuildSchema)
              .BuildSessionFactory();
        }

        private static void BuildSchema(Configuration config)
        {
            // delete the existing db on each run
            if (DropDataBaseOnStart &&  File.Exists(DbFile))
                File.Delete(DbFile);

            // this NHibernate tool takes a configuration (with mapping info in)
            // and exports a database schema from it
            bool script = false;
            bool export = true;
            new SchemaExport(config).Create(script, export);
        }

    }
}
