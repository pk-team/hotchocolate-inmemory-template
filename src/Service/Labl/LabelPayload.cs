namespace App.Service;

public class SaveLabelInput {
    public Guid? Id { get; set; } 
    public string Name { get; set; } = "";
    public string? HexColor { get; set; } 
}

public class RemoveLabelInput {
    public Guid Id { get; set; }
}

public class LabelPayload : IMutationPayload {
    public SaveLabelPayload? Label { get; set; }
    public ICollection<Error> Errors { get; set; } = new List<Error>();
}

public record SaveLabelProps(Guid Id, string Name, string? HexColor, DateTime CreatedAt, DateTime? RemovedAt);
public class SaveLabelPayload : IMutationPayload {
    public SaveLabelProps? Label { get; set; }
    public ICollection<Error> Errors { get; set; } = new List<Error>();
}
