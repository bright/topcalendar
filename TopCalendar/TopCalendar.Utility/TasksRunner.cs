using System;
using System.Linq;
using System.Reflection;
using TopCalendar.Utility.BasicExtensions;

namespace TopCalendar.Utility
{
	public class TasksRunner
	{
		public void Execute<TTask>()
			where TTask : IBootstrapperTask
		{
			Execute(typeof(TTask));
		}

		public void Execute(Type t)
		{
			var task = Activator.CreateInstance(t) as IBootstrapperTask;
			if(task != null) Execute(task);
		}

		public void Execute(IBootstrapperTask task)
		{
			task.Execute();
		}

		public void FromAssemblyOf<TType>()
		{
			var assembly = typeof (TType).Assembly;
			assembly.GetTypes()
				.Where(t => t.IsSubclassOf(typeof (IBootstrapperTask)))
				.Each(Execute);
		}

		public static TasksRunner Get()
		{
			return new TasksRunner();
		}
	}
}