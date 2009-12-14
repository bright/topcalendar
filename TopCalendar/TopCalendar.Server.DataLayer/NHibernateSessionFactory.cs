#region

using System.IO;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using TopCalendar.Server.DataLayer.Entities;

#endregion

namespace TopCalendar.Server.DataLayer
{
    public class NHibernateSessionFactory
    {
        private const string DbFile = "TopCalendarDataBase.db";
        protected virtual bool DropDataBaseOnStart { get; set; }

        protected virtual IPersistenceConfigurer DbConfig { get; set; }

        public NHibernateSessionFactory()
        {
            DbConfig = SQLiteConfiguration.Standard.UsingFile(DbFile);
            DropDataBaseOnStart = false;
        }

        public ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
                .Database(DbConfig)
                .Mappings(m =>
                          m.FluentMappings.AddFromAssemblyOf<User>())
                .ExposeConfiguration(BuildSchema)
                .BuildSessionFactory();
        }

        private void BuildSchema(Configuration config)
        {
            // delete the existing db on each run
            if (DropDataBaseOnStart && File.Exists(DbFile))
                File.Delete(DbFile);

            // this NHibernate tool takes a configuration (with mapping info in)
            // and exports a database schema from it
            bool script = true;
            bool export = true;

            if (DropDataBaseOnStart)
            {
                new SchemaExport(config).Create(script, export);
            }
        }
    }
}