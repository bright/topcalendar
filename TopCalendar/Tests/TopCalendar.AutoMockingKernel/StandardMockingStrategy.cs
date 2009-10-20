using System;
using System.Reflection;
using Ninject.Activation;
using Rhino.Mocks;

namespace TopCalendar.NinjectAutoMockingKernel
{
	public class StandardMockingStrategy : AbstractMockingStrategy
	{
		#region StandardMockingStrategy()

		public StandardMockingStrategy(IAutoMockingRepository autoMock) : base(autoMock)
		{
		}

		#endregion

		#region IMockingStrategy Members

		public override object Create(IContext context, Type type)
		{
			return Mocks.StrictMock(type);
		}

		#endregion
	}

	public class ReplayedMockingStrategy : AbstractMockingStrategy
	{
		public ReplayedMockingStrategy(IAutoMockingRepository autoMock) : base(autoMock)
		{
		}

		public override object Create(IContext context, Type type)
		{
			object target = Mocks.DynamicMock(type);
			Mocks.Replay(target);
			return target;
		}
	}
}