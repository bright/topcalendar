using System;
using NUnit.Framework;
using Rhino.Mocks;

namespace TopCalendar.NinjectAutoMockingKernel
{
	public interface ICollectionOfServices
	{
		IDisposable SomethingToDispose { get; }
	}

	public interface IReallyCoolService
	{
		void SayHello();
	}

	public class ComponentBeingConfigured
	{
		#region Member Data

		public IReallyCoolService ReallyCoolService;
		public ICollectionOfServices Services;

		#endregion

		#region ComponentBeingConfigured()

		public ComponentBeingConfigured(IReallyCoolService reallyCoolService, ICollectionOfServices services)
		{
			this.ReallyCoolService = reallyCoolService;
			this.Services = services;
		}

		#endregion

		#region Methods

		public void RunDispose()
		{
			this.Services.SomethingToDispose.Dispose();
		}

		#endregion
	}

	public class DefaultCollectionOfServices : ICollectionOfServices
	{
		#region ICollectionOfServices Members

		public IDisposable SomethingToDispose
		{
			get { return null; }
		}

		#endregion
	}

	public class AutoMockingTests
	{
		#region Member Data

		protected MockRepository _mocks;
		protected AutoMockingKernel _container;

		#endregion

		#region Test Setup and Teardown Methods

		[SetUp]
		public virtual void Setup()
		{
			_mocks = new MockRepository();
			_container = new AutoMockingKernel(_mocks);
		}

		#endregion
	}

	[TestFixture]
	public class AutoMockingContainerTests : AutoMockingTests
	{
		#region Test Methods

		[Test]
		public void Resolving_WithComponent_ReturnsMock()
		{
			ComponentBeingConfigured target = _container.Create<ComponentBeingConfigured>();

			using (_mocks.Unordered())
			{
				target.ReallyCoolService.SayHello();
			}

			_mocks.ReplayAll();
			target.ReallyCoolService.SayHello();
			_mocks.VerifyAll();

			_container.Dispose();
		}

		[Test]
		public void Resolving_WithOtherImplementation_ReturnsMock()
		{
			//_container.AddComponent("DefaultCollectionOfServices", typeof(ICollectionOfServices), typeof(DefaultCollectionOfServices));
			_container.Bind<ICollectionOfServices>().To<DefaultCollectionOfServices>();
			ComponentBeingConfigured target = _container.Create<ComponentBeingConfigured>();

			_mocks.ReplayAll();
			Assert.IsInstanceOfType(typeof (DefaultCollectionOfServices), target.Services);
			_mocks.VerifyAll();

			_container.Dispose();
		}

		[Test]
		public void Resolving_WithComponentWithStub_ReturnsMock()
		{
			_container.MarkStub<ICollectionOfServices>();
			ComponentBeingConfigured target = _container.Create<ComponentBeingConfigured>();

			using (_mocks.Unordered())
			{
				target.Services.SomethingToDispose.Dispose();
			}

			_mocks.ReplayAll();
			target.RunDispose();
			_mocks.VerifyAll();

			_container.Dispose();
		}

		[Test]
		public void Resolving_GetTwice_ReturnsSameMock()
		{
			_container.MarkStub<ICollectionOfServices>();
			ComponentBeingConfigured target1 = _container.Create<ComponentBeingConfigured>();
			ComponentBeingConfigured target2 = _container.GetService<ComponentBeingConfigured>();

			Assert.AreEqual(target1, target2);
			Assert.AreEqual(target1.ReallyCoolService, target2.ReallyCoolService);
		}

		[Test]
		public void Get_NotGotten_ReturnsNull()
		{
			Assert.IsNull(_container.GetService<IReallyCoolService>());
		}

		[Test]
		public void Get_AlreadyGotten_ReturnsMock()
		{
			ComponentBeingConfigured target = _container.Create<ComponentBeingConfigured>();
			Assert.AreEqual(target.ReallyCoolService, _container.GetService<IReallyCoolService>());
		}

		#endregion
	}
}