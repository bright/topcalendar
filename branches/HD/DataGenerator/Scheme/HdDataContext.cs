using System;
using System.Data.Linq;
using System.Linq;
using AutoMapper;

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

		public Data FindDateOrInsertNew(DateTime date)
		{
			var d = Datas.Where(dat => dat.PK_Date.Equals(date.Date)).FirstOrDefault();
			if(d == null)
			{
				d = Mapper.Map<DateTime, Data>(date);
				Datas.InsertOnSubmit(d);
				SubmitChanges();
			}
			return d;
		}
	}
}