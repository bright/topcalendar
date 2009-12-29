using AutoMapper;
using TopCalendar.UI.Modules.Notifier.Services.EmailNotifier;
using TopCalendar.Utility;

namespace TopCalendar.UI.Modules.Notifier.Services
{
	public class MappingConfiguration : IBootstrapperTask
	{
		public void Execute()
		{
			Mapper.CreateMap<SmtpServerConfiguration, SmtpServerConfiguration>();
			Mapper.CreateMap<ISmtpServerConfiguration, SmtpServerConfiguration>();
		}
	}
}