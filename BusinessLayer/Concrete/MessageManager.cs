using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using DataAccessLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace BusinessLayer.Concrete
{
    public class MessageManager : IMessageService
    {
        private readonly IGenericDal<Message> _MessageDal;

        public MessageManager(IGenericDal<Message> MessageDal)
        {
            _MessageDal = MessageDal;
        }

        public void TAdd(Message Message)
        {
            
            _MessageDal.Insert(Message);
        }

        public Message TGetByID(int id)
        {
            // ID'ye göre mesaj getirme işlemi
            return _MessageDal.GetByID(id);
        }

        public List<Message> TGetList()
        {
            // Mesaj listeleme işlemi
            return _MessageDal.GetList();
        }

        public void TUpdate(Message Message)
        {
            // Burada validasyon ve diğer işlemler yapılabilir.
            _MessageDal.Update(Message);
        }

        public void TDelete(Message Message)
        {
            // Burada validasyon ve diğer işlemler yapılabilir.
            _MessageDal.Delete(Message);
        }
    }
}
