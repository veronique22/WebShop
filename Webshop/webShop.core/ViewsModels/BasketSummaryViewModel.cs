using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webShop.core.ViewsModels
{
    public class BasketSummaryViewModel
    {
        public int BasketCount  { get; set; }
        public decimal BasktetTotal { get; set; }

        public BasketSummaryViewModel(int BasketCount, decimal BasktetTotal)
        {
            this.BasketCount = BasketCount;
            this.BasktetTotal = BasktetTotal;
        }
    }
}
