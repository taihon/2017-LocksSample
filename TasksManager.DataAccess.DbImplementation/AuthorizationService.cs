using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TasksManager.Db;

namespace TasksManager.DataAccess.DbImplementation
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IdentityContext context;

        public AuthorizationService(IdentityContext context)
        {
            this.context = context;
        }
        public async Task<bool> Authorize(string userId, string activity, string objectName, int id)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
                return false;
            
            return true;
        }
    }
}
