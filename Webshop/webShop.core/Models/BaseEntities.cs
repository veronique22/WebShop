using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webShop.core.Models
{
   public abstract class BaseEntities
    {
        public string Id { get; set; }

        public DateTime CreateAt { get; set; }   
        // ces deux propriétes ici nous permettent d'enregistrer par exemple quand est ce que nos objets sont crées. 
        //public DateTime UpdateAt { get; set; }


        public BaseEntities()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreateAt = DateTime.Now;
           // this.UpdateAt = DateTime.Now;
        }
    }
}
