using System.Text.RegularExpressions;

namespace App.Service;

public class LabelService {
    private AppDbContext context;
    public LabelService(AppDbContext ctx) {
        context = ctx;
    }
    public async Task<MutateLabelPayload> Create(CreateLabelInput input) {
        var payload = new MutateLabelPayload {
            Errors = await ValidateCreate(input)
        };
        if (payload.Errors.Any()) {
            return payload;
        }
        var label = new Label() {
            Name = input.Name,
            HexColor = input.HexColor
        };
        context.Labels.Add(label);

        await context.SaveChangesAsync();
        payload.Label = new MutateLabelProps(label.Id, label.Name, label.HexColor, label.CreatedAt, label.RemovedAt);
        return payload;
    }

    public async Task<MutateLabelPayload> Update(UpdateLabelInput input) {
        var payload = new MutateLabelPayload {
            Errors = await ValidateUpdate(input)
        };
        if (payload.Errors.Any()) {
            return payload;
        }
        var label = context.Labels.First(t => t.Id == input.Id);
        label.Name = input.Name;
        label.HexColor = input.HexColor;

        await context.SaveChangesAsync();
        payload.Label = new MutateLabelProps(label.Id, label.Name, label.HexColor, label.CreatedAt, label.RemovedAt);
        return payload;
    }

    public async Task<List<Error>> ValidateCreate(ILabelInput input) {
        await Task.Delay(100);
        var errors = new List<Error>();

        errors.AddRange(await ValidateProps(input));

        return errors;
    }

    public async Task<List<Error>> ValidateUpdate(UpdateLabelInput input) {
        await Task.Delay(100);
        var errors = new List<Error>();

        var label = context.Labels.FirstOrDefault(t => t.Id == input.Id);
        if (label == null) {
            errors.Add($"Label not found for Id {input.Id}");
            return errors;
        }

        errors.AddRange(await ValidateProps(input, input.Id));

        return errors;
    }

    public async Task<List<Error>> ValidateProps(ILabelInput input, Guid? Id = null) {
        await Task.Delay(10);
        var errors = new List<Error>();

        if (input.Name is null) {
            errors.Add("Label name required");
        } else if (input.Name.Trim().Length > Label.Name_MaxLen) {
            errors.Add($"label name cannot be more than {Label.Name_MaxLen} characters");
        }

        if (!String.IsNullOrWhiteSpace(input.Name)) {
            var duplicate = context.Labels.Any(t => t.Name == input.Name.Trim() && t.Id != Id);
            if (duplicate) {
                errors.Add($"Duplicate name");
            }
        }

        if (input.HexColor is not null) {
            var match = Regex.Match(input.HexColor, "^#([0-9a-f]{6}|[0-9a-f]{3})$");
            if (!match.Success) {
                errors.Add("Invalid hex code");
            }
        }


        return errors;
    }



}