using System.Collections.Generic;

namespace HostManager.Contracts
{
    public interface IRepository<T>
    {
        T Get(int Id);
        IEnumerable<T> GetAll();
        bool Add(T Item);
        bool Delete(int Id);
        bool Edit(T Item);
        T Find(T Item);
        T FindById(int Id);
    }
}
