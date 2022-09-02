namespace App.Service;

public class ActivityService {

    private AppDbContext context;

    public ActivityService(AppDbContext context) {
        this.context = context;
    }
    public async Task<SaveActivityPayload> SaveActivity(SaveActivityInput input) {
        await Task.Delay(100);
        var payload = new SaveActivityPayload();

        var activity = context.Activities.FirstOrDefault(t => t.Id == input.Id);
        var duplicateActivity = context.Activities.FirstOrDefault(t => t.Id != input.Id && t.Title == input.Title);
        if (duplicateActivity is not null) {
            payload.Errors.AddError($"Duplication title found - Activity ID: \"{duplicateActivity.Id}\"");
        }

        if (payload.Errors.Any()) {
            return payload;
        }

        if (activity is null) {
            activity = new Activity();
            context.Activities.Add(activity);
        }

        activity.Title = input.Title;
        payload.Activity = new(Title: activity.Title, Id: activity.Id, CreatedAt: activity.CreatedAt, RemovedAt: activity.RemovedAt);
        return payload;
    }

    public async Task<SaveActivityPayload> RemoveActivity(RemoveActivityInput input) {
        await Task.Delay(100);
        var payload = new SaveActivityPayload();

        var activity = context.Activities.FirstOrDefault(t => t.Id == input.Id);
        if (activity is null) {
            payload.Errors.AddError($"Acitivy not found for {input.Id}");
            return payload;
        }

        if (activity.RemovedAt is not null) {
            payload.Errors.AddError($"Acitivy already removed on {activity.RemovedAt.ToString()}");
            return payload;
        }

        activity.RemovedAt = DateTime.UtcNow;
        // save..
        payload.Activity = new(Id: activity.Id, Title: activity.Title, CreatedAt: activity.CreatedAt, RemovedAt: activity.RemovedAt);
        return payload;
    }

    public List<Activity> GetActivities() => context.Activities;
}