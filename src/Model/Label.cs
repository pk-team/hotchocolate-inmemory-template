namespace App.Model;

public class Label : EntityBase {
    public string Name { get; set; } = "";
    public string? HexColor { get; set; } 

    public static int Name_MaxLen = 20;
    public static int Hex_Len = 7;
}

