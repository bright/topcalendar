using System;
using Ninject;
using Rhino.Mocks;

namespace TopCalendar.NinjectAutoMockingKernel
{
	public interface IAutoMockingRepository
	{
		IMockingStrategy GetMockingStrategy(Type type);
		MockRepository MockRepository { get; }
		void AddService(Type type, object service);
		object GetService(Type type);
		T GetService<T>();
		IKernel Kernel { get; }
		void MarkStub(Type type);
		void MarkStub<T>();
		void MarkDynamickMock<T>();
		void MarkDynamickMock(Type type);
		void MarkNonDynamic(Type type);
		void MarkNonMocked(Type type);
	}
}