public class Query {
    public string Info() => "Activities app";

    public IQueryable<Activity> GetActivities(
       [Service] ActivityService db
    )  => db.GetActivities().AsQueryable();
}