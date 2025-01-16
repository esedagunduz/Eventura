using EntityLayer.Concrete;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DataAccessLayer.Abstract
{
    public interface ITicketDal : IGenericDal<Ticket>
    {
        IQueryable<Ticket> GetQueryable();
        
       
            IQueryable<Ticket> GetListAll(Expression<Func<Ticket, bool>> filter = null);
            IEnumerable<Ticket> GetTicketsByEventsTicketId(int eventsTicketId);
        
       
    }
}
