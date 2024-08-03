using Core.Entities;
using Core.Messages;
using Data.Contexts;
using Data.Repositories.Concrete;
using Data.UnitOfWork.Abstract;
using System.Text.RegularExpressions;

namespace Data.UnitOfWork.Concrete;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    public readonly SellerRepository Sellers;
    public readonly CustomerRepository Customers;
    public readonly CategoryRepository Categories;
    public readonly ProductRepository Products;
    public readonly OrderRepository Orders;
    public readonly AdminRepository Admins;
    public UnitOfWork()
    {
        _context = new AppDbContext();
        Sellers = new SellerRepository(_context);
        Customers = new CustomerRepository(_context);
        Categories = new CategoryRepository(_context);
        Orders = new OrderRepository(_context);
        Admins=new AdminRepository(_context);
        Products=new ProductRepository(_context);
    }

    public void Commit()
    {
        try
        {
            _context.SaveChanges();
        }
        catch (Exception)
        {
            Messages.ErrorOccuredMessage();
        }
    }
}
    
