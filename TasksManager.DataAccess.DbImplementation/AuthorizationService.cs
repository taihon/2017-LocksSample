﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TasksManager.Db;
using TasksManager.ViewModel.Auth;

namespace TasksManager.DataAccess.DbImplementation
{
    public class AuthService : IAuthService
    {
        private readonly IdentityContext context;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConfiguration configuration;

        public AuthService(
            IdentityContext context, 
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            IConfiguration configuration)
        {
            this.context = context;
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.configuration = configuration;
        }
        public async Task<bool> AuthorizeAsync(string userId, string activity, string objectName, int id)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
                return false;
            
            return true;
        }
        public async Task<AuthorizeResponse> LoginAsync(string username, string password)
        {
            var authResult = await signInManager.PasswordSignInAsync(username, password, false, false);
            if (authResult.Succeeded)
            {
                var user = await userManager.Users.FirstAsync(u => u.UserName == username);
                return await GenerateToken(username, user);
            }
            return null;
        }

        public async Task<AuthorizeResponse> RegisterAsync(string username, string password)
        {
            var user = new IdentityUser
            {
                UserName = username
            };
            var registrationResult = await userManager.CreateAsync(user, password);
            if (registrationResult.Succeeded)
            {
                await signInManager.SignInAsync(user, false);
                return await GenerateToken(username, user);
            }
            return null;
        }

        private async Task<AuthorizeResponse> GenerateToken(string username, IdentityUser user)
        {
            var identity = new ClaimsIdentity(JwtBearerDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(ClaimTypes.Name, username));
            var claims = await userManager.GetClaimsAsync(user);
            identity.AddClaims(claims);
            var jwt = new JwtSecurityToken(
                issuer:AuthOptions.Issuer,
                audience:AuthOptions.Audience,
                claims: claims,
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(),SecurityAlgorithms.HmacSha256),
                expires: DateTime.UtcNow.Add(TimeSpan.FromSeconds(120))
                );
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            return new AuthorizeResponse { Token = encodedJwt };
        }
    }
}
