using Medium.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Medium.DA.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int> //: DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Publisher>(publisher =>
            {
                //One to Many ==> 1 publisher to many stories
                publisher.HasMany(p => p.Stories)
                .WithOne(s => s.Publisher)
                .HasForeignKey(s => s.PublisherId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

                //many to many => many publisher (follower) to many publisher (following)
                publisher.HasMany(p => p.Followings).WithMany(following => following.Followers).UsingEntity(config =>
                {
                    config.ToTable("Follow");

                });

                //one to many ==> 1 publisher has many savelist
                publisher
                .HasMany(p => p.SavingLists)
                .WithOne(sl => sl.Publisher)
                .HasForeignKey(s => s.PublisherId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

                //one to one ==> publisher has user
                publisher
                .HasOne(p => p.User)
                .WithOne(u => u.Publisher)
                .HasForeignKey<Publisher>(p => p.Id)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            });


            modelBuilder.Entity<Story>(story =>
            {
                //one to many ==> 1 story has many topics
                story.HasMany(s => s.Topics).WithMany(t => t.Stories);

                //one to many ==> 1 story has many photos
                story.HasMany(s => s.StoryPhotos)
                .WithOne(sp => sp.Story)
                .HasForeignKey(s => s.StoryId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

                //one to many ==> 1 story has many photos
                story
                .HasMany(s => s.StoryVideos)
                .WithOne(sp => sp.Story)
                .HasForeignKey(s => s.StoryId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade); ;

                //many to many ==> many story exist in many savelist
                story.HasMany(s => s.SavingLists).WithMany(sl => sl.Stories);
            });

            //ternary relation
            modelBuilder.Entity<React>(react =>
            {
                react.HasKey(r => new { r.PublisherId, r.StoryId });

                react
                .HasOne(r => r.Publisher)
                .WithMany(p => p.Reacts)
                .HasForeignKey(r => r.PublisherId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade); // if publisher deleted its reacts will deleted 

                react.HasOne(r => r.Reaction).WithMany(p => p.Reacts).HasForeignKey(r => r.ReactionId);

                react.HasOne(r => r.Story).WithMany(p => p.Reacts).HasForeignKey(r => r.StoryId).OnDelete(DeleteBehavior.NoAction);

                react.Ignore(r => r.Id);
            });


        }
        public DbSet<Publisher> Publishers => Set<Publisher>();
        public DbSet<SavingList> SavingLists => Set<SavingList>();
        public DbSet<Topic> Topics => Set<Topic>();
        public DbSet<Story> Stories => Set<Story>();
        public DbSet<StoryVideo> StoryVideos => Set<StoryVideo>();
        public DbSet<StoryPhoto> StoryPhotos => Set<StoryPhoto>();
        public DbSet<React> Reacts => Set<React>();
        public DbSet<Reaction> Reactions => Set<Reaction>();
    }
}
