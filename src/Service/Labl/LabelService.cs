using System.ComponentModel;
using System.Text.RegularExpressions;

namespace App.Service;

public class LabelService {
    private AppDbContext context;
    public LabelService(AppDbContext ctx) {
        context = ctx;
    }

    public async Task<SaveLabelPayload> Save(SaveLabelInput input) {
        var label = context.Labels.FirstOrDefault(t => t.Id == input.Id);
        var payload = new SaveLabelPayload {
            Errors = await ValidateSsave(input)
        };
        if (payload.Errors.Any()) {
            return payload;
        }
        if (label is null) {
            label = new Label();
            context.Labels.Add(label);
        }
        label.Name = input.Name;
        label.HexColor = input.HexColor;

        await context.SaveChangesAsync();
        payload.Label = new SaveLabelProps(label.Id, label.Name, label.HexColor,  label.CreatedAt, label.RemovedAt);
        return payload;
    }

    public async Task<List<Error>> ValidateSsave(SaveLabelInput input) {
        await Task.Delay(100);
        var errors = new List<Error>();

        if (input.Name is null) {
            errors.Add("Label name required");            
        } else if (input.Name.Trim().Length > Label.Name_MaxLen) {
            errors.Add($"label name cannot be more than {Label.Name_MaxLen} characters");
        }

        // duplicate
        if (!String.IsNullOrWhiteSpace(input.Name)) {
            var duplicate = context.Labels.Any(t => t.Name == input.Name.Trim());
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