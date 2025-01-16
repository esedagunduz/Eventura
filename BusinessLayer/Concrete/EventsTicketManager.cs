using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Concrete;

namespace BusinessLayer.Concrete
{
    public class EventsTicketManager : IEventsTicketsService
    {
        private readonly IEventsTicketDal _eventsTicketDal;

        public EventsTicketManager(IEventsTicketDal eventsTicketDal)
        {
            _eventsTicketDal = eventsTicketDal;
        }

        public List<EventsTickets> GetEventTicketsWithEvents()
        {
            throw new NotImplementedException();
        }

        public void TAdd(EventsTickets t)
        {
            _eventsTicketDal.Insert(t); // Insert işlemi
        }

        public void TDelete(EventsTickets t)
        {
            _eventsTicketDal.Delete(t); // Delete işlemi
        }

        public EventsTickets TGetByID(int id)
        {
            return _eventsTicketDal.GetByID(id); // ID'ye göre bilet getirme
        }

        // IEventsTicketsService arayüzündeki TGetList metodunun implementasyonu
        public List<EventsTickets> TGetList()
        {
            return _eventsTicketDal.GetList();  // DAL'daki GetList metodunu çağırıyoruz
        }

        public void TUpdate(EventsTickets t)
        {
            _eventsTicketDal.Update(t); // Güncelleme işlemi
        }

        public List<Ticket> GetTicketsByEventsTicketId(int eventsTicketId)
        {
            
            return _eventsTicketDal.GetTicketsByEventsTicketId(eventsTicketId);
        }
        // EventId'ye göre EventsTickets döndürür
        public List<EventsTickets> GetEventsTicketsByEventId(int eventId)
        {
            return _eventsTicketDal.GetEventsTicketsByEventId(eventId);
        }

    }
}
