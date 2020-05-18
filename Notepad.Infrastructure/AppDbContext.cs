using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Notepad.Core.Entities;

namespace Notepad.Infrastructure
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserProfile>()
                .HasOne(x => x.User);

            builder.Entity<Debt>()
                .HasQueryFilter(c => !c.IsRepaid);

            builder.Entity<Debtor>()
                .HasMany<Debt>(x => x.Depts)
                .WithOne(x => x.Debtor)
                .OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Debtor> Debtors{ get; set; }
        public DbSet<Debt> Debts { get; set; }
    }
}