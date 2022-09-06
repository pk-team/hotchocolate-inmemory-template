namespace App.Service;


public interface ILabelInput {
    string Name { get; set; }
    string? HexColor { get; set; } 
}

public class CreateLabelInput: ILabelInput {
    public string Name { get; set; } = "";
    public string? HexColor { get; set; } 
}


public class UpdateLabelInput:  ILabelInput  {
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public string? HexColor { get; set; } 
}

public class RemoveLabelInput {
    public Guid Id { get; set; }
}

public class LabelPayload : IMutationPayload {
    public MutateLabelPayload? Label { get; set; }
    public ICollection<Error> Errors { get; set; } = new List<Error>();
}

public record MutateLabelProps(Guid Id, string Name, string? HexColor, DateTime CreatedAt, DateTime? RemovedAt);
public class MutateLabelPayload : IMutationPayload {
    public MutateLabelProps? Label { get; set; }
    public ICollection<Error> Errors { get; set; } = new List<Error>();
}
