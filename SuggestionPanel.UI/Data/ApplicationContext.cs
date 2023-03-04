using Microsoft.EntityFrameworkCore;
using SuggestionPanel.Domain.Models;

namespace SuggestionPanel.UI.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Suggestion>(eb =>
            {
                eb.HasOne(x => x.SubmissionOwner);
                eb.HasOne(x => x.SignedTo);
            });

            modelBuilder.Entity<ValueStreamResponsibility>(eb =>
            {
                eb.HasOne(x => x.ValueStream);
            });
        }

        public DbSet<Cost> Costs { get; set; }
        public DbSet<HumanResources> HumanResources { get; set; }
        public DbSet<Suggestion> Suggestions { get; set; }
        public DbSet<ValueStream> ValueStreams { get; set; }
        public DbSet<ValueStreamResponsibility> ValueStreamResponsibilities { get; set; }
    }
}
