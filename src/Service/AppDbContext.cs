namespace App.Service;

public class AppDbContext {
    public List<Activity> Activities = new List<Activity>();
    public List<Label> Labels = new List<Label>();

    public async Task SaveChangesAsync() => await Task.Delay(100);
}