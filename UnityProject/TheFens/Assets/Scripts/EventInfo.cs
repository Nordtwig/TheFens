
public enum Effects { MoveTo, Remove, Add, None }

[System.Serializable]
public class EventInfo {
    public string eventName;
    public string eventDescription;
    public ChoiceEffect[] choices;

    [System.Serializable]
    public class ChoiceEffect {
        public string choiceName;
        public Effects effect;
    }    
}

[System.Serializable]
public class ResultInfo {
    string resultName;
    public string resultDescription;

}
