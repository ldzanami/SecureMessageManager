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
        /// Сущность Chats.
        /// </summary>
        public DbSet<Chat> Chats { get; set; }

        /// <summary>
        /// Сущность ChatMembers.
        /// </summary>
        public DbSet<ChatMember> ChatMembers { get; set; }

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

            modelBuilder.Entity<User>().HasMany(user => user.SentFiles)
                                       .WithOne(file => file.Sender)
                                       .HasForeignKey(file => file.SenderId)
                                       .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>().HasMany(user => user.Sessions)
                                       .WithOne(session => session.User)
                                       .HasForeignKey(session => session.UserId)
                                       .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>().HasMany(user => user.Chats)
                                       .WithOne(member => member.User)
                                       .HasForeignKey(member => member.UserId)
                                       .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Chat>().HasMany(c => c.Members)
                                       .WithOne(m => m.Chat)
                                       .HasForeignKey(m => m.ChatId)
                                       .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Chat>().HasMany(c => c.Messages)
                                       .WithOne(m => m.Chat)
                                       .HasForeignKey(m => m.ChatId)
                                       .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Chat>().HasMany(c => c.Files)
                                       .WithOne(f => f.Chat)
                                       .HasForeignKey(m => m.ChatId)
                                       .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
