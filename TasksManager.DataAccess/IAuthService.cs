using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TasksManager.ViewModel.Auth;

namespace TasksManager.DataAccess
{
    public interface IAuthService
    {
        Task<bool> AuthorizeAsync(string userId, string activity, string objectName, int id);
        Task<AuthorizeResponse> LoginAsync(string username, string password);
        Task<AuthorizeResponse> RegisterAsync(string username, string password);
        Task<string[]> GetRolesAsync();
        Task<string[]> GetClaimsAsync(string roleId);
    }
}
