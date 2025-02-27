using CodeFirstBasicPatika.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeFirstBasicPatika.Context;

public class PatikaFirstDbContext(DbContextOptions<PatikaFirstDbContext> options) : DbContext(options)
{
    public DbSet<Game> Games { get; set; }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Post> Posts { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // 3 adet basit Movie örneği ekliyoruz
        modelBuilder.Entity<Movie>().HasData(
            new Movie { Id = 1, Title = "Inception", Genre = "Sci-Fi", ReleaseYear = 2010 },
            new Movie { Id = 2, Title = "The Dark Knight", Genre = "Action", ReleaseYear = 2008 },
            new Movie { Id = 3, Title = "Forrest Gump", Genre = "Drama", ReleaseYear = 1994 }
        );

        // 3 adet basit Game örneği ekliyoruz
        modelBuilder.Entity<Game>().HasData(
            new Game { Id = 1, Name = "The Witcher 3", Platform = "PC", Rating = 9.5m },
            new Game { Id = 2, Name = "Uncharted 4", Platform = "PlayStation", Rating = 9.0m },
            new Game { Id = 3, Name = "Halo Infinite", Platform = "Xbox", Rating = 8.7m }
        );


        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.Id);

            // Zorunlu alanlar
            entity.Property(u => u.UserName)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(255);

            // Bir kullanıcının birden çok Post'u vardır
            // Bir Post yalnızca tek bir kullanıcıya aittir
            entity.HasMany(u => u.Posts)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Mock (Seed) Data
            entity.HasData(
                new User
                {
                    Id = 1,
                    UserName = "User1",
                    Email = "user1@example.com"
                },
                new User
                {
                    Id = 2,
                    UserName = "User2",
                    Email = "user2@example.com"
                },
                new User
                {
                    Id = 3,
                    UserName = "User3",
                    Email = "user3@example.com"
                }
            );
        });


        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(p => p.Id);

            // Title alanı zorunlu
            entity.Property(p => p.Title)
                .IsRequired()
                .HasMaxLength(200); // örnek karakter sınırı

            entity.Property(p => p.Content)
                .HasMaxLength(2000); // örnek karakter sınırı
            
            entity.HasOne(p => p.User)
                .WithMany(u => u.Posts)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);


            // Seed Data for Posts
            entity.HasData(
                new Post
                {
                    Id = 1,
                    Title = "User1'ın ilk gönderisi",
                    Content = "User1 için örnek içerik",
                    UserId = 1
                },
                new Post
                {
                    Id = 2,
                    Title = "User2'nın ilk gönderisi",
                    Content = "User2 için örnek içerik",
                    UserId = 2
                },
                new Post
                {
                    Id = 3,
                    Title = "User3'ün ilk gönderisi",
                    Content = "User3 için örnek içerik",
                    UserId = 3
                }
            );
        });
    }
}

// veri eklerken post ile ID kismini yaazmiyoruz otomatik atayacak