using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerSpot : MonoBehaviour {
    public GameObject windowPrefab;
    public GameObject buildingWindow = null;
    public bool isOccupied = false;

    private void OnMouseUp()
    {
        if (!buildingWindow)
        {
            buildingWindow = Instantiate(windowPrefab,
                Camera.main.WorldToScreenPoint(this.transform.position),
                new Quaternion(0,0,0,0),
                GameObject.FindGameObjectWithTag("Canvases").transform);

            BuildingManager.Instance.selectedTowerSpot = this;
        }
    }
}
