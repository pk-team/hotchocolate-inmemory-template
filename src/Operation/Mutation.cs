using HotChocolate.Subscriptions;

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
        SaveLabelInput input,
        [Service] ITopicEventSender sender
    ) {
        var payload = await service.Save(input);
        if (!payload.Errors.Any()) {
            await sender.SendAsync(nameof(Subscription.LabelSaved), payload.Label);
        }
        return payload;
    }

    public async Task<PingProps> Ping(
        string message,
        [Service] ITopicEventSender sender
    ) {

        var payload = new PingProps(message);
        await sender.SendAsync(nameof(Subscription.PingAdded), payload);
        return payload;
    }
}