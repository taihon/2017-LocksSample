using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TasksManager.DataAccess
{
    public interface IAuthorizationService
    {
        Task<bool> Authorize(string userId, string activity, string objectName, int id);
    }
}
