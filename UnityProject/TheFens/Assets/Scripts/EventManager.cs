using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventManager : MonoBehaviour {

    public GameObject eventPanel;
    public GameObject choicePanel;
    public GameObject resultPanel;
    public GameObject resultButton;
    public Button buttonPrefab;
    public Text eventName;
    public Text eventDescription;
    public Text resultName;
    public Text resultDescription;

    Button[] choiceButtons;

    public EventInfo[] events;

    public int eventIndex;


    void Start() {
        TriggerNewEvent();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.E)) {
            TriggerNewEvent();
        }
    }

    void TriggerNewEvent() {
        UpdateEvent(eventIndex);
        eventPanel.SetActive(true);
        resultPanel.SetActive(false);
        eventIndex++;
    }

    void UpdateEvent(int eventIndex) {
        EventInfo currentEvent = events[eventIndex];

        // Update EventPanel
        this.eventName.text = currentEvent.eventName;
        this.eventDescription.text = currentEvent.eventDescription;

        choiceButtons = new Button[currentEvent.choices.Length];

        for (int i = 0; i < choiceButtons.Length; i++) {
            Button choice = choiceButtons[i];
            int effectEnum = (int)currentEvent.choices[i].effect;
            int resourceEnum = (int)currentEvent.choices[i].resource;
            string resultDescription = currentEvent.choices[i].resultDescription;
            int magnitude = currentEvent.choices[i].magnitude;
            choice = Instantiate(buttonPrefab, choicePanel.transform);
            choice.GetComponentInChildren<Text>().text = currentEvent.choices[i].choiceName;
            choice.onClick.AddListener(() => CalculateChoiceEffect(choice, effectEnum, resourceEnum, resultDescription, magnitude));
            choiceButtons[i] = choice;
        }
    }

    void CalculateChoiceEffect(Button choice, int effectEnum, int resourceEnum, string resultDescription, int magnitude) {
        choice.onClick.RemoveListener(() => CalculateChoiceEffect(choice, effectEnum, resourceEnum, resultDescription, magnitude));


        Effects effect = (Effects)effectEnum;
        Resources resource = (Resources)resourceEnum;

        switch (effect) {
            case Effects.Dismiss:
                eventPanel.SetActive(false);
                CleanUpEvent(choice);
                resultPanel.SetActive(false);
                break;
            case Effects.Remove:
                eventPanel.SetActive(false);
                resultName.text = choice.GetComponentInChildren<Text>().text;
                UpdateResultPanel(effect, resource, resultDescription, magnitude);
                resultPanel.SetActive(true);
                break;
            case Effects.Add:
                eventPanel.SetActive(false);
                resultName.text = choice.GetComponentInChildren<Text>().text;
                UpdateResultPanel(effect, resource, resultDescription, magnitude);
                resultPanel.SetActive(true);
                break;
            case Effects.None:
                eventPanel.SetActive(false);
                resultName.text = choice.GetComponentInChildren<Text>().text;
                UpdateResultPanel(effect, resource, resultDescription, magnitude);
                resultPanel.SetActive(true);
                break;
            default:
                Debug.Log("Somehow, this choice lacks an effect.");
                break;
        }

        foreach (Transform choiceButton in choicePanel.transform) {
            Destroy(choiceButton.gameObject);
        }
    }

    void UpdateResultPanel(Effects effect, Resources resource, string resultDescription, int magnitude) {
        this.resultDescription.text = resultDescription;
        if (effect == Effects.Remove) {
            this.resultDescription.text += "\n \n" + " -" + magnitude;
        }
        else if (effect == Effects.Add) {
            this.resultDescription.text += "\n \n" + " +" + magnitude;
        }

        if (effect != Effects.None) {
            if (resource == Resources.Crew)
                this.resultDescription.text += " Crew";
            if (resource == Resources.Supplies)
                this.resultDescription.text += " Supplies";
        }

        Button dismissButton = Instantiate(buttonPrefab, resultButton.transform);
        dismissButton.GetComponentInChildren<Text>().text = "Dismiss";
        dismissButton.onClick.AddListener(() => CleanUpEvent(dismissButton));
    }

    void CleanUpEvent(Button button) {
        button.onClick.RemoveListener(() => CleanUpEvent(button));
        Destroy(button.gameObject);
        
        resultPanel.SetActive(false);
    }
}
