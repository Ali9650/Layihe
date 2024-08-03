using Core.Entities;
using Data.Contexts;
using Data.Repositories.Abstract;
using Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories.Concrete;

public class ProductRepository : Repository<Product>, IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
    public List<Product> GetProductsBySellerId(int sellerId)
    {
        return _context.Products.Include(x => x.Seller).Where(x => x.SellerId == sellerId).ToList();
    }

    public List<Product> GetProductBySymbol(string namesymbol, int sellerId)
    {
        return _context.Products
            .Include(s => s.Seller)
            .Where(x => x.Name.Contains(namesymbol) && x.SellerId == sellerId).ToList();
    }
}
