using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using webShop.core.Models;
using webShop.core.ViewsModels;
using WebShop.core.Interface;

namespace WebShop.Services.ServicesModel
{
    public class BasketService : IBasketService
    {
        IRepository<Product> productContext;
        IRepository<Basket> BasketContext;
        public const string BasketSessionName = "ICommerceBasket";


        public BasketService(IRepository<Product> Pcontext, IRepository<Basket> BContext)
        {
            productContext = Pcontext;
            BasketContext = BContext;
        }

        private Basket GetBasket(HttpContextBase httpContext, bool createIfNull) // HttpContexBase est la classe qui va sauvegarder l'ensemble des informations locales du client. 
        {
            HttpCookie cookie = httpContext.Request.Cookies.Get(BasketSessionName); // permet de lire les cookies
            Basket basket = new Basket();
            if (cookie != null)
            {
                string BasketId = cookie.Value;
                if (string.IsNullOrEmpty(BasketId) == false) // donc si le string existe et que nous avons une valeur de BasketId
                {
                    basket = BasketContext.FindById(BasketId);
                }
                else
                {
                    if (createIfNull)
                    {
                        basket = CreateNewBasket(httpContext);
                    }
                }
            }
            else
            {
                basket = CreateNewBasket(httpContext);
            }

            return basket;
        }

        private Basket CreateNewBasket(HttpContextBase httpContext)
        {

            Basket basket = new Basket();
            BasketContext.Insert(basket);
            BasketContext.Commit();
            HttpCookie cookie = new HttpCookie(BasketSessionName);
            cookie.Value = basket.Id;
            cookie.Expires = DateTime.Now.AddDays(2); // le panier sera sauvegardé pendant 2 jours et après il expirera.

            return basket;

        }
        public void addToBasket(HttpContextBase httpContext, string productId)
        {
            Basket basket = GetBasket(httpContext, true);
            BasketItem item = basket.BasketItems.FirstOrDefault(i => i.ProductID == productId);

            if (item == null)
            {
                item = new BasketItem()
                {
                    BasketID = basket.Id,
                    ProductID = productId,
                    Quantity = 1
                };
                basket.BasketItems.Add(item);
            }
            else
            {
                item.Quantity = item.Quantity + 1;
            }
            BasketContext.Commit();
        }

        public void removeFromBasket(HttpContextBase httpContext, string ItemId)
        {

            Basket basket = GetBasket(httpContext, true);
            BasketItem item = basket.BasketItems.FirstOrDefault(i => i.Id == ItemId);

            if (item != null)
            {
                basket.BasketItems.Remove(item);
                BasketContext.Commit();

            }

        }


        public List<BasketItemViewModel> GetBasketItems(HttpContextBase httpContext)
        {
            Basket basket = GetBasket(httpContext, false);

            if (basket != null)
            {
                var results = (from b in basket.BasketItems
                               join p in productContext.Collection()
                               on b.ProductID equals p.Id
                               select new BasketItemViewModel()
                               {
                                   Id = b.Id,
                                   Quantity = b.Quantity,
                                   ProductName = p.Name,
                                   Image = p.Image,
                                   Price = p.Price

                               }).ToList();
                return results;
            }
            else
            {
                return new List<BasketItemViewModel>();
            }

        }

        public BasketSummaryViewModel BasketSummary(HttpContextBase httpContext)
        {
            Basket basket = GetBasket(httpContext, false);
            BasketSummaryViewModel model = new BasketSummaryViewModel(0, 0);
            if (basket!=null)
            {
                int? basketCount = (from item in basket.BasketItems
                                    select item.Quantity).Sum();

                decimal? BasketTotal = (from item in basket.BasketItems
                                        join p in productContext.Collection()
                                        on item.ProductID
                                        equals p.Id
                                        select item.Quantity * p.Price).Sum();

                model.BasketCount = basketCount ?? 0; // ici nous disons si il a une valeur, montrer la valeur, si non 
                                                      // afficher 0 comme valeur

                return model;
            }

            else
            {
                return model;
            }
        }
    }
}
