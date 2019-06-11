using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using webShop.core.Models;
using WebShop.core.Interface;

namespace WebShop.DataAcces.InMemory.Repositories
{
   public class InMemoryRepository <T> : core.Interface.IRepository<T> where T: BaseEntities
    {
        ObjectCache cache = MemoryCache.Default;
        List<T> Items;
        string ClassName;

        public InMemoryRepository()
        {
            ClassName = typeof(T).Name;
            Items = cache["ClassName"] as List<T>;
            if (Items == null)
            {
                Items = new List<T>();
            }
        }
        public void Commit()
        {
            cache["ClassName"] = Items;
        }

        public void Insert(T t)
        {
            Items.Add(t);
        }
        public void Update(T t)
        {
            T tToUpdate = Items.Find(i => i.Id == t.Id);

            if (tToUpdate != null)
            {
                int index = Items.FindIndex(i =>i.Id == tToUpdate.Id);
                Items[index] = t;
            }
            else
            {
                throw new Exception(ClassName + "not found");
            }
        }
        public T FindById (string Id)
        {
            T tToFind = Items.Find(p => p.Id == Id);
            if (tToFind != null)
            {
                return tToFind;

            }
            else
            {
                throw new Exception(ClassName + " not found");
            }
        }
        public IQueryable<T> GetCategory()
        {
            return Items.AsQueryable();
        }
        public bool Delete(string Id)
        {
            var tToDelete = FindById(Id);
            return Items.Remove(tToDelete);

        }
    }
}
