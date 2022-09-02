using System.Security;

namespace App.Model;

public class Activity : EntityBase {
    public string Title { get; set; } = "";
    public List<Label> Labels { get; set; } = new List<Label>();

}