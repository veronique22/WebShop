﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Caching;
using System.Threading.Tasks;
using webShop.core.Models;

namespace WebShop.DataAcces.InMemory.Repositories
{
    public class ProductRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<Product> products;
        public ProductRepository()
        {
            products = cache["products"] as List<Product>;
            if (products == null)
            {
                products = new List<Product>();
            }
        }

        public void Commit()
        {
            cache["products"] = products;
        }
        public void Insert(Product p)
        {
            products.Add(p);
        }
        public void Update(Product product)
        {
            Product productsToUpdate = products.Find(p => p.Id == product.Id);

            if (productsToUpdate != null)
            {
                productsToUpdate = product;
            }
            else
            {
                throw new Exception("product not found");
            }
        }

        public Product FindProduct(string Id)
        {
            Product productsToFind = products.Find(p => p.Id == Id);
            if (productsToFind != null)
            {
                return productsToFind;

            }
            else
            {
                throw new Exception("product not found");
            }
        }

        public IQueryable<Product> GetProducts()
        {
            return products.AsQueryable();
        }
        public bool Delete(string Id)
        {
            var productToDelete = FindProduct(Id);
            products.Remove(productToDelete);

            return true;
        }
    }
}
