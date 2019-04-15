using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour {

    public GameManager gameManager;

    public GameObject nodePrefab;
    public GameObject nodePanel;

    public Button startPos;
    public Button secondPos;
    public Button[] thirdPos;
    public Button[] OceanPos;

    public GameObject[] nodes;

    public Button currentNode;

    void GenerateNodes() {
        nodes = new GameObject[50];

        foreach (GameObject node in nodes) {
            Instantiate(nodePrefab, nodePanel.transform);
        }
    }

    void Start() {
        currentNode.interactable = false;
        ToggleButtons();
    }

    void Update() {
        
    }

    void ToggleButtons() {
        currentNode.interactable = false;

        if (currentNode == startPos) {
            secondPos.interactable = true;
            foreach (Button button in thirdPos) {
                button.interactable = false;
            }
            foreach (Button button in OceanPos) {
                button.interactable = false;
            }
        }
        if (currentNode == secondPos) {
            foreach (Button button in thirdPos) {
                button.interactable = true;
            }
        }
        if (currentNode == thirdPos[0]) {
            thirdPos[1].interactable = false;
            OceanPos[0].interactable = true;
        }
        if (currentNode == thirdPos[1]) {
            thirdPos[0].interactable = false;
            OceanPos[1].interactable = true;
        }
    }

    public void SetTargetNode(Button button) {
        currentNode = button;
        ToggleButtons();
        gameManager.Move();
        transform.GetChild(0).gameObject.SetActive(false);
    }
}
