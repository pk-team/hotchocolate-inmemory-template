public class Mutation {
    public async Task<SaveActivityPayload> SaveActivity(
        [Service] ActivityService service,    
        SaveActivityInput input
    ) => await service.SaveActivity(input);

    public async Task<SaveActivityPayload> RemoveActivity(
        [Service] ActivityService context,
        RemoveActivityInput input
    ) => await context.RemoveActivity(input);
}