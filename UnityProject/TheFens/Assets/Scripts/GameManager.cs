using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public int tick;
    public int ticksRequired;

    EventManager eventManager;
    MapManager mapManager;

    private void Start() {
        eventManager = GameObject.FindObjectOfType<EventManager>();

        //Invoke();
    }

    private void Update() {
        
    }
}
