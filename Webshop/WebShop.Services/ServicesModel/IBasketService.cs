using System.Collections.Generic;
using System.Web;
using webShop.core.ViewsModels;

namespace WebShop.Services.ServicesModel
{
    public interface IBasketService
    {
        void addToBasket(HttpContextBase httpContext, string productId);
        void removeFromBasket(HttpContextBase httpContext, string ItemId);
        List<BasketItemViewModel> GetBasketItems(HttpContextBase httpContext);
        BasketSummaryViewModel BasketSummary(HttpContextBase httpContext);
    }
}