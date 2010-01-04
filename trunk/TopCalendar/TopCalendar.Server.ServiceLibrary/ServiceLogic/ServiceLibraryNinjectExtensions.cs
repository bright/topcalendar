using Ninject;
using Ninject.Syntax;

namespace TopCalendar.Server.ServiceLibrary.ServiceLogic.NinjectExtensions
{
	public static class ServiceLibraryNinjectExtensions
	{
		public static IBindingWhenInNamedWithOrOnSyntax<IRequestToResponseLogic<TRequest, TResponse>> BindRequestToResponseLogic<TServiceLogic, TRequest, TResponse>(this IKernel kernel) where TServiceLogic : IRequestToResponseLogic<TRequest, TResponse>
		{
			 return kernel.Bind<IRequestToResponseLogic<TRequest, TResponse>>().To<TServiceLogic>();
		}
	}
}