using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace TopCalendar.UI.Modules.Plugins.Services
{
	public class AssemblyLoader
	{
		public Type Load(string path)
		{
			AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += ReflectionOnlyAssemblyResolve;
			var assembly = Assembly.ReflectionOnlyLoadFrom(path);
			var module = (from item in assembly.GetTypes()
						  where item.GetInterface("IModule") != null
						  select item).FirstOrDefault();

			return module;
		}

		private Assembly ReflectionOnlyAssemblyResolve(object sender, ResolveEventArgs args)
		{
			return Assembly.ReflectionOnlyLoad(args.Name);
		}
	}
}
