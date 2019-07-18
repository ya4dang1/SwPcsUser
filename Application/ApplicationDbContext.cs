using Application.Models;
using Core.Libraries;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application
{
    public partial class ApplicationDbContext : BaseDbContext
    {

        public DbSet<UserProfile> UserProfiles { get; set; }

        public DbSet<UserCard> UserCards { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
