using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TasksManager.Entities;

namespace TasksManager.Db
{
    public class IdentityContext : IdentityDbContext
    {
        public IdentityContext(DbContextOptions<IdentityContext> options): base(options)
        {

        }
        public DbSet<ObjectPermission> ObjectPermissions { get; set; }
    }
}
