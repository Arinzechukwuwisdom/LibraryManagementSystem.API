using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SureLbraryAPI.Models;

namespace SureLbraryAPI.Context
{
    public class LibraryContext:DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options)
        : base(options)
        {
        }
        public DbSet<Book>Books { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Transaction>()
                .Property(x => x.Status)
                .HasConversion<string>();
           
            modelBuilder.Entity<User>()
                .HasData(new User { Email = "WisdomSure5@gmail.com", Name = "Arinzechukwu", Password = "Trigger1919" , MembershipNumber=1,Id=1,Role="Admin"});
        }
    }
}
