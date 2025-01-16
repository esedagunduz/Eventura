using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IEventService : IGenericService<Event>
    {
        List<Event> GetAllEvents();
        List<Event> TGetListWithArtistAndCategory();
        Event TGetByIDWithArtistAndCategory(int id);
        List<Event> GetEventsByCity(int cityId);

        // Etkinlikleri kategoriye göre getirme
        List<Event> GetEventsByCategory(int categoryId);

        // Etkinlikleri hem şehre hem de kategoriye göre getirme
        List<Event> GetEventsByCityAndCategory(int cityId, int categoryId);

        List<Event> TGetList();


    }
}