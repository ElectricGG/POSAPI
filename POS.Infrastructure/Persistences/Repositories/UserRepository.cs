using Microsoft.EntityFrameworkCore;
using POS.Domain.Entities;
using POS.Infrastructure.Persistences.Context;
using POS.Infrastructure.Persistences.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Infrastructure.Persistences.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly POSContext _context;
        public UserRepository(POSContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User> AccountByUserName(string userName)
        {
            var account = await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.UserName!.Equals(userName));

            return account!;
        }
    }
}
