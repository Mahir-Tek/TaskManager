namespace MainGorevUygulama.Models
{
    public class Mission
    {
        public int Id { get; set; } // Primary key
        public int? UserId { get; set; }
        public string Title{ get; set; }
        public string? Description { get; set; }
        public DateTime Date { get; set; }
        public User User { get; set; } // Navigation property to User
        public bool Statu { get; set; }
    }
}