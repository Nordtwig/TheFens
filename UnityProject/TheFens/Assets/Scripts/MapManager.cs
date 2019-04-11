using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour {

    public GameObject nodePrefab;
    public GameObject nodePanel;

    public GameObject[] nodes;

    void GenerateNodes() {
        nodes = new GameObject[50];

        foreach (GameObject node in nodes) {
            Instantiate(nodePrefab, nodePanel.transform);
        }
    }
}
