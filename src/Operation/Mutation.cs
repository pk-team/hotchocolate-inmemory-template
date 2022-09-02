public class Mutation {
    public async Task<SaveActivityPayload> SaveActivity(
        [Service] Database db,    
        SaveActivityInput input
    ) => await db.SaveActivity(input);

    public async Task<SaveActivityPayload> RemoveActivity(
        [Service] Database db,
        RemoveActivityInput input
    ) => await db.RemoveActivity(input);
}