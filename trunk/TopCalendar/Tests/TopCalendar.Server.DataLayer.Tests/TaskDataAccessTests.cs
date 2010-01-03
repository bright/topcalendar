using System;
using NUnit.Framework;
using TopCalendar.Server.DataLayer.Entities;
using TopCalendar.Utility.Tests;

namespace TopCalendar.Server.DataLayer.Tests
{
	public class when_adding_plugin_data_to_task : observations_with_in_memory_database_of_sut_of_type<Task>
	{
		private byte[] _data;
		private Guid _pluginIdentifier;
	

		protected override void Because()
		{
			WithEntityInDatabaseDo<Task>(Sut.Id,
				s=> s.AddPluginData(_pluginIdentifier, _data)
			);
		}

		protected override Task CreateSut()
		{						
			return Persist(
					New.Task().WithUser(
						Persist(New.User().Build())
					).Build()
				);			
		}

		protected override void EstablishContext()
		{
			base.EstablishContext();
			_pluginIdentifier = Guid.NewGuid();
			_data = new byte[]{1,2};
		}

		[Test]
		public void should_save_plugin_identifier_properly()
		{
			WithEntityInDatabaseDo<Task>(Sut.Id,(t)=> t.PluginDatas.ShouldContain(pd=> pd.PluginIdentifier.Equals(_pluginIdentifier)));
		}

		[Test]
		public void should_save_plugin_data_properly()
		{
			WithEntityInDatabaseDo<Task>(Sut.Id, t => t.PluginDatas.ShouldContain(pd => pd.Data[0].Equals(1) && pd.Data[1].Equals(2)));
		}
	}

}