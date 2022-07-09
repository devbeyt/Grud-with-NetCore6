global using Microsoft.EntityFrameworkCore;


namespace SampleExamle.DataAccess
{
    public class DataContext : DbContext
    {
       public DataContext(DbContextOptions<DataContext> options):base(options) { }

        public DbSet<Product> Products { get; set; }
    }
}
