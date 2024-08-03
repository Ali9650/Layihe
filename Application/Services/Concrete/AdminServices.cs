using Core.Messages;
using Core.Extensions;
using Core.Entities;
using Data.UnitOfWork.Concrete;
using Application.Services.Abstract;
using System.Globalization;
using Microsoft.AspNetCore.Identity;

namespace Application.Services.Concrete;

public class AdminServices : IAdminService
{
    private readonly UnitOfWork _unitOfWork;

    public AdminServices(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public bool Login()
    {
    LoginEMailInput: Messages.InputMessages("email");
        string email = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(email))
        {
            Messages.InvalidInputMeesages("email");
            goto LoginEMailInput;
        }
    LoginPasswordInput: Messages.InputMessages("pasword");

        string password = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(password))
        {
            Messages.InvalidInputMeesages(" password ");
            goto LoginPasswordInput;
        }
        var existAdmin = _unitOfWork.Admins.GetAdminByEmail(email);
        if (existAdmin is null)
        {
            Messages.InvalidInputMeesages("email or password");
            return false;
        }

        PasswordHasher<Admin> passwordHasher = new PasswordHasher<Admin>();
        var result = passwordHasher.VerifyHashedPassword(existAdmin, existAdmin.Password, password);
        if (result == PasswordVerificationResult.Failed)
        {
            Messages.InvalidInputMeesages("email or password");
            return false;
        }
        return true;
    }

    public void GetAllSellers()
    { 
        if (_unitOfWork.Sellers.GetAll().Count==0)
        {
            Messages.NotFountMessage("any seller");
            return;
        }
        foreach (var seller in _unitOfWork.Sellers.GetAll())
            Console.WriteLine($"Id - {seller.Id}, Name - {seller.Name}");
    }
    public void GetAllCustomers()
    {
        if (_unitOfWork.Customers.GetAll().Count == 0)
        {
            Messages.NotFountMessage("any customer");
            return;
        }
        foreach (var group in _unitOfWork.Customers.GetAll())
            Console.WriteLine($"Id - {group.Id}, Name - {group.Name}");
    }

    public void CreateSeller()
    {
        var seller = new Seller();
    SellerNameInput: Messages.InputMessages("Seller name");
        string name = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(name))
        {
            Messages.InvalidInputMeesages("Seller Name");
            goto SellerNameInput;
        }
    SellerSurnameInput: Messages.InputMessages("Seller surname");
        string surname = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(surname))
        {
            Messages.InvalidInputMeesages("Group Surname");
            goto SellerSurnameInput;
        }
    SellerEmailInput: Messages.InputMessages("Seller email");
        string email = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(email) || !email.IsValidEMail())
        {
            Messages.InvalidInputMeesages("Seller email");
            goto SellerEmailInput;
        }
        if (_unitOfWork.Sellers.GetSellerByInput (email )is not null || _unitOfWork.Customers.GetCustomerByInput(email) is not null)
        {
            Messages.AlreadyExistMessage("email");
            goto SellerEmailInput;
        }

    SellerPasswordInput: Messages.InputMessages("Seller password");
        string password = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(password) || !password.IsValidPassword())
        {
            Messages.InvalidInputMeesages(" Seller password");
            goto SellerPasswordInput;
        }
        PasswordHasher<Seller> passwordHasher = new PasswordHasher<Seller>();
           seller.Password = passwordHasher.HashPassword(seller, password);
    SellerPhoneNumberInput: Messages.InputMessages("Seller phonenumber");
        string phoneNumber = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(password))
        {
            Messages.InvalidInputMeesages(" Seller phonenumber");
            goto SellerPhoneNumberInput;
        }
        if (_unitOfWork.Sellers.GetSellerByInput(phoneNumber) is not null || _unitOfWork.Customers.GetCustomerByInput(phoneNumber) is not null)
        {
            Messages.AlreadyExistMessage("phone");
            goto SellerPhoneNumberInput;
        }
    SellerFinInput: Messages.InputMessages("Seller fin");
        string fin = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(fin))
        {
            Messages.InvalidInputMeesages(" Seller fin");
            goto SellerFinInput;
        }
        if (_unitOfWork.Sellers.GetSellerByInput(fin) is not null || _unitOfWork.Customers.GetCustomerByInput(fin) is not null)
        {
            Messages.AlreadyExistMessage("fin");
            goto SellerFinInput;
        }

    SellerSerialNumberInput: Messages.InputMessages("Seller serial number");
        string serialNumber = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(serialNumber))
        {
            Messages.InvalidInputMeesages(" Seller serial number");
            goto SellerSerialNumberInput;
        }
        if (_unitOfWork.Sellers.GetSellerByInput(serialNumber) is not null || _unitOfWork.Customers.GetCustomerByInput(serialNumber) is not null)
        {
            Messages.AlreadyExistMessage("serial number");
            goto SellerSerialNumberInput;
        }


        seller.Name = name;
        seller.Surname = surname;
        seller.Email = email;
        seller.Fin = fin;
        seller.SeriaNumber = serialNumber;
        seller.PhoneNumber = phoneNumber;
        
        _unitOfWork.Sellers.Add(seller);
        _unitOfWork.Commit();
        Messages.SuccessMessages("Add", "seller");
    }
    public void CreateCustomer()
    {
        var customer = new Customer();
    CustomerNameInput: Messages.InputMessages("Customer name");
        string name = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(name))
        {
            Messages.InvalidInputMeesages("Customer Name");
            goto CustomerNameInput;
        }
    CustomerSurnameInput: Messages.InputMessages("Customer surname");
        string surname = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(surname))
        {
            Messages.InvalidInputMeesages("Customer Surname");
            goto CustomerSurnameInput;
        }
    CustomerEmailInput: Messages.InputMessages("Customer email");
        string email = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(email) || !email.IsValidEMail())
        {
            Messages.InvalidInputMeesages("Customer email");
            goto CustomerEmailInput;
        }
        if (_unitOfWork.Customers.GetCustomerByInput(email) is not null || _unitOfWork.Sellers.GetSellerByInput(email) is not null)
        {
            Messages.AlreadyExistMessage("email");
            goto CustomerEmailInput;
        }

    CustomerPasswordInput: Messages.InputMessages("Customer password");
        string password = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(password) || !password.IsValidPassword())
        {
            Messages.InvalidInputMeesages(" Customer password");
            goto CustomerPasswordInput;
        }
        PasswordHasher<Customer> passwordHasher = new PasswordHasher<Customer>();
        customer.Password = passwordHasher.HashPassword(customer, password);
    CustomerPhoneNumberInput: Messages.InputMessages("Customer phonenumber");
        string phoneNumber = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(password))
        {
            Messages.InvalidInputMeesages(" Customer phonenumber");
            goto CustomerPhoneNumberInput;
        }
        if (_unitOfWork.Customers.GetCustomerByInput(phoneNumber) is not null || _unitOfWork.Sellers.GetSellerByInput(phoneNumber) is not null)
        {
            Messages.AlreadyExistMessage("phone number");
            goto CustomerPhoneNumberInput;
        }
    CustomerFinInput: Messages.InputMessages("Customer fin");
        string fin = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(fin))
        {
            Messages.InvalidInputMeesages(" Customer fin");
            goto CustomerFinInput;
        }
        if (_unitOfWork.Customers.GetCustomerByInput(fin) is not null || _unitOfWork.Sellers.GetSellerByInput(fin) is not null)
        {
            Messages.AlreadyExistMessage("fin");
            goto CustomerFinInput;
        }
    CustomerSerialNumberInput: Messages.InputMessages("Customer serial number");
        string serialNumber = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(serialNumber))
        {
            Messages.InvalidInputMeesages(" Customer serial number");
            goto CustomerSerialNumberInput;
        }
        if (_unitOfWork.Customers.GetCustomerByInput(serialNumber) is not null || _unitOfWork.Sellers.GetSellerByInput(serialNumber) is not null)
        {
            Messages.AlreadyExistMessage("serial number");
            goto CustomerSerialNumberInput;
        }

        customer.Name = name;
        customer.Surname = surname;
        customer.Email = email;
        customer.Fin = fin;
        customer.SeriaNumber = serialNumber;
        customer.PhoneNumber = phoneNumber;
       
        _unitOfWork.Customers.Add(customer);
        _unitOfWork.Commit();
        Messages.SuccessMessages("Add", "customer");
    }
    public void DeleteSeller()
    {
    InputSellerId: Messages.InputMessages("Seller id");
        GetAllSellers();
        string inputId = Console.ReadLine();
        int sellerId;
        bool issucceeded = int.TryParse(inputId, out sellerId);
        if (!issucceeded)
        {
            Messages.InvalidInputMeesages("seller id");
            goto InputSellerId;
        }
        var seller = _unitOfWork.Sellers.Get(sellerId);
        if (seller is null)
        {
            Messages.NotFountMessage("Seller");
            return;
        }
        _unitOfWork.Sellers.Delete(seller);
        _unitOfWork.Commit();
        Messages.SuccessMessages("Seller", "deleted");
    }
    public void DeleteCustomer()
    {
    InputCustomerId: Messages.InputMessages("Customer id");
        GetAllCustomers();
        string inputId = Console.ReadLine();
        int customerId;
        bool issucceeded = int.TryParse(inputId, out customerId);
        if (!issucceeded)
        {
            Messages.InvalidInputMeesages("customer id");
            goto InputCustomerId;
        }
        var customer = _unitOfWork.Customers.Get(customerId);
        if (customer is null)
        {
            Messages.NotFountMessage("customer");
            return;
        }
        _unitOfWork.Customers.Delete(customer);
        _unitOfWork.Commit();
        Messages.SuccessMessages("Customer", "deleted");
    }
    
    public void CreateCategory()
    {
    CategoryNameInput: Messages.InputMessages("Category name");
        string name = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(name))
        {
            Messages.InvalidInputMeesages("Category Name");
            goto CategoryNameInput;
        }
        var existcategory = _unitOfWork.Categories.GetByName(name);
        if (existcategory is not null)
        {
            Messages.AlreadyExistMessage("existcategory");
            return;
        }
        var category = new Category
        {
            Name = name,               
        };
        _unitOfWork.Categories.Add(category);
        _unitOfWork.Commit();
        Messages.SuccessMessages("Add", "category");
    }

    public void OrdersByDesc()
    {
        foreach (var order in _unitOfWork.Orders.GetOrderWithProductAndSellerAndCustomer().OrderByDescending(x=>x.CreateAt))
        {
            Console.WriteLine($"Id : {order.Id} Seller Name : {order.Seller.Name}" +
                " Customer Name:" + order.Customer.Name + "product name" + order.Product.Name + "Product Count:" + order.Count);              
        }
    }
    public void OrdersBySeller()
    {
    IdInput: Messages.InputMessages("id");
        GetAllSellers();
        string idInput = Console.ReadLine();
        int id;
        bool isSucceeded = int.TryParse(idInput, out id);
        if (!isSucceeded)
        {
            Messages.InvalidInputMeesages("id");
            goto IdInput;
        }
        var ordersOfSeller = _unitOfWork.Orders.GetOrderBySellerId(id);
        if (ordersOfSeller == null)
        {
            Messages.NotFountMessage("seller");
            return;
        }

        foreach (var order in ordersOfSeller)
        {
            Console.WriteLine($"OrderId : {order.Id} CustomerId : {order.Customer.Name} " +
                $" ProductName:  {order.Product.Name} {order.TotalAmount}");
        }

    }

    public void OrdersByCustomer()
    {
    IdInput: Messages.InputMessages("id");
        GetAllCustomers();
        string idInput = Console.ReadLine();
        int id;
        bool isSucceeded = int.TryParse(idInput, out id);
        if (!isSucceeded)
        {
            Messages.InvalidInputMeesages("id");
            goto IdInput;
        }
        var ordersOfCustomer = _unitOfWork.Orders.GetOrderByCustomerId(id);
        if (ordersOfCustomer == null)
        {
            Messages.NotFountMessage("customer");
            return;
        }

        foreach (var order in ordersOfCustomer)
        {
            Console.WriteLine($"OrderId : {order.Id} Sellername : {order.Seller.Name} " +
                $" ProductName:  {order.Product.Name} {order.TotalAmount}");
        }

    }

    public void GetOrdersByCreateDate()
    {
        OrderDateInput:Messages.InputMessages("date (dd.MM.yyyy)");
        string dateInput=Console.ReadLine();
        DateTime date;
        bool isSucceded=DateTime.TryParseExact(dateInput,"dd.MM.yyyy",
            CultureInfo.InvariantCulture,DateTimeStyles.None, out date);
        if (!isSucceded)
        {
            Messages.InvalidInputMeesages("date");
            goto OrderDateInput;
        }
         var orders=_unitOfWork.Orders.GetOrdersByCreateDate(date);
        if (orders.Count==0)
        {
            Messages.NotFountMessage("any order on date");
            return;
        }

        foreach (var order in orders)
        {
            Console.WriteLine($"order id : {order.Id} seller name :  {order.Seller.Name}" +
                $" customer name {order.Customer.Name} Product name : {order.Product.Name}" +
                $" total amount: {order.TotalAmount}");
        }
    }
}




