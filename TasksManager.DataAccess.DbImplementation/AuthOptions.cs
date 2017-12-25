using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace TasksManager.DataAccess.DbImplementation
{
    public class AuthOptions
    {
        public const string Issuer = "ikit-mita";
        public const string Audience = "ikit-mita";
        const string Key = "ikitmita-micro-service";
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
        }
    }
}
