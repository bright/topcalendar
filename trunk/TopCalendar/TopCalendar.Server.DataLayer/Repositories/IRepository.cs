using System.Collections.Generic;

namespace TopCalendar.Server.DataLayer.Repositories
{
    public interface IRepository<T, TPk>
    {
        T GetById(TPk id);
        IList<T> GetAll();
        T Add(T entity);
        void Remove(T entity);
    }
}