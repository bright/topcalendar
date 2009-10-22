using System;
using CommonServiceLocator.NinjectAdapter;
using Microsoft.Practices.ServiceLocation;
using Ninject;
using NUnit.Framework;
using Rhino.Mocks;
using TopCalendar.NinjectAutoMockingKernel;

namespace TopCalendar.Utility.Tests
{
	/// <summary>
	/// Bazowa klasa dla testow zgodna z AAA
	/// </summary>
	[TestFixture]
	public abstract class observations_for_sut
	{
		[SetUp]
		public void setup()
		{
			EstablishContext();						
			Because();
		}

		/// <summary>
		/// Wywolywana po kazdym tescie
		/// </summary>
		[TearDown]
		protected virtual void AfterEachObservation()
		{
		}

		/// <summary>
		/// Przeprowadz test (Act)
		/// </summary>
		protected abstract void Because();

		/// <summary>
		/// Przygotuwuje œrodowisko do testowania (Arrange)
		/// </summary>
		protected virtual void EstablishContext()
		{
		}
	
	}


	/// <summary>
	/// Bazowa klasa testow konkretnej klasy, zgodna z AAA
	/// </summary>
	/// <typeparam name="TSut">Testowana klasa</typeparam>
	[TestFixture]
	public abstract class observations_for_sut_of_type<TSut>
		: observations_for_sut
	{

		protected TSut Sut;
		
		[SetUp]
		public new void Setup()
		{
			EstablishContext();
			Sut = CreateSut();
			AfterSutCreation();
			Because();
		}

		/// <summary>
		/// Wywolywana po kazdym tescie
		/// </summary>
		[TearDown]
		protected virtual void AfterEachObservation()
		{
		}
				
		protected virtual void AfterSutCreation()
		{
		}
		/// <summary>
		/// Tworzy testowany system
		/// </summary>
		/// <returns>Testowany system</returns>
		protected abstract TSut CreateSut();

	}	
	[TestFixture]
	public abstract class observations_for_auto_created_sut_of_type<TSut>
		: observations_for_sut_of_type<TSut>
		where TSut : class
	{
		private MockRepository _mocks;
		private AutoMockingKernel _mockingKernel;

		[SetUp]
		public void setup()
		{
			_mocks = new MockRepository();
			_mockingKernel = new AutoMockingKernel(_mocks);
			_mockingKernel.DefaultMockingStrategy = new ReplayedMockingStrategy(_mockingKernel);
			Kernel.Bind<IServiceLocator>().To<NinjectServiceLocator>().InSingletonScope();
			ServiceLocator.SetLocatorProvider(() => Kernel.Get<IServiceLocator>());
			base.Setup();
		}

		protected IAutoMockingRepository AutoMockingRepository { get { return _mockingKernel; } }

		protected IKernel Kernel { get { return _mockingKernel; } }		
				
		protected T Stub<T>()
		{
			_mockingKernel.MarkStub<T>();
			return _mockingKernel.Get<T>();
		}

		protected T DynamickMock<T>()
		{
			_mockingKernel.MarkDynamickMock<T>();
			return _mockingKernel.Get<T>();
		}



		protected T Dependency<T>()
			where T : class 
		{
			T result;
			try
			{
				result = _mockingKernel.Get<T>();
				if(!_mocks.IsInReplayMode(result))
				{
					_mocks.Replay(result);
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}


		protected override void AfterEachObservation()
		{									
			_mockingKernel.Dispose();						
			_mocks = null;
			_mockingKernel = null;
			base.AfterEachObservation();
		}

		protected override TSut CreateSut()
		{
			return _mockingKernel.Create<TSut>();
		}
	}

}