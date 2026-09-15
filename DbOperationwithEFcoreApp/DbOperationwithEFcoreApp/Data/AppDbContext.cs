using Microsoft.EntityFrameworkCore;
namespace DbOperationwithEFcoreApp.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CurrencyType>().HasData(
                new CurrencyType() {  Id = 1, CurrencyName = "IND", Description = "Indian Rupee" },
                new CurrencyType() { Id = 2, CurrencyName = "USD", Description = "United States Dollar" },
                new CurrencyType() { Id = 3, CurrencyName = "EUR", Description = "Euro" },
                new CurrencyType() { Id = 4, CurrencyName = "GBP", Description = "British Pound" }
            );


            modelBuilder.Entity<Language>().HasData(
                new Language() { Id = 1, Title = "Hindi", Description = "Hindi Language" },
                new Language() { Id = 2, Title = "English", Description = "English Language" },
                new Language() { Id = 3, Title = "Spanish", Description = "Spanish Language" },
                new Language() { Id = 4, Title = "French", Description = "French Language" }
            );

            modelBuilder.Entity<Auther>().HasData(
                new Auther() { Id = 1, AutherName = "Hindi", AutherBook = "Hindi Language" },
                new Auther() { Id = 2, AutherName = "English", AutherBook = "English Language" },
                new Auther() { Id = 3, AutherName = "Spanish", AutherBook = "Spanish Language" }
               
            );
        }

        
        
        

        public DbSet<Book> Books { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<BookPrice> BookPrices { get; set; }
        public DbSet<CurrencyType> CurrencyTypes { get; set; }

        public DbSet<Auther> Authers { get; set; }


    }
}
