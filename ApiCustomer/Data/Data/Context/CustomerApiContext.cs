using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Context
{
    public class CustomerApiContext : DbContext
    {
        #region Props
        public DbSet<CustomerEntity> Customers { get; set; }
        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseNpgsql("Host=localhost; Port=5442; Database=Deal; UserId=postgres; Password=123456");
        }

    }
}
