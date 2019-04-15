using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public enum GameState { Main, Event, Map }
    public GameState state;

    public int ticksRequired;
    public int ticksLeft;
    public int tickSpeed;
    public int tickIntervall;

    public int crewCount;
    public int supplyCount;

    public Text crewDisplay;
    public Text suppliesDisplay;
    public Text ticksDisplay;

    float timer;

    EventManager eventManager;
    MapManager mapManager;

    private void Start() {
        state = GameState.Main;
        crewCount = 10;
        supplyCount = 20;
        ticksLeft = ticksRequired;

        crewDisplay.text = crewCount.ToString();
        suppliesDisplay.text = supplyCount.ToString();
        ticksDisplay.text = ticksRequired.ToString();

        eventManager = GameObject.FindObjectOfType<EventManager>();

        if (state == GameState.Main) {
            OnNewEvent();
        }

        //StartCoroutine(Tick());
    }

    private void Update() {

        if (state == GameState.Main) {
            timer++;
            if (timer >= tickIntervall) {
                UpdateTick();
                timer = 0;
            }
        }
        

        if (Input.GetKeyDown(KeyCode.E)) {
            OnNewEvent();
        }
    }
    
    //IEnumerator Tick() {
    //    while (state == GameState.Main) {
    //        UpdateTick();
    //        yield return new WaitForSeconds(tickSpeed);
    //    }
    //}

    void OnNewEvent() {
        state = GameState.Event;
        eventManager.TriggerNewEvent();
    }

    void UpdateTick() {
        ticksLeft--;
        ticksDisplay.text = ticksLeft.ToString();
    }
}
