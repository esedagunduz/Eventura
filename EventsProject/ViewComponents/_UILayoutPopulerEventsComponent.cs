using DataAccessLayer.Concrete;
using EventsProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

public class _UILayoutPopulerEventsComponent : ViewComponent
{
    private readonly Context _context;

    public _UILayoutPopulerEventsComponent(Context context)
    {
        _context = context;
    }

    public IViewComponentResult Invoke()
    {
        // Önce en çok favorilenen 5 etkinliği bul
        var popularEventIds = _context.UserFavorites
            .GroupBy(uf => uf.EventId)
            .Select(group => new
            {
                EventId = group.Key,
                FavoriteCount = group.Count()
            })
            .OrderByDescending(x => x.FavoriteCount)
            .Take(5)
            .Select(x => x.EventId)
            .ToList();

        // Şimdi bu etkinliklerin detaylarını ve fiyatlarını al
        var eventsWithPrices = _context.Events
            .Where(e => popularEventIds.Contains(e.EventId))
            .Select(e => new EventWithPriceViewModel
            {
                EventId = e.EventId,
                EventName = e.Title,
                EventImageUrl = e.ImageUrl,
                CategoryName = e.Category.Name,
                Price = _context.EventsTickets
                    .Where(et => et.EventId == e.EventId)
                    .Select(et => et.Price)
                    .FirstOrDefault()
            })
            .ToList();

        // popularEventIds sırasına göre sonuçları sırala
        var orderedEvents = popularEventIds
            .Select(id => eventsWithPrices.FirstOrDefault(e => e.EventId == id))
            .Where(e => e != null)
            .ToList();

        return View(orderedEvents);
    }

}