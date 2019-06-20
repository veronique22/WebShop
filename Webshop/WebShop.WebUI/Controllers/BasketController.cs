using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShop.Services.ServicesModel;

namespace WebShop.WebUI.Controllers
{
    public class BasketController : Controller
    {
        IBasketService basketService;

        public BasketController(IBasketService basketService)
        {
            this.basketService = basketService;
        }
        // GET: Basket
        public ActionResult Index()
        {
            var model = basketService.GetBasketItems(HttpContext);
            return View(model);
        }
        public ActionResult AddToBasket(string Id)
        {
            basketService.addToBasket(HttpContext, Id);

            return RedirectToAction("Index");
        }
        public ActionResult RemoveFromBasket(string Id)
        {
            basketService.removeFromBasket(HttpContext, Id);

            return RedirectToAction("Index");
        }
        public PartialViewResult Basketsummary()
        {
            var basketSummary = basketService.BasketSummary(HttpContext);

            return PartialView(basketSummary);
        }
    }
}