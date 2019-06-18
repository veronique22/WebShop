﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webShop.core.Models;

namespace WebShop.DataAccess.SQL
{
    public class DataContext : DbContext

    {

        public DataContext() : base("name=DefaultConnection")

        {

            Configuration.LazyLoadingEnabled = false;

        }



        public DbSet<Product> Products { get; set; }

        public DbSet<ProductCategory> Categories { get; set; }

        public DbSet<Basket> Baskets { get; set; }

        public DbSet<BasketItem> BasketItems { get; set; }


    }
}
