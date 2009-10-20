using System;
using System.Reflection;
using Ninject.Activation;
using Rhino.Mocks;

namespace TopCalendar.NinjectAutoMockingKernel
{
	public class DynamicMockingStrategy : AbstractMockingStrategy
	{
		#region DynamicMockingStrategy()

		public DynamicMockingStrategy(IAutoMockingRepository autoMock) : base(autoMock)
		{
		}

		#endregion

		#region IMockingStrategy Members

		public override object Create(IContext context, Type type)
		{
			return this.Mocks.DynamicMock(type);
		}

		#endregion
	}
}