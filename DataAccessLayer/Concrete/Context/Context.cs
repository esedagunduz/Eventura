

using System;
using EntityLayer.Concrete;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccessLayer.Concrete
{
    public class Context : IdentityDbContext<AppUser, AppRole, int>
    //bu kısımda identity öncesi DbContext den miras alıyordu ama identitiy işlemleri sonrası deiştirerek IdentitityDbContexten miras aldırıdk

    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Server=pg-35d0b49b-eventura.c.aivencloud.com;Port=25558;Database=event;Username=avnadmin;Password=AVNS_UHYggiYYAYpKfGAMhym;Ssl Mode=Require;");
        }

        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<UserFavorite> UserFavorites { get; set; }

        public DbSet<Message> Messages { get; set; }
        public DbSet<City> Cities  { get; set; }

        public DbSet<SavedCard> SavedCards { get; set; }
        public DbSet<EventsTickets> EventsTickets { get; set; }
        public DbSet<EventEventsTickets> EventEventsTicket { get; set; }
        public IConfiguration Configuration { get; set; }


        // İlişkilerin yapılandırılması
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // EventEventsTickets için bileşik birincil anahtar tanımlaması
            modelBuilder.Entity<EventEventsTickets>()
                .HasKey(e => new { e.EventId, e.EventsTicketsEventsTicketId }); // Bileşik Anahtar

            // Event ve Ticket ile ilişkilerin kurulması
            modelBuilder.Entity<EventEventsTickets>()
                .HasOne(e => e.Event)
                .WithMany()  // Event ile ilişki
                .HasForeignKey(e => e.EventId); // EventId ForeignKey ilişkisi

            


            // UserFavorite ile AppUser arasındaki ilişkiyi tanımla
            modelBuilder.Entity<UserFavorite>()
                .HasOne(uf => uf.User)
                .WithMany(u => u.UserFavorites)
                .HasForeignKey(uf => uf.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // UserFavorite ile Event arasındaki ilişkiyi tanımla
            modelBuilder.Entity<UserFavorite>()
                .HasOne(uf => uf.Event)
                .WithMany(e => e.UserFavorites)
                .HasForeignKey(uf => uf.EventId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<EventEventsTickets>()
                .HasKey(e => new { e.EventId, e.EventsTicketsEventsTicketId });

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.EventsTicket)    // Ticket'ın bir EventsTicket'i vardır
                .WithMany(et => et.Tickets)    // EventsTicket birçok Ticket'a sahiptir
                .HasForeignKey(t => t.EventsTicketId); // Foreign key ilişkisi

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Event)          // Ticket bir Event'e bağlıdır
                .WithMany(e => e.Tickets)     // Event birçok Ticket'a sahiptir
                .HasForeignKey(t => t.EventId);  // Foreign key ilişkisi


        }





    }
}
