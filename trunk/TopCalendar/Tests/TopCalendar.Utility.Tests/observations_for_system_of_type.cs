using System;
using AutoMapper;
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
		public void Setup()
		{
			EstablishContext();						
			Because();
		}

		/// <summary>
		/// Wywolywana po kazdym tescie
		/// </summary>
		protected virtual void AfterEachObservation()
		{
		}

		[TearDown]
		private void TearDown()
		{
			AfterEachObservation();
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
		
		protected virtual void AfterSutCreation()
		{
		}
		/// <summary>
		/// Tworzy testowany system
		/// </summary>
		/// <returns>Testowany system</returns>
		protected abstract TSut CreateSut();

	}	
	
	public abstract class observations_for_auto_created_sut_of_type<TSut>
		: observations_for_sut_of_type<TSut>
		where TSut : class
	{
		private MockRepository _mocks;
		private AutoMockingKernel _mockingKernel;

		[SetUp]
		public new void Setup()
		{
			_mocks = new MockRepository();
			_mockingKernel = new AutoMockingKernel(_mocks);
			_mockingKernel.DefaultMockingStrategy = new ReplayedMockingStrategy(_mockingKernel);
			_mockingKernel.Bind<IServiceLocator>().To<NinjectServiceLocator>().InSingletonScope();
			ServiceLocator.SetLocatorProvider(() => _mockingKernel.Get<IServiceLocator>());
			base.Setup();
		}

/*
		protected IAutoMockingRepository AutoMockingRepository { get { return _mockingKernel; } }
*/

/*
		protected IKernel Kernel { get { return _mockingKernel; } }		
*/
		
		/// <summary>
		/// Instruuje AutoMockingKernel ze typ ma byc stubem
		/// jesli jednak typ byl juz zbindowany to zostanie zwrocony obiekt zgodnie z poprzednim bindingiem
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		protected T Stub<T>()
		{
			_mockingKernel.MarkStub<T>();
			return _mockingKernel.Get<T>();
		}

		/// <summary>
		/// Generuje stuba w oderwaniu od automocking kernela
		/// </summary>
		/// <typeparam name="T">Typ stuba</typeparam>
		/// <returns>Stub typu t</returns>
		protected T GenerateStub<T>()
		{
			return _mocks.Stub<T>();
		}


		protected void MarkNonMocked<T>()
		{
			_mockingKernel.MarkNonMocked(typeof(T));
		}

		protected bool IsTypeBinded<T>()
		{
			return _mockingKernel.IsBinded<T>();
		}

/*
		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		protected T DynamickMock<T>()
		{
			_mockingKernel.MarkDynamickMock<T>();
			return _mockingKernel.Get<T>();
		}
*/


		/// <summary>
		/// Binduje typ do sta³ej
		/// </summary>
		/// <typeparam name="TType"></typeparam>
		/// <param name="implementation"></param>
		protected void ProvideImplementationOf<TType>(TType implementation)
		{
			_mockingKernel.Bind<TType>().ToConstant(implementation);
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


	public abstract class observations_for_mapping_configuration_defined_in<TMappingDefinition> : observations_for_mapping_configuration
		where TMappingDefinition : IBootstrapperTask
	{
		protected override void EstablishContext()
		{
			TasksRunner.Get().Execute<TMappingDefinition>();
			base.EstablishContext();
		}
	}

	public abstract class observations_for_mapping_configuration : observations_for_sut
	{
		protected override void AfterEachObservation()
		{
			base.AfterEachObservation();
			Mapper.Reset();
		}

		protected void assert_mapper_configuration_is_valid()
		{
			Mapper.AssertConfigurationIsValid();
		}
	}
}