using Core.Entities;
using Data.Contexts;
using Data.Repositories.Abstract;
using Data.Repositories.Base;

namespace Data.Repositories.Concrete;

public class CustomerRepository : Repository<Customer>, ICustomerRepository
{
    private readonly AppDbContext _context;

    public CustomerRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
}
