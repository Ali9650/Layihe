using Application.Services.Concrete;
using Core.Messages;

namespace Presentation
{
    internal static class Program
    {
        private static readonly SellerServices _sellerServices;
        static Program()
        {
            _sellerServices = new SellerServices();
        }
        static void Main(string[] args)
        {
            LoginInput:  Console.WriteLine(" choose a s c");
            string choose=Console.ReadLine();
            if (choose=="s")
            {
                _sellerServices.Login();

                if (!_sellerServices.Login())
                {
                    goto LoginInput;
                }  
            _sellerServices.AddProduct();

            }
        }
    }
}
