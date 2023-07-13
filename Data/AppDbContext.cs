using cineVote.Models.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : IdentityDbContext<Person>
{
    public string ConnectionString { get; set; }
    public DbSet<Admin> Admins { get; set; }
    public DbSet<CategoryNominee> CategoryNominees { get; set; }
    public DbSet<NomineeCompetition> NomineeCompetitions { get; set; }
    public DbSet<CompetitionCategory> CompetitionCategories {get;set;}
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<Result> Results { get; set; }
    public DbSet<Subscription> Subscriptions { get; set; }
    public DbSet<SubscriptionNotifications> SubscriptionNotifications { get; set; }
    public DbSet<Person> People { get; set; }
    public DbSet<Competition> Competitions { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Nominee> Nominees { get; set; }
    public DbSet<Vote> Votes { get; set; }
    public DbSet<VoteSubscription> voteSubscriptions {get;set;}

    public DbSet<Posts> Posts {get;set;}

    public DbSet<Comments> Comments {get;set;}

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public AppDbContext()
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure the primary key for IdentityUserLogin<TKey> entity
        modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
        {
            entity.HasKey(login => new { login.LoginProvider, login.ProviderKey });
        });

        // Add any other model configurations or entity mappings here

        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }
    }




}
