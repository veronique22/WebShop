using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webShop.core.Models;
using WebShop.core.Interface;
using WebShop.DataAcces.InMemory.Repositories;

namespace WebShop.WebUI.Controllers
{
    public class ProductCategoryManagerController : Controller
    {
        private IRepository<ProductCategory> context;
        public ProductCategoryManagerController(IRepository<ProductCategory>CategoryContext)
        {
            context = CategoryContext;
        }
        public ActionResult Index()
        {
            List<ProductCategory> categories = context.Collection().ToList();

            return View(categories);
        }
        [HttpGet]
        public ActionResult Create()
        {
            ProductCategory category = new ProductCategory();
            return View(category);
        }
        [HttpPost]
        public ActionResult Create(ProductCategory ProductCategory)
        {
            if (ModelState.IsValid == false)
            {
                return View(ProductCategory);
            }
            else
            {
                context.Insert(ProductCategory);
                context.Commit();  // cette methode que nous avons crée nous permet de sauver le produit dans le cache. 
            }
            return RedirectToAction("Index");

        }
        [HttpGet]
        public ActionResult Delete(string Id)
        {
            var CategorieToDelete = context.FindById(Id);
            return View(CategorieToDelete);
        }
        [HttpDelete]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            var CategorieToDelete = context.Delete(Id);
            if (CategorieToDelete == false)
            {
                return HttpNotFound();
            }
            else
            {
                return RedirectToAction("Index");
            }

        }
        [HttpGet]
        public ActionResult Edit(string ID)
        {
            var CategorieToUpdate = context.FindById(ID);
            return View(CategorieToUpdate);
        }
        [HttpPost]
        [ActionName("Edit")]

        public ActionResult Editnew(ProductCategory ProductCategory)
        {
            context.Update(ProductCategory);
            //context.Delete(product.Id);
            //context.Insert(product);
            context.Commit();
            return RedirectToAction("Index"); ;
        }
    }
}
