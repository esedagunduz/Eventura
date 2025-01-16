using Microsoft.AspNetCore.Mvc;
using DataAccessLayer;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using EntityLayer.Concrete;
using DataAccessLayer.Concrete;
using Microsoft.EntityFrameworkCore;

namespace EventsProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        private readonly Context _context;
        

        public DashboardController(Context context)
        {
            _context = context;
        }
        
        public IActionResult Index()
        {
            // İstatistikleri ViewBag ile gönder
            ViewBag.ArtistCount = _context.Artists.Count();
            ViewBag.EventCount = _context.Events.Count();
            ViewBag.CategoryCount = _context.Categories.Count();
            ViewBag.MessageCount = _context.Messages.Count();

            // Toplam satış miktarı
            var totalSales = _context.EventsTickets
                                      .Select(t => t.Price * t.SoldCount) // Price ve SoldCount çarpımı
                                      .Sum(); // Tüm satılan biletlerin toplam tutarı
            ViewBag.TotalSales = totalSales;

            // Kullanıcı sayısını al
            ViewBag.UserCount = _context.Users.Count(); // Kullanıcı sayısını alıyoruz

            // Satış verisi
            var salesData = new[] {
                new { Label = "Total Sales", Value = totalSales }
            };
            ViewBag.SalesData = salesData;

            // En son kayıt olan kullanıcıyı almak için ID'yi kullanıyoruz
            var latestUsers = _context.Users
                                      .OrderByDescending(u => u.Id)  // ID'ye göre azalan sıralama
                                      .Take(5)  // Son 5 kullanıcı
                                      .ToList();

            ViewBag.LatestUsers = latestUsers;

            var payments = _context.Payments
        .Include(p => p.Ticket)          // Payment ile ilişkilendirilmiş Ticket verisini dahil et
        .ThenInclude(t => t.EventsTicket) // Ticket ile ilişkilendirilmiş EventTicket verisini dahil et
        .Select(p => new
        {
            PaymentId = p.PaymentId,                         // Payment ID
            UserName = p.Ticket.User.Name,            // Kullanıcı adı (Ticket tablosunda yer alıyorsa)
            UserSurname = p.Ticket.User.Surname,     // Kullanıcı soyadı
            Price = p.Ticket.EventsTicket.Price,      // EventTicket'tan Price
            PaymentDate = p.PaymentDate              // Payment tarihi
        })
        .ToList();

            // Veriyi ViewBag ile view'a gönderiyoruz
            ViewBag.Payments = payments;

            return View();
        }
    }
}