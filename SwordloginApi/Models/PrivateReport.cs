﻿public class PrivateReport
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Subject { get; set; }
    public string Message { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}