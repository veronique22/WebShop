using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webShop.core.Models;
using webShop.core.ViewsModels;
using WebShop.core.Interface;
using WebShop.DataAcces.InMemory.Repositories;

namespace WebShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        // GET: ProductManager
        IRepository<Product> context;
        IRepository<ProductCategory> productCategoryContext;
        ProductManagerViewModel viewModel = new ProductManagerViewModel();

        public ProductManagerController(IRepository<Product> ProductContext, IRepository<ProductCategory>CategoryContext)
        {
            context = ProductContext;
            productCategoryContext = CategoryContext;
        }

        public ActionResult Index()
        {
            List<Product> products = context.GetCategory().ToList();

            return View(products);
        }
        [HttpGet]
        public ActionResult Create()
        {
            viewModel.Product = new Product();
            viewModel.ProductCategories = productCategoryContext.GetCategory();
            return View(viewModel);
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
            var productToDelete = context.FindById(Id);
            return View(productToDelete);
        }
        [HttpDelete]
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
        [HttpGet]
        public ActionResult Edit(string Id)
        {
            var productToUpdate = context.FindById(Id);
            if (productToUpdate == null)
            {
                return HttpNotFound();
            }
            viewModel.Product = productToUpdate;
            viewModel.ProductCategories = productCategoryContext.GetCategory();
            return View(viewModel);
        }
        [HttpPost]
        [ActionName("Edit")]

        public ActionResult Editnew(Product product)
        {
            context.Update(product);
            //context.Delete(product.Id);
            //context.Insert(product);
            context.Commit();
            return RedirectToAction("Index"); ;
        }
    }
}