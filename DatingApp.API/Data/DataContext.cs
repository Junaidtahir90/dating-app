
using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data
{
    public class DataContext:DbContext
    {
         public DataContext(DbContextOptions <DataContext> options) : base (options) {}
         public DbSet <Value> Values { get; set; }
         public DbSet <User> Users { get; set; }
         public DbSet <Photo> Photos { get; set; }
         public DbSet<Like> Likes { get; set; }

         protected override void OnModelCreating(ModelBuilder builder) {

            builder.Entity<Like>()
                    .HasKey(key => new { key.LikerId, key.LikeeId } );
  
            builder.Entity<Like>()
                    .HasOne(lk => lk.Likee)
                    .WithMany(lk => lk.Likers)
                    .HasForeignKey(lk => lk.LikeeId)
                    .OnDelete(DeleteBehavior.Restrict);

                    
            builder.Entity<Like>()
                    .HasOne(lk => lk.Liker)
                    .WithMany(lk => lk.Likees)
                    .HasForeignKey(lk => lk.LikerId)
                    .OnDelete(DeleteBehavior.Restrict);
         
         
         
         }  
         
    }
}