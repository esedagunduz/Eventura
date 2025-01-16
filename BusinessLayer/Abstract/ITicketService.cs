using EntityLayer.Concrete;
using System.Collections.Generic;

namespace BusinessLayer.Abstract
{
    public interface ITicketService : IGenericService<Ticket>
    {
        // Add method to add multiple tickets at once
        void TAddRange(IEnumerable<Ticket> tickets);
        IEnumerable<Ticket> GetTicketsByEventsTicketId(int eventsTicketId);

     
    }
    }

