using System;
using Ninject;

namespace TopCalendar.Server.Bootstrap.NinjectWcf
{
    public static class KernelContainer
    {
        private static IKernel _kernel;
        /// <summary>
        /// Gets or sets the kernel that is used in the application.
        /// </summary>
        public static IKernel Kernel
        {
            get { return _kernel; }
            set
            {
                if (_kernel != null)
                    throw new NotSupportedException("The static container already has a kernel associated with it!");
                _kernel = value;
            }
        }
        /// <summary>
        /// Injects the specified instance by using the container's kernel.
        /// </summary>
        /// <param name="instance">The instance to inject.</param>
        public static void Inject(object instance)
        {
            if (_kernel == null)
            {
                throw new InvalidOperationException(String.Format(
                                                        "The type {0} requested an injection, but no kernel has been registered for the web application.\r\n" +
                                                        "Please ensure that your project defines a NinjectHttpApplication.",
                                                        instance.GetType()));
            }
            _kernel.Inject(instance);
        }
    }
}