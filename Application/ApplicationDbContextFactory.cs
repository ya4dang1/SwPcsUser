using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application
{
    class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        const string CONNECTIONSTRING = "server=localhost;uid=root;pwd=user123;database=pcsuserdb;";
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseMySql(CONNECTIONSTRING);
            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
