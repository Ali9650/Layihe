using Application.Services.Abstract;
using Core.Entities;
using Core.Extensions;
using Core.Messages;
using Data.UnitOfWork.Abstract;
using Data.UnitOfWork.Concrete;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Application.Services.Concrete
{
    public class SellerServices :ISellerServices
    {
        private readonly UnitOfWork _unitOfWork;
        private Seller _seller;

        public SellerServices(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _seller = null;
        }

        public bool Login()
        {
        LoginEMailInput: Messages.InputMessages("email");
           string email = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(email) || !email.IsValidEMail())
            {
                Messages.InvalidInputMeesages("email");
                goto LoginEMailInput;
            }
            var existSeller = _unitOfWork.Sellers.GetSellerByEmail(email);
            if (existSeller is null)
            {
                Messages.InvalidInputMeesages("email or password");
                return false;
            }
        LoginPasswordInput: Messages.InputMessages("pasword");

            string password = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(password))
            {
                Messages.InvalidInputMeesages(" password ");
                goto LoginPasswordInput;
            }
           

            PasswordHasher<Seller> passwordHasher = new PasswordHasher<Seller>();
             var result= passwordHasher.VerifyHashedPassword(existSeller, existSeller.Password, password);
            if (result == PasswordVerificationResult.Failed)
            {
                Messages.InvalidInputMeesages("email or password");
                return false;
            }
            _seller=existSeller;
            return true;
        }
        public void AddProduct()
        {
            if (_unitOfWork.Categories.GetAll().Count == 0)
            {
                Messages.NotFountMessage("any categories");
                return;
            }
            foreach (var category in _unitOfWork.Categories.GetAll())
                Console.WriteLine($"Id - {category.Id}, Name - {category.Name}");
       
        IdInput: Messages.InputMessages("id");
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
        ProductNameInput: Messages.InputMessages("product name");
            string name = Console.ReadLine();
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
        ProductCountInput: Messages.InputMessages("product count");
            string countInput = Console.ReadLine();
            int count;
            isSucceeded = int.TryParse(countInput, out count);
            if (!isSucceeded)
            {
                Messages.InvalidInputMeesages("count");
                goto ProductCountInput;
            }
        ProductPriceInput: Messages.InputMessages("product price");
            string priceinput = Console.ReadLine();
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
            _unitOfWork.Commit();
            Messages.SuccessMessages("product", "added");
        }

        public void ChangeProductCount()
        {
            
            foreach (var product in _unitOfWork.Products.GetProductsBySellerId(_seller.Id))
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
            foreach (var product in _unitOfWork.Products.GetProductsBySellerId(_seller.Id))
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
            var existProducts=_unitOfWork.Products.GetProductBySymbol(symbol,_seller.Id);
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
                    $"product count {order.Count}");
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
