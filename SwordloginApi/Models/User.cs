using System.Text.Json.Serialization;

namespace SwordloginApi.Models
{
    public class User
    {
        public int Id { get; set; }
        [JsonPropertyName("username")]

        public string Username { get; set; }
        [JsonPropertyName("password")]

        public string Password { get; set; }
        public string Role { get; set; } = "User"; // Varsayılan olarak normal kullanıcı
    }
}
