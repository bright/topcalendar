using System;
using System.Reflection;
using Ninject.Activation;
using Rhino.Mocks;

namespace TopCalendar.NinjectAutoMockingKernel
{
	public abstract class AbstractMockingStrategy : IMockingStrategy
	{
		#region Member Data

		private IAutoMockingRepository _autoMock;

		#endregion

		#region Properties

		public IAutoMockingRepository AutoMock
		{
			get { return _autoMock; }
		}

		public MockRepository Mocks
		{
			get { return _autoMock.MockRepository; }
		}

		#endregion

		#region AbstractMockingStrategy()

		public AbstractMockingStrategy(IAutoMockingRepository autoMock)
		{
			_autoMock = autoMock;
		}

		#endregion

		#region IMockingStrategy Members

		public virtual object Create(IContext context, Type type)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}