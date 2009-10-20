using System;
using Ninject.Activation;

namespace TopCalendar.NinjectAutoMockingKernel
{
	public interface IMockingStrategy
	{
		object Create(IContext context, Type type);
	}
}