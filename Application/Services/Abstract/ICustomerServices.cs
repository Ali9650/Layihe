using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Abstract
{
    public interface ICustomerServices
    {
        public void BuyProduct();
        public void ShowBoughProducts();
        public void ShowOrderByDate();
        public void FilterProduct();
    }
}
