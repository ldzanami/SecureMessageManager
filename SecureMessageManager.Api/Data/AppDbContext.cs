using Microsoft.EntityFrameworkCore;
using SecureMessageManager.Api.Entities;

namespace SecureMessageManager.Api.Data
{
    /// <summary>
    /// Контекст БД приложения.
    /// </summary>
    /// <param name="options">Параметры контекста.</param>
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        /// <summary>
        /// Сущность Users.
        /// </summary>
        public DbSet<User> Users { get; set; }
        
        /// <summary>
        /// Сущность Messages.
        /// </summary>
        public DbSet<Message> Messages { get; set; }
        
        /// <summary>
        /// Сущность Files.
        /// </summary>
        public DbSet<Entities.File> Files { get; set; }
        
        /// <summary>
        /// Сущность Sessions.
        /// </summary>
        public DbSet<Session> Sessions { get; set; }

        /// <summary>
        /// Сущность Logs.
        /// </summary>
        public DbSet<Log> Logs { get; set; }

        /// <summary>
        /// Особенности содзания схемы БД.
        /// </summary>
        /// <param name="modelBuilder">Объект проектировщика БД.</param>
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
