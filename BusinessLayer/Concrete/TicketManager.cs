using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Concrete
{
    public class TicketManager : ITicketService
    {
        private readonly ITicketDal _ticketDal;

        public TicketManager(ITicketDal ticketDal)
        {
            _ticketDal = ticketDal;
        }

        public void TAdd(Ticket t)
        {
            // Implementation for adding a single ticket
            _ticketDal.Add(t);
        }

        public void TDelete(Ticket t)
        {
            // Implementation for deleting a ticket
            _ticketDal.Delete(t);
        }

        public Ticket TGetByID(int id)
        {
            // Implementation for getting a ticket by ID
            return _ticketDal.GetByID(id);
        }

        public List<Ticket> TGetList()
        {
            // Implementation for getting a list of tickets
            return _ticketDal.GetList();
        }

        public void TUpdate(Ticket t)
        {
            // Implementation for updating a ticket
            _ticketDal.Update(t);
        }

        // Implement the TAddRange method to add multiple tickets at once
        public void TAddRange(IEnumerable<Ticket> tickets)
        {
            foreach (var ticket in tickets)
            {
                _ticketDal.Add(ticket);
            }
        }

        public IEnumerable<Ticket> GetTicketsByEventsTicketId(int eventsTicketId)
        {
            // ToList() çağrısını burada yapmayıp, doğrudan IEnumerable döndürüyoruz
            return _ticketDal.GetTicketsByEventsTicketId(eventsTicketId);
        }
    }
    
}
