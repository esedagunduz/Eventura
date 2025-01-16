using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repository;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore; // EF Core kullanıyoruz
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
   public class EfPaymentDal : GenericRepository<Payment>, IPaymentDal
{
    public async Task<bool> BuyTicketAsync(int eventId, int userId, int eventTicketId)
    {
        try
        {
            using (var context = new Context())
            {
                using (var transaction = await context.Database.BeginTransactionAsync())
                {
                    try
                    {
                        // Get EventsTicket with locking to prevent concurrent updates
                        var eventTicket = await context.EventsTickets
                            .FirstOrDefaultAsync(et => et.EventId == eventId && 
                                                     et.EventsTicketId == eventTicketId);

                        if (eventTicket == null)
                        {
                            throw new Exception("Event ticket bulunamadı.");
                        }

                        // Check if selling this ticket would exceed capacity
                        if (eventTicket.SoldCount >= eventTicket.TicketCapacity)
                        {
                            throw new Exception("Bu etkinlik için biletler tükenmiştir.");
                        }

                        // Find available ticket
                        var ticket = await context.Tickets
                            .Where(t => t.EventId == eventId &&
                                      t.EventsTicketId == eventTicketId &&
                                      t.UserId == null &&
                                      t.IsAvailable)
                            .OrderBy(t => t.TicketId)
                            .FirstOrDefaultAsync();

                        if (ticket == null)
                        {
                            throw new Exception("Uygun bilet bulunamadı.");
                        }

                        // Double-check capacity before proceeding
                        var currentSoldCount = await context.Tickets
                            .CountAsync(t => t.EventsTicketId == eventTicketId && 
                                           !t.IsAvailable);

                        if (currentSoldCount >= eventTicket.TicketCapacity)
                        {
                            throw new Exception("Bu etkinlik için biletler tükenmiştir.");
                        }

                        // Update sold count
                        eventTicket.SoldCount = currentSoldCount + 1;
                        
                        // Ensure we're not exceeding capacity
                        if (eventTicket.SoldCount > eventTicket.TicketCapacity)
                        {
                            throw new Exception("Bilet kapasitesi aşıldı.");
                        }

                        context.EventsTickets.Update(eventTicket);

                        // Update ticket status
                        ticket.UserId = userId;
                        ticket.IsAvailable = false;
                        context.Tickets.Update(ticket);

                        // Save changes and commit transaction
                        await context.SaveChangesAsync();
                        await transaction.CommitAsync();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        throw new Exception($"Bilet satın alma işlemi sırasında bir hata oluştu: {ex.Message}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"BuyTicketAsync genel hata: {ex.Message}");
        }
    }

    public decimal GetEventTicketPrice(int id)
    {
        using (var context = new Context())
        {
            var price = context.EventsTickets
                .Where(et => et.EventId == id)
                .Select(et => et.Price)
                .FirstOrDefault();

            return price;
        }
    }
}
}