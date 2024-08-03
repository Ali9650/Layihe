using Application.Services.Concrete;
using Core.Constants;
using Core.Messages;
using Data;
using Data.UnitOfWork.Concrete;

namespace Presentation
{
    public static class Program
    {
        private static readonly AdminServices _adminServices;
        private static readonly SellerServices _sellerServices;
        private static readonly CustomerServices _customerServices;
        private static readonly UnitOfWork _unitOfWork;
        static Program()
        {
            _unitOfWork=new UnitOfWork();
            _sellerServices = new SellerServices(_unitOfWork);
            _adminServices = new AdminServices(_unitOfWork);
            _customerServices = new CustomerServices(_unitOfWork);
        }
        static void Main(string[] args)
        {
            DbInitializer.SeedData();
            while (true)
            {
            LoginInput: Console.WriteLine(" select admin, seller or customer");
                string select = Console.ReadLine();
                if (select == "admin")
                {
                    if (!_adminServices.Login())
                    {
                        goto LoginInput;
                    }
                    while (true)
                    {
                    AdminChooseInput: Messages.InputMessages("choose");

                        ShowAdminMenu();
                        string choiceInput = Console.ReadLine();
                        int choice;
                        bool issucceeded = int.TryParse(choiceInput, out choice);
                        if (issucceeded)
                        {
                            switch ((AdminOperations)choice)
                            {
                                case AdminOperations.Exit:
                                    return;
                                case AdminOperations.GetAllSellers:
                                    _adminServices.GetAllSellers();
                                    break;
                                case AdminOperations.GetAllCustomers:
                                    _adminServices.GetAllCustomers();
                                    break;
                                case AdminOperations.CreateSeller:
                                    _adminServices.CreateSeller();
                                    break;
                                case AdminOperations.CreateCustomer:
                                    _adminServices.CreateCustomer();
                                    break;
                                case AdminOperations.DeleteSeller:
                                    _adminServices.DeleteSeller();
                                    break;
                                case AdminOperations.DeleteCustomer:
                                    _adminServices.DeleteCustomer();
                                    break;
                                case AdminOperations.CreateCategory:
                                    _adminServices.CreateCategory();
                                    break;
                                case AdminOperations.OrdersByDesc:
                                    _adminServices.OrdersByDesc();
                                    break;
                                case AdminOperations.OrdersBySeller:
                                    _adminServices.OrdersBySeller();
                                    break;
                                case AdminOperations.OrdersByCustomer:
                                    _adminServices.OrdersByCustomer();
                                    break;
                                case AdminOperations.GetOrdersByCreateDate:
                                    _adminServices.GetOrdersByCreateDate();
                                    break;
                                default:
                                    Console.WriteLine("Invalid choice");
                                    break;
                            }
                        }
                        else
                        {
                            Messages.InvalidInputMeesages("choice");
                            goto AdminChooseInput;
                        }
                    }
                }

                else if (select == "seller")
                {
                    if (!_sellerServices.Login())
                    {
                        goto LoginInput;
                    }
                    while (true)
                    {
                    SellerChooseInput: Messages.InputMessages("choose");

                        ShowSellerMenu();
                        string choiceInput = Console.ReadLine();
                        int choice;
                        bool issucceeded = int.TryParse(choiceInput, out choice);
                        if (issucceeded)
                        {
                            switch ((SellerOperations)choice)
                            {
                                case SellerOperations.Exit:
                                    return;
                                case SellerOperations.AddProduct:
                                    _sellerServices.AddProduct();
                                    break;
                                case SellerOperations.ChangeProductCount:
                                    _sellerServices.ChangeProductCount();
                                    break;
                                case SellerOperations.DeleteProduct:
                                    _sellerServices.DeleteProduct();
                                    break;
                                case SellerOperations.ShowOrders:
                                    _sellerServices.ShowOrders();
                                    break;
                                case SellerOperations.ShowOrderByDate:
                                    _sellerServices.ShowOrderByDate();
                                    break;
                                case SellerOperations.FilterProduct:
                                    _sellerServices.FilterProduct();
                                    break;
                                case SellerOperations.GetTotalAmounts:
                                    _sellerServices.GetTotalAmounts();
                                    break;
                                default:
                                    Console.WriteLine("Invalid choice");
                                    break;
                            }
                        }
                        else
                        {
                            Messages.InvalidInputMeesages("choice");
                            goto SellerChooseInput;
                        }
                    }
                }

                else if (select == "customer")
                {
                    if (!_customerServices.Login())
                    {
                        goto LoginInput;
                    }
                    while (true)
                    {
                    CustomerChooseInput: Messages.InputMessages("choose");

                        ShowCustomerMenu();
                        string choiceInput = Console.ReadLine();
                        int choice;
                        bool issucceeded = int.TryParse(choiceInput, out choice);
                        if (issucceeded)
                        {
                            switch ((CustomerOperatons)choice)
                            {
                                case CustomerOperatons.Exit:
                                    return;
                                case CustomerOperatons.BuyProduct:
                                    _customerServices.BuyProduct();
                                    break;
                                case CustomerOperatons.ShowBoughProducts:
                                    _customerServices.ShowBoughProducts();
                                    break;
                                case CustomerOperatons.ShowOrderByDate:
                                    _customerServices.ShowOrderByDate();
                                    break;
                                case CustomerOperatons.FilterProduct:
                                    _customerServices.FilterProduct();
                                    break;
                                default:
                                    Console.WriteLine("Invalid choice");
                                    break;
                            }
                        }
                        else
                        {
                            Messages.InvalidInputMeesages("choice");
                            goto CustomerChooseInput;
                        }
                    }
                }
                else
                {
                    Messages.InvalidInputMeesages("select");
                    goto LoginInput;
                }

            }
        }

        private static void ShowAdminMenu()
        {
            Console.WriteLine("------MENU-----");
            Console.WriteLine("1. Get All Sellers");
            Console.WriteLine("2. Get All Customers");
            Console.WriteLine("3. Create Seller");
            Console.WriteLine("4. Create Customer");
            Console.WriteLine("5. Delete Seller");
            Console.WriteLine("6. Delete Customer");
            Console.WriteLine("7. Create Category");
            Console.WriteLine("8. Orders by desc");
            Console.WriteLine("9. Orders by Seller");
            Console.WriteLine("10. Orders by Customer");
            Console.WriteLine("11. Get Orders by Create Date");
            Console.WriteLine("0. Exit");
        }
        private static void ShowSellerMenu()
        {
            Console.WriteLine("------MENU-----");
            Console.WriteLine("1. Add product");
            Console.WriteLine("2. Change product count");
            Console.WriteLine("3. Delete product");
            Console.WriteLine("4. Show Orders");
            Console.WriteLine("5. Show Order by Date");
            Console.WriteLine("6. Filter Product");
            Console.WriteLine("7. Get Total Amounts");
            Console.WriteLine("0. Exit");
        }
        private static void ShowCustomerMenu()
        {
            Console.WriteLine("------MENU-----");
            Console.WriteLine("1. Buy product");
            Console.WriteLine("2. Show bough product");
            Console.WriteLine("3. Show Order by Date");
            Console.WriteLine("4. Filter product");
            Console.WriteLine("0. Exit");
        }
    }
}
