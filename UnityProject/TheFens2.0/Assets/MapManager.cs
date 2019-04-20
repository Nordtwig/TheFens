using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour {

    GameObject MapView;
    Transform[] nodes;

    void Start() {
        MapView = GameObject.Find("MapView");

        ConstructMap();
    }

    void Update() {
        
    }

    void ConstructMap() {
        int x = 0;
        int y = 0;

        GameObject go = MapView.transform.GetChild(0).gameObject;
        nodes = new Transform[go.transform.childCount];

        for (int i = 0; i < go.transform.childCount; i++) {
            if (i % 5 == 0) { 
                x++;
                y = 0;
            }
            y++;

            GameObject node = go.transform.GetChild(i).gameObject;
            node.GetComponentInChildren<Text>().text = x + "," + y;

        }
    }
}
