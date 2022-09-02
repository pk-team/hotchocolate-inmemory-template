public class Mutation {
    public async Task<SaveActivityPayload> SaveActivity(
        [Service] ActivityService service,    
        SaveActivityInput input
    ) => await service.Save(input);

    public async Task<SaveActivityPayload> RemoveActivity(
        [Service] ActivityService context,
        RemoveActivityInput input
    ) => await context.RemoveActivity(input);

    public async Task<SaveLabelPayload> SaveLabel(
        [Service] LabelService service,
        SaveLabelInput input
    ) => await service.Save(input);
}