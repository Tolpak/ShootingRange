using UnityEngine;
using System.Collections.Generic;

public class BuildingManager : MonoBehaviour {

    public static BuildingManager Instance { get; private set; }
    
    public TowerSpot selectedTowerSpot;
    public List<GameObject> buildingPrefabs;
    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        var loadedObjects = Resources.LoadAll("Prefabs/Towers");
        buildingPrefabs = new List<GameObject>();
        foreach (var loadedObject in loadedObjects)
        {
            buildingPrefabs.Add(loadedObject as GameObject);
        }
    }

    private void Contruct(int id)
    {
        ScoreManager scoreManager = ScoreManager.Instance;
        scoreManager.SpendMoney(buildingPrefabs[id].GetComponent<Tower>().cost);
        Instantiate(buildingPrefabs[id], selectedTowerSpot.transform.position, buildingPrefabs[id].transform.localRotation);
        Destroy(selectedTowerSpot.gameObject);
    }

    public bool tryToConstruct(int id) {
        int towerCost = buildingPrefabs[id].GetComponent<Tower>().cost;
        ScoreManager scoreManager = ScoreManager.Instance;
        if (scoreManager.isEnoughMoney(towerCost))
        {
            Contruct(id);
            return true;
        }
        else
            return false;
    }
    public int GetTowerCost(int id)
    {
        int price = buildingPrefabs[id].GetComponent<Tower>().cost;
        return price;
    }
}
