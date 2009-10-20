using System;
using System.Reflection;
using Ninject;
using Ninject.Activation;
using Rhino.Mocks;

namespace TopCalendar.NinjectAutoMockingKernel
{
	public class NonMockedStrategy : AbstractMockingStrategy
	{
		#region NonMockedStrategy()

		public NonMockedStrategy(IAutoMockingRepository autoMock) : base(autoMock)
		{
		}

		#endregion

		#region IMockingStrategy Members

		public override object Create(IContext context, Type type)
		{
			return AutoMock.Kernel.Get(type);
		}

		#endregion
	}
}