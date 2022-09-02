public class Query {
    public string Info() => "Activities app";

    public IQueryable<Activity> GetActivities(
        [Service] Database db
    )  => db.GetActivities().AsQueryable();
}