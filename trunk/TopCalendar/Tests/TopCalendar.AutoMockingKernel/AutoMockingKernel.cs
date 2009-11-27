using System;
using System.Collections.Generic;
using Ninject;
using Ninject.Activation;
using Ninject.Infrastructure;
using Ninject.Planning.Bindings;
using Rhino.Mocks;


namespace TopCalendar.NinjectAutoMockingKernel
{
	public class AutoMockingKernel : StandardKernel, IAutoMockingRepository
	{
		private readonly IList<Type> _markMissing;

		public bool CanResolveFromMockRepository(Type service)
		{
			return (!this._markMissing.Contains(service) &&
			        (this.GetMockingStrategy(service).GetType() != typeof (NonMockedStrategy)));
		}


		private Dictionary<Type, IMockingStrategy> _strategies = new Dictionary<Type, IMockingStrategy>();
		private Dictionary<Type, object> _services = new Dictionary<Type, object>();
		private MockRepository _mocks;

		private Dictionary<Type, object> _bindedTypes = new Dictionary<Type, object>();
		private IMockingStrategy _defaultMockingStrategy;


		public AutoMockingKernel(MockRepository mocks)
		{
			_mocks = mocks;
		}

		private void AutoMockingKernel_BindingRemoved(IBinding binding)
		{
		    _bindedTypes.Remove(binding.Service);
		}

		private void AutoMockingKernel_BindingAdded(IBinding binding)
		{
		    _bindedTypes[binding.Service] = binding.Target;
		}

		public bool IsBinded<T>()
		{
			return IsBinded(typeof (T));
		}

		private bool IsBinded(Type type)
		{
			return _bindedTypes.ContainsKey(type);
		}

		private void AddComponentIfMissing<T>()
		{
			if (!IsBinded<T>())
			{
				Bind<T>().ToSelf();
			}
		}

		public T Create<T>()
		{
			AddComponentIfMissing<T>();
			var result = ResolutionExtensions.Get<T>(this);
			AddService(result.GetType(), result);
			return result;
		}

		public IMockingStrategy GetMockingStrategy(Type type)
		{
			if (this._strategies.ContainsKey(type))
			{
				return this._strategies[type];
			}
			return this.DefaultMockingStrategy;
		}

		public IMockingStrategy DefaultMockingStrategy
		{
			get
			{
				if (_defaultMockingStrategy == null)
				{
					_defaultMockingStrategy = new DynamicMockingStrategy(this);
				}
				return _defaultMockingStrategy;
			}
			set { _defaultMockingStrategy = value; }
		}

		public MockRepository MockRepository
		{
			get { return _mocks; }
		}

		public void AddService(Type type, object service)
		{
			_services[type] = service;
		}

		public object GetService(Type type)
		{
			if (_services.ContainsKey(type))
				return _services[type];
			return null;
		}

		public T GetService<T>()
		{
			return (T) GetService(typeof (T));
		}

		public IKernel Kernel
		{
			get { return this; }
		}

		public void MarkStub(Type type)
		{
			_strategies[type] = new StubbedStrategy(this);
		}

		public void MarkDynamickMock(Type type)
		{
			_strategies[type] = new DynamicMockingStrategy(this);
		}

		public void MarkDynamickMock<T>()
		{
			MarkDynamickMock(typeof(T));
		}

		public void MarkNonDynamic(Type type)
		{
			_strategies[type] = new StandardMockingStrategy(this);
		}

		public void MarkNonMocked(Type type)
		{
			_strategies[type] = new NonMockedStrategy(this);
		}

		public void MarkStub<T>()
		{
			MarkStub(typeof (T));
		}

		public void MarkNonDynamic<T>()
		{
			MarkNonDynamic(typeof (T));
		}

		public void MarkNonMocked<T>()
		{
			MarkNonMocked(typeof (T));
		}

		protected override bool HandleMissingBinding(Type service)
		{
			if (service == null)
				throw new ActivationException("Null service type");

			object target = GetService(service);
			Binding binding;
			if (null != target)
			{
				binding = new Binding(service) {IsImplicit = true};
				AddBinding(binding);
				return true;
			}


			//if (service.ContainsGenericParameters)
			//    return false;

			binding = new Binding(service)
			          	{
			          		ProviderCallback = c => new MockingProvider(this, service),
			          		IsImplicit = true,
							ScopeCallback = StandardScopeCallbacks.Singleton
			          	};
			AddBinding(binding);
			return true;
		}

		public override void AddBinding(IBinding binding)
		{
			base.AddBinding(binding);
			AutoMockingKernel_BindingAdded(binding);
		}

		public override void RemoveBinding(IBinding binding)
		{
			base.RemoveBinding(binding);
			AutoMockingKernel_BindingRemoved(binding);
		}
		
	}

	internal class MockingProvider : IProvider
	{
		private readonly IAutoMockingRepository _mockingRepository;
		private Type _type;

		public MockingProvider(IAutoMockingRepository mockingRepository, Type service)
		{
			_mockingRepository = mockingRepository;
			_type = service;
		}

		public object Create(IContext context)
		{
			var strategy = _mockingRepository.GetMockingStrategy(Type);
			var target = strategy.Create(context, Type);
			_mockingRepository.AddService(Type, target);
			return target;
		}

		public Type Type
		{
			get { return _type; }
		}
	}
}