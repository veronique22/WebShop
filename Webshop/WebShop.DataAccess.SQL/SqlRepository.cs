using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webShop.core.Models;
using WebShop.core.Interface;

namespace WebShop.DataAccess.SQL
{
    public class SqlRepository<T> : IRepository<T> where T : BaseEntities
    {
        internal DbContext context = new DataContext();
        internal DbSet<T> dbset;

        public SqlRepository()
        {
            this.dbset = context.Set<T>();
        }

        public void Commit()
        {
            context.SaveChanges();
        }

        public bool Delete(string id)
        {
            var item = FindById(id);

            if (item is null)
            {
                return false;
            }

            if (context.Entry(item).State == EntityState.Detached)
            {
                dbset.Attach(item);
            }

            context.Entry(item).State = EntityState.Deleted;
            dbset.Remove(item);
            Commit();
            return true;
        }

        public T FindById(string id) => dbset.FirstOrDefault(x => x.Id == id);

        public IQueryable<T> Collection() => dbset;

        public void Insert(T item)
        {
            dbset.Add(item);
        }

        public void Update(T item)
        {
            var itemToEdit = FindById(item.Id);
            if (itemToEdit is null)
            {
                throw new Exception(item.GetType().Name + " not found");
            }

            else
            {
                context.Entry(item).State = EntityState.Modified;
                itemToEdit = item;
                Commit();
            }
        }
    }
}

