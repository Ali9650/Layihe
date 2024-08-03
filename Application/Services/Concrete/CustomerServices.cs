using Application.Services.Abstract;
using Core.Entities;
using Core.Extensions;
using Core.Messages;
using Data.UnitOfWork.Concrete;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Concrete
{
    public class CustomerServices : ICustomerServices
    {
        private readonly UnitOfWork _unitOfWork;
        private Customer _customer;

        public CustomerServices(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _customer = null;
        }
        public bool Login()
        {
        LoginEMailInput: Messages.InputMessages("email");
            string email = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(email)||!email.IsValidEMail())
            {
                Messages.InvalidInputMeesages("email");
                goto LoginEMailInput;
            }
            LoginPasswordInput: Messages.InputMessages("pasword");

            string password = Console.ReadLine();
            if ( string.IsNullOrWhiteSpace(password))
            {
                Messages.InvalidInputMeesages(" password ");
                goto LoginPasswordInput;
            }
            var existCustomer = _unitOfWork.Customers.GetCustomerByEmail(email);
            if (existCustomer is null)
            {
                Messages.InvalidInputMeesages("email or password");
                return false;
            }

            PasswordHasher<Customer> passwordHasher = new PasswordHasher<Customer>();
           var result= passwordHasher.VerifyHashedPassword(existCustomer,existCustomer.Password,password);
            if (result==PasswordVerificationResult.Failed)
            {
                Messages.InvalidInputMeesages("email or password");
                return false;
            }
            _customer = existCustomer;
            return true;
        }
        public void BuyProduct()
        {
            Inputid:  Messages.InputMessages("product id");
            foreach (var product in _unitOfWork.Products.GetAll())
            {
                Console.WriteLine($"product id {product.Id} product name {product.Name} product count {product.Count} product price {product.Price}");
            }
            string idInput=Console.ReadLine();
            int id;
            bool isSucceded=int.TryParse(idInput, out id);
            if (!isSucceded)
            {
                Messages.InvalidInputMeesages("id");
                goto Inputid;
            }
            var existproduct = _unitOfWork.Products.Get(id);
           if (existproduct is null)
            {
                Messages.NotFountMessage("product");
                goto Inputid;
            }
            Inputcount: Messages.InputMessages("product count");
            string countInput = Console.ReadLine();
            int count;
             isSucceded = int.TryParse(countInput, out count);
            if (!isSucceded || count<1)
            {
                Messages.InvalidInputMeesages("count");
                goto Inputcount;
            }
            if (count> existproduct.Count)
            {
                Messages.ErrorOccuredMessage();
                goto Inputcount;
            }
           
            decimal totalAmount=count* existproduct.Price;
            Console.WriteLine($"total Amount :{totalAmount} ");

            AnswerINput:  Console.WriteLine("want to buy product? yes or not");
            string answer=Console.ReadLine();
            if (string.IsNullOrWhiteSpace(answer))
            {
                Messages.InvalidInputMeesages("answer");
                goto AnswerINput;
            }
            if (answer.ToLower() =="no")
            {
                Console.WriteLine("order  cancelled");
                return;
            }
            else if (answer.ToLower()!="yes")
            {
                Messages.InvalidInputMeesages("answer");
                goto AnswerINput;
            }
            existproduct.Count -= count;
            if (existproduct.Count == 0)
            {
                _unitOfWork.Products.Delete(existproduct);
            }
            Order order = new Order
            {
                ProductId = existproduct.Id,
                SellerId = existproduct.SellerId,
                CustomerId = _customer.Id,
                Count=count,
                TotalAmount=count*existproduct.Price
            };
            _unitOfWork.Products.Update(existproduct);
            _unitOfWork.Orders.Add(order);
            _unitOfWork.Commit();   
        }
        public void ShowBoughProducts()
        {
            var existOrders = _unitOfWork.Orders.GetOrderByCustomerId(_customer.Id);
            if (!existOrders.Any())
            {
                Messages.NotFountMessage("order");
                return;
            }
            foreach (var order in existOrders)
            {
                Console.WriteLine($" Product Name : {order.Product.Name}" +
                    $" Seller name {order.Seller.Name} product price {order.Product.Price} " +
                    $"product count {order.Count}");
            }
        }
        public void ShowOrderByDate()
        {
        DateInput: Messages.InputMessages("date (dd.MM.yyyy)");
            string dateInput = Console.ReadLine();
            DateTime date;
            bool isSucceeded = DateTime.TryParseExact(dateInput, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date);
            if (!isSucceeded)
            {
                Messages.InvalidInputMeesages("date");
                goto DateInput;
            }
            var orders = _unitOfWork.Orders.GetOrdersByCustomerCreateDate(date, _customer.Id);
            if (orders is null)
            {
                Messages.NotFountMessage("any orders");
                return;
            }
            foreach (var order in orders)
            {
                Console.WriteLine($" Name : {order.Product.Name} Seller name {order.Seller.Name} Count {order.Count}  price {order.Product.Price} create date {order.CreateAt}");
            }
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
            var existOrders = _unitOfWork.Orders.GetOrdersByProductSymbol(symbol, _customer.Id);
            if (existOrders is null)
            {
                Messages.NotFountMessage("order");
                return;
            }
            foreach (var order in existOrders)
            {
                Console.WriteLine($" Name : {order.Product.Name} Count {order.Count} Price {order.Product.Price} Seller name {order.Seller.Name}");                  
            }

        }

    }
}
