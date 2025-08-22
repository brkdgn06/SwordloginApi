namespace SwordloginApi.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Role { get; set; } // 0 = normal, 1 = admin
    }
}
