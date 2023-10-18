using Medium.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Medium.DA.Context
{
    public class ApplicationDbContext : DbContext
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
                .HasForeignKey(s => s.PublisherId);

                //many to many => many publisher (follower) to many publisher (following)
                publisher.HasMany(p => p.Followings).WithMany(following => following.Followers).UsingEntity(config =>
                {
                    config.ToTable("Follow");

                });

                //one to many ==> 1 publisher has many savelist
                publisher.HasMany(p => p.SavingLists).WithOne(sl => sl.Publisher).HasForeignKey(s => s.PublisherId);
            });


            modelBuilder.Entity<Story>(story =>
            {
                //one to many ==> 1 story has many topics
                story.HasMany(s => s.Topics).WithMany(t => t.Stories);
                //one to many ==> 1 story has many photos
                story.HasMany(s => s.StoryPhotos).WithOne(sp => sp.Story).HasForeignKey(s => s.StoryId);
                //one to many ==> 1 story has many photos
                story.HasMany(s => s.StoryVideos).WithOne(sp => sp.Story).HasForeignKey(s => s.StoryId);
                //many to many ==> many story exist in many savelist
                story.HasMany(s => s.SavingLists).WithMany(sl => sl.Stories);
            });

            //ternary relation
            modelBuilder.Entity<React>(react =>
            {
                react.HasKey(r => new { r.PublisherId, r.StoryId });
                react.HasOne(r => r.Publisher).WithMany(p => p.Reacts).HasForeignKey(r => r.PublisherId);
                react.HasOne(r => r.Reaction).WithMany(p => p.Reacts).HasForeignKey(r => r.ReactionId);
                react.HasOne(r => r.Story).WithMany(p => p.Reacts).HasForeignKey(r => r.StoryId).OnDelete(DeleteBehavior.NoAction);
            });


        }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<SavingList> SavingLists { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Story> Stories { get; set; }
        public DbSet<StoryVideo> StoryVideos { get; set; }
        public DbSet<StoryPhoto> StoryPhotos { get; set; }
        public DbSet<React> Reacts { get; set; }
        public DbSet<Reaction> Reactions { get; set; }
    }
}
