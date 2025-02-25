using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Model.Entity;

namespace DataAccess
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
    {
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Content> Contents { get; set; }
        public DbSet<Reaction> Reactions { get; set; }
        public ApplicationDbContext(DbContextOptions dbContextOptions)
        : base(dbContextOptions)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Reaction>().HasKey(r=>new { r.ApplicationUserId, r.ContentId});
            base.OnModelCreating(builder);
            builder.Entity<Content>();



            builder.Entity<Content>(entity =>
            {
                entity.HasOne(c=>c.ApplicationUser)
                .WithMany(a=>a.Contents)
                .HasForeignKey(c=>c.ApplicationUserId)
                .OnDelete(DeleteBehavior.NoAction)
                ;

                entity.HasOne(c=>c.Parent)
                .WithMany(c=>c.Comments)
                .HasForeignKey(c=>c.ParentId)
                .OnDelete(DeleteBehavior.NoAction)
                ;
            });
        }
    }
}

//migrationBuilder.DropForeignKey(
//name: "FK_Comments_Posts_PostId",
//table: "Comments");
