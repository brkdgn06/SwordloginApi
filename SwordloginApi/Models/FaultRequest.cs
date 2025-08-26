public class FaultRequest
{
    public int Id { get; set; }
    public string Title { get; set; } // Örn: "Yazıcı çalışmıyor"
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public string Status { get; set; } = "Pending"; // Pending, InProgress, Resolved
    public string RequestedByUserId { get; set; } // Normal kullanıcı
}