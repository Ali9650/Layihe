using Core.Entities;
using Core.Messages;
using Data.UnitOfWork.Abstract;
using Data.UnitOfWork.Concrete;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Application.Services.Concrete
{
    public class SellerServices
    {
        private readonly UnitOfWork _unitOfWork;
        private Seller _seller;

        public SellerServices()
        {
            _unitOfWork = new UnitOfWork();
            _seller = null;
        }


        public bool Login()
        {
        LoginInput: Messages.InputMessages("email");
            string email = Console.ReadLine();
            Messages.InputMessages("pasword");

            string password = Console.ReadLine();
            //if (string.IsNullOrWhiteSpace(email)||string.IsNullOrWhiteSpace(password))
            //{
            //    Messages.InvalidInputMeesages("email or password incorrect");
            //    goto LoginInput;
            //}
            var existSeller = _unitOfWork.Sellers.GetSellerWithEmailAndPassword(email, password);
            if (existSeller is null)
            {
                Messages.InvalidInputMeesages("email or password");
                return false;
            }
            _seller = existSeller;
            return true;
        }
        public void AddProduct()
        {

        IdInput: Messages.InputMessages("id");

            _unitOfWork.Categories.GetAll();
            string idInput = Console.ReadLine();
            int id;
            bool isSucceeded = int.TryParse(idInput, out id);
            if (!isSucceeded)
            {
                Messages.InvalidInputMeesages("id");
                goto IdInput;
            }
            var existCategory = _unitOfWork.Categories.Get(id);
            if (existCategory == null)
            {
                Messages.NotFountMessage("id");
                goto IdInput;
            }
        ProductNameInput: string name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name))
            {
                Messages.InvalidInputMeesages("Product Name");
                goto ProductNameInput;
            }
            var existProduct = _unitOfWork.Sellers.GetExistProductWithNameAndSellerId(name, _seller.Id);
            if (existProduct is not null)
            {
                Messages.AlreadyExistMessage("product");
                return;
            }
        ProductCountInput: string countInput = Console.ReadLine();
            int count;
            isSucceeded = int.TryParse(countInput, out count);
            if (!isSucceeded)
            {
                Messages.InvalidInputMeesages("count");
                goto ProductCountInput;
            }
        ProductPriceInput: string priceinput = Console.ReadLine();
            int price;
            isSucceeded = int.TryParse(priceinput, out price);
            if (!isSucceeded)
            {
                Messages.InvalidInputMeesages("price");
                goto ProductPriceInput;
            }
            var product = new Product
            {
                Name = name,
                Count = count,
                Price = price,
                CategoryId = id,
                SellerId = _seller.Id
            };

            _unitOfWork.Products.Add(product);
            Messages.SuccessMessages("product", "added");
        }

        public void ChangeProductCount()
        {
            Messages.InputMessages("product id");
            foreach (var product in _unitOfWork.Sellers.GetProductsBySellerId(_seller.Id))
            {
                Console.WriteLine($"Product Id : {product.Id}  Product name : {product.Name} Product count : {product.Count}");
            }
        ProductIdInput: Messages.InputMessages("product id");
            string IdInput = Console.ReadLine();
            int productId;
            bool issucceeded = int.TryParse(IdInput, out productId);
            if (!issucceeded)
            {
                Messages.InvalidInputMeesages("product id");
                goto ProductIdInput;
            }
            var existProduct = _unitOfWork.Products.Get(productId);
            if (existProduct is null)
            {
                Messages.NotFountMessage("product");
                goto ProductIdInput;
            }
        Productcountinput: Messages.InputMessages("new count");
            string countInput = Console.ReadLine();
            int newcount;
            issucceeded = int.TryParse(countInput, out newcount);
            if (!issucceeded || newcount < 1)
            {
                Messages.InvalidInputMeesages(" new count");
                goto Productcountinput;
            }
            existProduct.Count = newcount;
            _unitOfWork.Products.Update(existProduct);
            _unitOfWork.Commit();
            Messages.SuccessMessages("product count", "updated");
        }
        public void DeleteProduct()
        {
            Messages.InputMessages("product id");
            foreach (var product in _unitOfWork.Sellers.GetProductsBySellerId(_seller.Id))
            {
                Console.WriteLine($"Product Id : {product.Id}  Product name : {product.Name} Product count : {product.Count}");
            }
        ProductIdInput: Messages.InputMessages("product id");
            string IdInput = Console.ReadLine();
            int productId;
            bool issucceeded = int.TryParse(IdInput, out productId);
            if (!issucceeded)
            {
                Messages.InvalidInputMeesages("product id");
                goto ProductIdInput;
            }
            var existProduct = _unitOfWork.Products.Get(productId);
            if (existProduct is null)
            {
                Messages.NotFountMessage("product");
                return;
            }
            _unitOfWork.Products.Delete(existProduct);
            _unitOfWork.Commit();
            Messages.SuccessMessages("product", "deleted");
        }
        public void FilterProduct()
        {
            filtersymbol: Messages.InputMessages("name symbol");
            string symbol = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(symbol))
            {
                Messages.InvalidInputMeesages("name symbol");
                goto filtersymbol;
            }
            var existProducts=_unitOfWork.Sellers.GetProductBySymbol(symbol,_seller.Id);
            if (existProducts is null)
            {
                Messages.NotFountMessage("product");
                return;
            }
            foreach (var product in existProducts)
            {
                Console.WriteLine($" Name : {product.Name} Count {product.Count}  price {product.Price}");
            }

        }

        public void ShowOrders()
        {
            var existOrders = _unitOfWork.Orders.GetOrderBySellerId(_seller.Id);
            if (!existOrders.Any())
            {
                Messages.NotFountMessage("order");
                return;
            }
            foreach (var order in existOrders)
            {
                Console.WriteLine($" Product Name : {order.Product.Name}" +
                    $" Customer name {order.Customer.Name} product price {order.Product.Price} " +
                    $"product count {order.Product.Count}");
            }
        }

        public void ShowOrderByDate()
        {
            DateInput: Messages.InputMessages("date (dd.MM.yyyy)");
            string dateInput = Console.ReadLine();
            DateTime date;
            bool isSucceeded = DateTime.TryParseExact(dateInput,"dd.MM.yyyy",CultureInfo.InvariantCulture,DateTimeStyles.None,out date);
            if (!isSucceeded)
            {
                Messages.InvalidInputMeesages("date");
                goto DateInput;
            }
            var orders = _unitOfWork.Orders.GetOrdersBySellerCreateDate(date, _seller.Id);
            if (orders is null)
            {
                Messages.NotFountMessage("any orders");
                return;
            }
            foreach (var order in orders)
            {
                Console.WriteLine($" Name : {order.Product.Name} Count {order.Product.Count}  price {order.Product.Price} create date {order.CreateAt}");
            }
        }

        public void GetTotalAmounts()
        {
            decimal totalAmount = 0;
            foreach (var order in _unitOfWork.Orders.GetOrderBySellerId(_seller.Id))
            {
                totalAmount += order.Count * order.Product.Price;
            }

            Console.WriteLine($" Total Amount : {totalAmount}");
        }
    }
}
