public class Subscription {

    [Subscribe]
    public PingProps PingAdded(
        [EventMessage] PingProps props
    ) {
        Console.WriteLine();
        return props;
    }

    [Subscribe]
    public MutateLabelProps LabelSaved(
        [EventMessage] MutateLabelProps label 
    ) {
        Console.WriteLine("label saved " + label.Name);
        return label;
    }
        
}