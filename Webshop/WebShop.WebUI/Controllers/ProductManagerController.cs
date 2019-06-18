using System;
using System.Collections.Generic;
using System.IO;
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

        public ProductManagerController(IRepository<Product> ProductContext, IRepository<ProductCategory> CategoryContext)
        {
            context = ProductContext;
            productCategoryContext = CategoryContext;
        }

        public ActionResult Index()
        {
            List<Product> products = context.Collection().ToList();

            return View(products);
        }
        [HttpGet]
        public ActionResult Create()
        {
            viewModel.Product = new Product();
            viewModel.ProductCategories = productCategoryContext.Collection();
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult Create(ProductManagerViewModel model, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    model.Product.Image = model.Product.Id + Path.GetExtension(file.FileName);
                    string path = Server.MapPath("~/Content/ProductImages/" + model.Product.Image);
                    file.SaveAs(path);
                }

                context.Insert(model.Product);
                context.Commit();
                return RedirectToAction("Index");
            }

            else
            {
                viewModel.ProductCategories = productCategoryContext.Collection();
                return View(model);
            }
            //if (ModelState.IsValid == false)
            //{
            //    return View(product);
            //}
            //else
            //{
            //    if (file != null)

            //    {

            //        product.Image = product.Id + Path.GetExtension(file.FileName);

            //        file.SaveAs(Server.MapPath("~/Content/ProductImages/" +  product.Image);

            //    }
            //    context.Insert(product);
            //    context.Commit();  // cette methode que nous avons crée nous permet de sauver le produit dans le cache. 
            //}
            //return RedirectToAction("Index");

        }
        [HttpGet]
        public ActionResult Delete(string Id)
        {
            var productToDelete = context.FindById(Id);
            if (productToDelete is null)
            {
                return HttpNotFound();
            }
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
            viewModel.ProductCategories = productCategoryContext.Collection();
            return View(viewModel);
        }
        [HttpPost]
        [ActionName("Edit")]

        public ActionResult Editnew(ProductManagerViewModel model, HttpPostedFileBase file)
        {
            var editProduct = context.FindById(model.Product.Id);
            if (ModelState.IsValid)

            {

                if (file != null)

                {

                    model.Product.Image = model.Product.Id + Path.GetExtension(file.FileName);

                    string path = Server.MapPath("~/Content/ProductImages/" + model.Product.Image);
                    file.SaveAs(path);

                }

                context.Update(editProduct);

                //context.Delete(product.Id);     //delete old product

                //context.Insert(product);        // insert new

                context.Commit();
                return RedirectToAction("Index"); ;

            }
            else
            {
                return View(model);
            }
        }
    }
}