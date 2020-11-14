using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using HostManager.Models;

namespace HostManager.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<Price> Prices { get; set; }
        public DbSet<Term> Terms { get; set; }

/*        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.Entity<Account>().HasIndex(account => account.DomainName);
            builder.Entity<Company>().HasIndex(company => company.Name).IsUnique();
                *//*.IsUnique();
            builder.Entity<Package>().HasIndex(package => package.Name).IsUnique();
            builder.Entity<Term>().HasIndex(term => term.Value).IsUnique();*//*
        }*/
    }
}
