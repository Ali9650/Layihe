using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Abstract
{
    public interface ISellerServices
    {
        public void AddProduct();
        public void ChangeProductCount();
        public void DeleteProduct();
        public void ShowOrders();
        public void ShowOrderByDate();
        public void FilterProduct();
        public void GetTotalAmounts();
    }
}
