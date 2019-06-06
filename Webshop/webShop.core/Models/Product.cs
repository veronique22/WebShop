using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webShop.core.Models
{
    public class Product
    {
        public Product()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }
        [DisplayName("Product Name")]
        public string Name { get; set; }
        public string Description { get; set; }
        [Range(typeof(decimal)," 0,00","10000,00"), DataType(DataType.Currency)]
        public decimal Price { get; set; }
        public string Category { get; set; }
        public string Image { get; set; }

    }
}
