using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.UI;

public class BuildingWindow : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    public GameObject cellPrefab;
    public int amountOfTowers;
    public bool isPointerOut;

    public void Awake()
    {
        for ( int i = 0; i < amountOfTowers; i++)
        {
            var cell = Instantiate(cellPrefab, transform);
            string iconName = "Icons/towerIcon" + i;
            GameObject cellImage = cell.transform.Find("Image").gameObject;
            cellImage.GetComponent<Image>().sprite = Resources.Load<Sprite>(iconName);
            cell.GetComponent<BuildingWindowCell>().towerId = i;
            cell.GetComponent<BuildingWindowCell>().cost = BuildingManager.Instance.GetTowerCost(i);
            Debug.Log("GetTowerCost " + BuildingManager.Instance.GetTowerCost(i));
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isPointerOut = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isPointerOut = false;
    }

    void OnGUI()
    {
        Event e = Event.current;
        if (e.type == EventType.MouseDown && isPointerOut)
        {
            CloseWindow();
        }
    }

    public void CloseWindow()
    {
        Destroy(gameObject);
    }
}
