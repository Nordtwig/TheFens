using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public enum GameState { Main, Event, Map }
    public GameState state;

    public bool isMoving;

    public int ticksRequired;
    public int ticksLeft;
    public int tickSpeed;
    public int tickIntervall;
    public float eatIntervall;


    public int crewCount;
    public int supplyCount;

    public Text crewDisplay;
    public Text suppliesDisplay;
    public Text ticksDisplay;

    float timer;
    float eatTimer;
    float hungerModifier;

    EventManager eventManager;
    MapManager mapManager;

    private void Start() {
        state = GameState.Main;
        crewCount = 10;
        supplyCount = 20;
        ticksLeft = ticksRequired;
        isMoving = true;

        UpdateUI();

        eventManager = GameObject.FindObjectOfType<EventManager>();
        mapManager = GameObject.FindObjectOfType<MapManager>();

        if (state == GameState.Main) {
            OnNewEvent();
        }

    }

    private void Update() {

        if (state == GameState.Main) {
            eatTimer++;

            if (eatTimer >= eatIntervall) {
                Eat();
                Debug.Log("Nom");
                eatTimer = 0;
            }

            if (isMoving) {
                timer++;
                if (timer >= tickIntervall) {
                    UpdateTick();
                    timer = 0;
                }
            }
        }

        if (mapManager.currentNode == mapManager.OceanPos[0] || mapManager.currentNode == mapManager.OceanPos[0]) {
            if (ticksLeft <= 0) {
                Debug.Log("You won!");
                Invoke("ReloadGame", 1.5f);
            }
        }
        

        if (Input.GetKeyDown(KeyCode.E)) {
            OnNewEvent();
        }
        if (Input.GetKeyDown(KeyCode.C)) {
            UpdateStats(Effects.Remove, Resources.Crew, 1);
        }
        if (Input.GetKeyDown(KeyCode.F)) {
            UpdateStats(Effects.Remove, Resources.Supplies, 1);
        }
        if (Input.GetKeyDown(KeyCode.R)) {
            ReloadGame();
        }
    }

    void OnNewEvent() {
        state = GameState.Event;
        eventManager.TriggerNewEvent();
    }

    void UpdateTick() {
        if (ticksLeft > 0) {
            ticksLeft--;
            UpdateStats();
            CheckForEvent(UnityEngine.Random.value);
        }
        else {
            ArriveOnNode();
        }
    }

    private void ArriveOnNode() {
        isMoving = false;
        mapManager.transform.GetChild(0).gameObject.SetActive(true);
    }

    public void UpdateStats(Effects effect, Resources resource, int magnitude) {
        if (effect == Effects.Remove) {
            if (resource == Resources.Crew) {
                crewCount -= magnitude;
            }
            else {
                supplyCount -= magnitude;
            }
        }
        if (effect == Effects.Add) {
            if (resource == Resources.Crew)
                crewCount += magnitude;
            else
                supplyCount += magnitude;
        }

        CheckStats();

        UpdateUI();
    }

    public void UpdateStats() {
        CheckStats();
        UpdateUI();
    }

    void CheckStats() {
        if (crewCount >= 10) {
            crewCount = Mathf.Clamp(crewCount, 0, 10);
        }
        else if (crewCount <= 0) {
            GameOver();
        }

        if (supplyCount <= 0) {
            supplyCount = Mathf.Clamp(supplyCount, 0, 10);
            CheckStarvation();
        }

        CalculateHungerModifier();
    }

    void UpdateUI() {
        crewDisplay.text = "Crew: " + crewCount.ToString();
        suppliesDisplay.text = "Supplies: " + supplyCount.ToString();
        ticksDisplay.text = "Arrival In: " + ticksLeft.ToString() + " Hours";
    }

    void CheckStarvation() {
        float random = UnityEngine.Random.value;

        if (random <= 0.5f) {
            UpdateStats(Effects.Remove, Resources.Crew, 1);
        }
    }

    void CheckForEvent(float n) {
        if (n <= 0.25f) {
            OnNewEvent();
        }
    }

    public void Move() {
        ticksRequired = 10;
        ticksLeft = ticksRequired;
        isMoving = true;
        UpdateStats();
    }

    void CalculateHungerModifier() {
        //if (!isMoving)
        //    hungerModifier = 0.5f;
        //else
        //    hungerModifier = 1;

        //hungerModifier = 1 * (crewCount / 10); // TODO: Add new modifier when Drummer speed is a thing
        //eatIntervall *= hungerModifier;
        //Debug.Log(crewCount);
        //Debug.Log(eatIntervall);
    }

    void Eat() {
        supplyCount--;
        UpdateStats();
    }

    void GameOver() {
        Debug.Log("You lost.");
        Invoke("ReloadGame", 1.5f);
    }

    void ReloadGame() {
        SceneManager.LoadScene(0);
    }
}
