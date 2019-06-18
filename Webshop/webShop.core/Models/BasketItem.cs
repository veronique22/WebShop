using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webShop.core.Models
{
    public class BasketItem : BaseEntities
    {
        public string BasketID { get; set; }
        public string ProductID { get; set; }  // il est préférable ici de garder uniquement l'id du produit et pas le produit entier
       // ceci facilitera l'entretien. si jamais un produit est changé à la base, tout sera facilement et automatiquement mis à jour dans le panier

        public int Quantity { get; set; }

    }
}
