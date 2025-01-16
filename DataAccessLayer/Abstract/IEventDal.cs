using EntityLayer.Concrete;
using System.Collections.Generic;

namespace DataAccessLayer.Abstract
{
    public interface IEventDal : IGenericDal<Event>
    {
        // Şehre göre etkinlikleri getirme
        List<Event> GetEventsByCity(int cityId);

        // Kategoriye göre etkinlikleri getirme
        List<Event> GetEventsByCategory(int categoryId);

        // Şehir ve kategoriye göre etkinlikleri getirme
        List<Event> GetEventsByCityAndCategory(int cityId, int categoryId);

        List<Event> TGetListWithArtistAndCategory();
        Event TGetByIDWithArtistAndCategory(int id);
    }
}
