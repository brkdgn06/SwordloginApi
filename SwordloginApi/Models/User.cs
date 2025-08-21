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
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsVerified { get; set; }
        public string VerificationToken { get; set; }
        public DateTime TokenExpiry { get; set; }

    }
}
