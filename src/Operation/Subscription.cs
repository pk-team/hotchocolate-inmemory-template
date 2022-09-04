public class Subscription {

    [Subscribe]
    public PingProps PingAdded(
        [EventMessage] PingProps props
    ) {
        Console.WriteLine();
        return props;
    }

    [Subscribe]
    public SaveLabelProps LabelSaved(
        [EventMessage] SaveLabelProps label 
    ) {
        Console.WriteLine("label saved " + label.Name);
        return label;
    }
        
}