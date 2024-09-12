namespace PracticeWebApi.Contexts
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    // Configuring Address as a keyless entity type
        //    modelBuilder.Entity<Address>().HasNoKey();
        //}
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<Address> Address { get; set; }
    }
}
