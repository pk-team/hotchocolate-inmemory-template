namespace App.Service;

public class SaveActivityInput {
    public Guid? Id { get; set; } 
    public string Title { get; set; } = "";
}

public class RemoveActivityInput {
    public Guid Id { get; set; }
}

public record SaveActivityProps(Guid Id, string Title, DateTime CreatedAt, DateTime? RemovedAt);
public class SaveActivityPayload : IMutationPayload {
    public SaveActivityProps? Activity { get; set; }
    public ICollection<Error> Errors { get; set; } = new List<Error>();
}
