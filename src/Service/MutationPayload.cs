namespace App.Service;


public interface IMutationPayload {
    public ICollection<Error> Errors { get; set; } 
}

public class Error {

    public Error(string message) {
        Message = message;
    }
    public string Message { get; set; } = "";
}

public static class ErrorExtension {
    public static void Add(this ICollection<Error> self, string message) {
        self.Add(new Error(message));
    }
}