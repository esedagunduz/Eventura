namespace EventsProject.Models
{
    public class PaymentViewModel
    {
        public string CardHolderName { get; set; }
        public string CardNumber { get; set; }
        public string ExpiryDate { get; set; }
        public string CVV { get; set; }
        public decimal Price { get; set; }
        public bool SaveCard { get; set; }

        // EventTicketId ve EventId ekliyoruz
        public int EventTicketId { get; set; } // Bu alanı ekledik
        public int EventId { get; set; } // Bu alanı ekledik
    }


}