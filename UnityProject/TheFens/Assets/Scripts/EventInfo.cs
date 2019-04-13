
public enum Effects { Dismiss, Remove, Add, None }
public enum Resources { Crew, Supplies }

[System.Serializable]
public class EventInfo {
    public string eventName;
    public string eventDescription;
    public ChoiceEffect[] choices;

    [System.Serializable]
    public class ChoiceEffect {
        public string choiceName;
        public string resultDescription;
        public Effects effect;
        public Resources resource;
        public int magnitude;
    }    
}
