using Core.Entities;
using Data.Contexts;
using Data.Repositories.Abstract;
using Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories.Concrete;

public class OrderRepository : Repository<Order>, IOrderRepository
{
    private readonly AppDbContext _context;

    public OrderRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
    public List<Order> GetOrderWithProductAndSellerAndCustomer()
    {
        return _context.Orders
            .Include(x => x.Product)
            .Include(s => s.Seller)
            .Include(c => c.Customer)
                .ToList();
    }
    
    public List<Order> GetOrderBySellerId(int id)
    {
        return _context.Orders
            .Include(x => x.Product)
            .Include(s => s.Seller)
            .Include(c => c.Customer)
            .Where(x=>x.SellerId==id).ToList();
                //.FirstOrDefault(x=>x.SellerId==id);
    }
    public List<Order> GetOrderByCustomerId(int id)
    {
        return _context.Orders
            .Include(x => x.Product)
            .Include(s => s.Seller)
            .Include(c => c.Customer)
            .Where(x => x.CustomerId == id).ToList();
    } 
    public List<Order> GetOrdersByCreateDate(DateTime date)
    {
        return _context.Orders
            .Include(x => x.Product)
            .Include(s => s.Seller)
            .Include(c => c.Customer)
              .Where(x => x.CreateAt.Date == date.Date).ToList();
    }
    public List<Order> GetOrdersBySellerCreateDate (DateTime date, int sellerid)
    {
        return _context.Orders 
            .Include(x => x.Product)
            .Include(s => s.Seller)
            .Include(c => c.Customer)
            .Where(x => x.CreateAt == date && x.SellerId== sellerid).ToList();
    }
    public List<Order> GetOrdersByCustomerCreateDate(DateTime date, int customerId)
    {
        return _context.Orders
            .Include(x => x.Product)
            .Include(s => s.Seller)
            .Include(c => c.Customer)
            .Where(x => x.CreateAt == date && x.CustomerId == customerId).ToList();
    }
    public List<Order> GetOrdersByProductSymbol(string namesymbol, int customerId)
    {
        return _context.Orders
            .Include(s => s.Seller)
            .Include(s => s.Customer)
            .Include(s => s.Product)
            .Where(x => x.Product.Name.Contains(namesymbol) && x.CustomerId == customerId).ToList();
    }

    //public List<Order> GetOrderBySeller(string namesymbol)
    //{
    //    return _context.Orders
    //        .Include(x => x.Product)
    //        .Include(s => s.Seller)
    //        .Include(c => c.Customer)
    //        .Where(x => x.Seller.Name.Contains(namesymbol)).ToList();
    //}
    //public List<Order> GetOrderByCustomer(string namesymbol)
    //{
    //    return _context.Orders
    //        .Include(x => x.Product)
    //        .Include(s => s.Seller)
    //        .Include(c => c.Customer)
    //        .Where(x => x.Customer.Name.Contains(namesymbol)).ToList();
    //}
}
