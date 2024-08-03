using Core.Entities;
using Data.Contexts;
using Data.Repositories.Abstract;
using Data.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Data.Repositories.Concrete
{
    public class AdminRepository : Repository<Admin>, IAdminRepository
    {
        private readonly AppDbContext _context;

        public AdminRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public Admin GetAdminByEmail(string email)
        {
            return _context.Admins.FirstOrDefault(a=>a.Email==email);
        }
    }
}
