public class ShareRequest {
    public string Target { get; set; }
    public Session Session { get; set; }

    public bool IsValid () {
        return !string.IsNullOrWhiteSpace (Target) && Session != null && Session.IsValid ();
    }
}

public class Session {
    public string Title { get; set; }
    public string Abstract { get; set; }
    public string Conference { get; set; }

    public bool IsValid () {
        return !string.IsNullOrWhiteSpace (Title) && !string.IsNullOrWhiteSpace (Abstract) && !string.IsNullOrWhiteSpace (Conference);
    }
}
