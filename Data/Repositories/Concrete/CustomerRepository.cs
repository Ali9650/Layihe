using Core.Entities;
using Data.Contexts;
using Data.Repositories.Abstract;
using Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories.Concrete;

public class CustomerRepository : Repository<Customer>, ICustomerRepository
{
    private readonly AppDbContext _context;

    public CustomerRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
    public Customer GetCustomerByEmail(string email)
    {
        return _context.Customers.FirstOrDefault(x => x.Email == email);
    }

  
    public Customer GetCustomerByInput(string input)
    {
        return _context.Customers.FirstOrDefault(x => x.Email == input || x.Fin.ToLower() == input.ToLower() || x.SeriaNumber.ToLower() == input.ToLower()
        || x.PhoneNumber == input);
    }

}
