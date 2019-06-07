using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webShop.core.Models;
using WebShop.DataAcces.InMemory.Repositories;

namespace WebShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        // GET: ProductManager
        public ProductManagerController()
        {
            context = new ProductRepository();
        }
        ProductRepository context;
        public ActionResult Index()
        {
            List<Product> products = context.GetProducts().ToList();

            return View(products);
        }
        [HttpGet]
        public ActionResult Create()
        {
            Product product = new Product();
            return View(product);
        }
        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid == false)
            {
                return View(product);
            }
            else
            {
                context.Insert(product);
                context.Commit();  // cette methode que nous avons crée nous permet de sauver le produit dans le cache. 
            }
            return RedirectToAction("Index");

        }
        [HttpGet]
        public ActionResult Delete(string Id)
        {
            var productToDelete = context.FindProduct(Id);
            return View(productToDelete);
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            var productToDelete = context.Delete(Id);
            if (productToDelete == false)
            {
                return HttpNotFound();
            }
            else
            {
                return RedirectToAction("Index");
            }
            
        }
    }
}