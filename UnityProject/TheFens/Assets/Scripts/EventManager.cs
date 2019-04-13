using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventManager : MonoBehaviour {

    public GameObject eventPanel;
    public GameObject choicePanel;
    public GameObject resultPanel;
    public Button buttonPrefab;
    public Text eventName;
    public Text eventDescription;

    Button[] choiceButtons;

    public EventInfo[] events;

    public int eventIndex;


    void Start() {
        UpdateEvent(0);
        eventPanel.SetActive(true);
        resultPanel.SetActive(false);
    }

    void Update() {
        
    }

    void UpdateEvent(int eventIndex) {
        EventInfo currentEvent = events[eventIndex];

        Debug.Log(currentEvent.eventName);

        this.eventName.text = currentEvent.eventName;
        this.eventDescription.text = currentEvent.eventDescription;

        choiceButtons = new Button[currentEvent.choices.Length];

        for (int i = 0; i < choiceButtons.Length; i++) {
            Button choice = choiceButtons[i];
            choice = Instantiate(buttonPrefab, choicePanel.transform);
            choice.GetComponentInChildren<Text>().text = currentEvent.choices[i].choiceName;
            GenerateChoiceEffect(choice, currentEvent.choices[i].effect);
        }
    }

    void GenerateChoiceEffect(Button choice, Effects effect) {
        switch (effect) {
            case Effects.Dismiss:
                resultPanel.SetActive(false);
                break;
            case Effects.Remove:
                break;
            case Effects.Add:
                break;
            case Effects.None:
                eventPanel.SetActive(false);
                break;
            default:
                Debug.Log("Somehow, this choice lacks an effect.");
                break;
        }
    }
}
