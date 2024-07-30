using Core.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Abstract
{
    public interface IAdminService
    {
        public void GetAllSellers();
        public void GetAllCustomers();
        public void CreateSeller();
        public void CreateCustomer();
        public void DeleteSeller();
        public void DeleteCustomer();
        public void CreateCategory();
        public void OrdersByDesc();
        public void OrdersBySeller();
        public void OrdersByCustomer();
        public void GetOrdersByCreateDate();

    }
}
