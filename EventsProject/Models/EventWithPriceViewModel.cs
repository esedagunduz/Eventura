namespace EventsProject.Models
{
    public class EventWithPriceViewModel
    {
        public int EventId { get; set; }
        public string EventName { get; set; }
        public string EventImageUrl { get; set; }  // Yeni alan
        public string CategoryName { get; set; }   // Yeni alan
        public decimal Price { get; set; }

        // Eğer Event modeline ilişkili başka özellikler varsa, onları da ekleyebilirsiniz
    }
    

}