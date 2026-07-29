using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identity;

namespace Talabat.Repository.Identity
{
    public class AppIdentityDbContext : IdentityDbContext<AppUser>
    {
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options)
        : base(options) { }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // => optionsBuilder.UseSqlServer("connectionString");

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Address>().ToTable("Addresses");
        }

        //Add-Migration "IdentityInitialCreate" -Context AppIdentityDbContext -Output Identity/Migrations
        //Remove-Migration -Context AppIdentityDbContext
    }
}

