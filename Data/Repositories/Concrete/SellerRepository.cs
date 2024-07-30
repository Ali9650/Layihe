using Core.Entities;
using Data.Contexts;
using Data.Repositories.Abstract;
using Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories.Concrete;

public class SellerRepository : Repository<Seller>, ISellerRepository
{
    private readonly AppDbContext _context;

    public SellerRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public Seller GetSellerWithEmailAndPassword(string email,string password)
    {
        return _context.Sellers.FirstOrDefault(x=>x.Email==email && x.Password==password);
    }
    
   public List<Product> GetProductsBySellerId (int sellerId)
    {
        return _context.Products.Include(x => x.Seller).Where(x=>x.SellerId==sellerId).ToList();
    }

    public Product GetExistProductWithNameAndSellerId(string name,int sellerId)
    {
        return _context.Products.Include(x => x.Seller).FirstOrDefault(x => x.Name.ToLower() == name.ToLower() && x.SellerId == sellerId);
    }
    public List<Product> GetProductBySymbol(string namesymbol, int sellerId)
    {
        return _context.Products
            .Include(s => s.Seller)
            .Where(x => x.Seller.Name.Contains(namesymbol)&& x.SellerId==sellerId).ToList();
    }
}
