using System;
using System.Data.Linq;
using System.Linq;
using AutoMapper;
using DataGenerator.Extensions;

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

		public Data FindDate(DateTime date)
		{
			var d = Datas.Where(dat => dat.PK_Date.Equals(date.Date)).FirstOrDefault();
            Check.Require(d != null, "Date not found {0}".AsFormat(date));			
			return d;
		}
	}
}