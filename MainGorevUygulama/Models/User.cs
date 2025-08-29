namespace MainGorevUygulama.Models
{
    public class User
    {
        public int Id { get; set; }  // Primary Key
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<Mission> Missions { get; set; } = new List<Mission>(); // Navigation property for related missions
    }
}