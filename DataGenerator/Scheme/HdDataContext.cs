using System;
using System.Data.Linq;

namespace DataGenerator.Scheme
{
	public partial class HdDataContext
	{
		public void DeleteAllAndSubmit<TEntity>(Table<TEntity> table)
			where TEntity : class 
		{
			table.DeleteAllOnSubmit(table);
			SubmitChanges();
		}
	}	
}