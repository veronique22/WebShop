using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using webShop.core.Models;

namespace WebShop.DataAcces.InMemory.Repositories
{
    public class CategoryRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<ProductCategory> Categories;
        public CategoryRepository()
        {
            Categories = cache["Categories"] as List<ProductCategory>;
            if (Categories == null)
            {
                Categories = new List<ProductCategory>();
            }
        }

        public void Commit()
        {
            cache["Categories"] = Categories;
        }
        public void Insert(ProductCategory p)
        {
            Categories.Add(p);
        }
        public void Update(ProductCategory product)
        {
            ProductCategory categorieToUpdate = Categories.Find(p => p.Id == product.Id);

            if (categorieToUpdate != null)
            {
                int index = Categories.FindIndex(p => p.Id == categorieToUpdate.Id);
                Categories[index] = product;
            }
            else
            {
                throw new Exception("product not found");
            }
        }

        public ProductCategory FindCategory(string Id)
        {
            ProductCategory CategoryToFind = Categories.Find(p => p.Id == Id);
            if (CategoryToFind != null)
            {
                return CategoryToFind;

            }
            else
            {
                throw new Exception("product not found");
            }
        }

        public IQueryable<ProductCategory> GetCategory()
        {
            return Categories.AsQueryable();
        }
        public bool Delete(string Id)
        {
            var categoryToDelete = FindCategory(Id);
            Categories.Remove(categoryToDelete);

            return true;
        }
    }
}
