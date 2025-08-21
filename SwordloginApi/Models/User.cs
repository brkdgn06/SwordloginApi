namespace SwordloginApi.Models
{
    public class User
    {
        public enum UserStatus
        {
            Pending,
            Approved,
            Rejected
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }      
        public DateTime TokenExpiry { get; set; }

    }
}
