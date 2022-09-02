public class Activity {
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = "";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? RemovedAt { get; set; }
}