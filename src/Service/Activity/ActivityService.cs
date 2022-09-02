using System.ComponentModel;

namespace App.Service;

public class ActivityService {

    private AppDbContext context;

    public ActivityService(AppDbContext context) {
        this.context = context;
    }
    public async Task<SaveActivityPayload> Save(SaveActivityInput input) {
        await Task.Delay(100);
        var payload = new SaveActivityPayload {
            Errors = await ValidateSave(input)
        }; 

        if (payload.Errors.Any()) {
            return payload;
        }

        var activity = context.Activities.FirstOrDefault(t => t.Id == input.Id);
        if (activity is null) {
            activity = new Activity();
            context.Activities.Add(activity);
        }

        activity.Title = input.Title;
        payload.Activity = new(Title: activity.Title, Id: activity.Id, CreatedAt: activity.CreatedAt, RemovedAt: activity.RemovedAt);
        return payload;
    }

    public async Task<List<Error>> ValidateSave(SaveActivityInput input) {
        await Task.Delay(10);
        var errors = new List<Error>();

        var activity = context.Activities.FirstOrDefault(t => t.Id == input.Id);
        if (activity is not null && activity.RemovedAt is not null) {
            errors.Add("Cannot modify removed activity");
            return errors;
        }

        if (string.IsNullOrWhiteSpace(input.Title)) {
            errors.Add("Title required");
        } 

        var dupplicate = context.Activities.Any(t => t.Id != input.Id && t.Title == input.Title);
        if (dupplicate) {
            errors.Add($"Duplicate activity title found");
        }
        return errors;
    }

    public async Task<SaveActivityPayload> RemoveActivity(RemoveActivityInput input) {
        await Task.Delay(100);
        var payload = new SaveActivityPayload();

        var activity = context.Activities.FirstOrDefault(t => t.Id == input.Id);
        if (activity is null) {
            payload.Errors.Add($"Acitivy not found for {input.Id}");
            return payload;
        }

        if (activity.RemovedAt is not null) {
            payload.Errors.Add($"Acitivy already removed on {activity.RemovedAt.ToString()}");
            return payload;
        }

        activity.RemovedAt = DateTime.UtcNow;
        // save..
        payload.Activity = new(Id: activity.Id, Title: activity.Title, CreatedAt: activity.CreatedAt, RemovedAt: activity.RemovedAt);
        return payload;
    }

    public List<Activity> GetActivities() => context.Activities;
}