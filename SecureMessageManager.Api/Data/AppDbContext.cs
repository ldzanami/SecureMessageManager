using Microsoft.EntityFrameworkCore;
using SecureMessageManager.Api.Entities;

namespace SecureMessageManager.Api.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Entities.File> Files { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Log> Logs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasMany(user => user.SentMessages)
                                       .WithOne(mes => mes.Sender)
                                       .HasForeignKey(mes => mes.SenderId)
                                       .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>().HasMany(user => user.ReceivedMessages)
                                       .WithOne(mes => mes.Receiver)
                                       .HasForeignKey(mes => mes.ReceiverId)
                                       .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>().HasMany(user => user.SentFiles)
                                       .WithOne(file => file.Sender)
                                       .HasForeignKey(file => file.SenderId)
                                       .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>().HasMany(user => user.ReceivedFiles)
                                       .WithOne(file => file.Receiver)
                                       .HasForeignKey(file => file.ReceiverId)
                                       .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>().HasMany(user => user.Sessions)
                                       .WithOne(session => session.User)
                                       .HasForeignKey(session => session.UserId)
                                       .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
