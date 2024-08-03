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
    
 

    public Product GetExistProductWithNameAndSellerId(string name,int sellerId)
    {
        return _context.Products.Include(x => x.Seller).FirstOrDefault(x => x.Name.ToLower() == name.ToLower() && x.SellerId == sellerId);
    }

    public Seller GetSellerByEmail(string email)
    {
        return _context.Sellers.FirstOrDefault(x=>x.Email==email);
    }
    public Seller GetSellerByInput(string input)
    {
        return _context.Sellers.FirstOrDefault(x=>x.Email==input || x.Fin.ToLower()==input.ToLower() || x.SeriaNumber.ToLower()==input.ToLower()
        || x.PhoneNumber==input);    
    }

}
