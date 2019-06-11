using System.Linq;
using webShop.core.Models;

namespace WebShop.core.Interface
{
    public interface IRepository<T> where T : BaseEntities
    {
        void Commit();
        bool Delete(string Id);
        T FindById(string Id);
        IQueryable<T> GetCategory();
        void Insert(T t);
        void Update(T t);
    }
}