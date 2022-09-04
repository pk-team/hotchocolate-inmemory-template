public class Query {
    public string Info() => "Activities app";

    public IQueryable<Activity> GetActivities(
       [Service] AppDbContext context
    )  => context.Activities.AsQueryable();

    public IQueryable<Label> GetLabels(
        [Service] AppDbContext context
    ) => context.Labels.AsQueryable();
}