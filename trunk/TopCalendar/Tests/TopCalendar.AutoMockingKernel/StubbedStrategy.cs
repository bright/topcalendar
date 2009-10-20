using System;
using Ninject.Activation;

namespace TopCalendar.NinjectAutoMockingKernel
{
	public class StubbedStrategy : AbstractMockingStrategy
	{
		#region Member Data

		private StandardMockingStrategy _default;

		#endregion

		#region StubbedStrategy()

		public StubbedStrategy(IAutoMockingRepository autoMock)
			: base(autoMock)
		{
			_default = new StandardMockingStrategy(autoMock);
		}

		#endregion

		#region IMockingStrategy Members

		public override object Create(IContext context, Type type)
		{
			object target = Mocks.Stub(type);
			AutoMock.AddService(type, target);
/*      foreach (PropertyInfo property in type.GetProperties())
      {
        IMockingStrategy strategy = this.AutoMock.GetMockingStrategy(property.PropertyType);
        object value = strategy.Create(context, property.PropertyType);
        Expect.Call(property.GetValue(target, null)).Repeat.Any().Return(value);
      }
      this.Mocks.Replay(target);*/
			return target;
		}

		#endregion
	}
}