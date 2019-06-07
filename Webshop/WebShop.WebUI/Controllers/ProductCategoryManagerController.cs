using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webShop.core.Models;
using WebShop.DataAcces.InMemory.Repositories;

namespace WebShop.WebUI.Controllers
{
    public class ProductCategoryManagerController : Controller
    {
        public ProductCategoryManagerController()
        {
            context = new CategoryRepository();
        }
        CategoryRepository context;
        public ActionResult Index()
        {
            List<ProductCategory> categories = context.GetCategory().ToList();

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
            var CategorieToDelete = context.FindCategory(Id);
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
            var CategorieToUpdate = context.FindCategory(ID);
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
