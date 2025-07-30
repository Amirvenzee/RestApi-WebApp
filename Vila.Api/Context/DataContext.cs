using Microsoft.EntityFrameworkCore;
using VILA.Api.Models;


namespace VILA.Api.Context
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options) 
        {
            
        }
        public DbSet<Models.Vila> Vilas { get; set; }
        public DbSet<Detail> Details { get; set; }
        public DbSet<Customer> Customers { get; set; }







    }
}
