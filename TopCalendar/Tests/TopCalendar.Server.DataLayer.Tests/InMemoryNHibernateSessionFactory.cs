#region

using System.Data;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

#endregion

namespace TopCalendar.Server.DataLayer.Tests
{
    public class InMemoryNHibernateSessionFactory : NHibernateSessionFactory
    {
        public InMemoryNHibernateSessionFactory()
        {
            DbConfig = SQLiteConfiguration.Standard.InMemory().ShowSql()				
            .Provider<SQLiteInMemoryTestConnectionProvider>();
            DropDataBaseOnStart = true;
        }

		public void SchemaExport()
		{
			SchemaExport(Configuration);
		}

		public void CloseConnection()
		{
			
		}
    }

    public class SQLiteInMemoryTestConnectionProvider :
    NHibernate.Connection.DriverConnectionProvider
    {
        private static IDbConnection _connection;

        public override IDbConnection GetConnection()
        {
            if (_connection == null)
                _connection = base.GetConnection();
            return _connection;
        }

        public override void CloseConnection(IDbConnection conn)
        {
        }

        /// <summary>
        /// Destroys the connection that is kept open in order to 
        /// keep the in-memory database alive.  Destroying
        /// the connection will destroy all of the data stored in 
        /// the mock database.  Call this method when the
        /// test is complete.
        /// </summary>
        public static void ExplicitlyDestroyConnection()
        {
            if (_connection != null)
            {
                _connection.Close();
                _connection = null;
            }
        }
    }
}