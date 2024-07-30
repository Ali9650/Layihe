using Core.Entities;
using Core.Messages;
using Data.Contexts;
using Data.UnitOfWork.Concrete;
using Microsoft.AspNetCore.Identity;


namespace Data
{
    public static class DbInitializer
    {
        private static readonly AppDbContext _contex;
        static DbInitializer()
        {
            _contex = new AppDbContext();
        }

        public static void SeedData()
        {
            SeedAdmin();
        }
        private static void SeedAdmin()
        {
            if (!_contex.Admins.Any()) { }
            {
                Admin admin = new Admin
                {
                    Name = "Admin",
                    Surname = "Admin",
                    Email = "admin@app.com"
                };
                PasswordHasher<Admin> passwordHasher = new PasswordHasher<Admin>();
                admin.Password = passwordHasher.HashPassword(admin, "Admin123");

                try
                {
                    _contex.SaveChanges();
                }
                catch (Exception)
                {
                    Messages.ErrorOccuredMessage();

                }
            }
        }

       
    }
    }


