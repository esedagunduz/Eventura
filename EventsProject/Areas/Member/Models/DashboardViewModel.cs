using EntityLayer.Concrete;
using System.Collections.Generic;
namespace EventsProject.Areas.Member.Models
{
    public class DashboardViewModel
    {
       
            public string Name { get; set; }
            public string Surname { get; set; }
            public string Email { get; set; }
            public string Image { get; set; } // Profil fotoğrafı
            public List<Event> FavoriteEvents { get; set; } // Favori etkinlikler (direkt Event listesi)
        }
}
