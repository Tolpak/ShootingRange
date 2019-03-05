
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildingWindowCell : MonoBehaviour, IPointerClickHandler
{
    public int towerId;
    public int cost;
    public Text costBox;

    public void Awake()
    {
        costBox.text = cost.ToString();
        ScoreManager scoremanager = ScoreManager.Instance;
        scoremanager.OnMoneyUpdated += OnMoneyUpdated;
        Debug.Log("Cell cost - " + cost);
        Debug.Log("CEll  tower id - " + towerId);
        updateCostBox();
    }

    private void updateCostBox()
    {
        ScoreManager scoremanager = ScoreManager.Instance;
        costBox.text = cost.ToString();
        if (scoremanager.isEnoughMoney(cost))
        {
            costBox.color = Color.red; ;
        }
        else
        {
            costBox.color = Color.gray; ;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        BuildingManager buildingManager = BuildingManager.Instance;
        bool state = buildingManager.tryToConstruct(towerId);
        Debug.Log("about to contruct tower number " + towerId);
        if (state)
        {
            transform.parent.GetComponent<BuildingWindow>().CloseWindow();
        }
    }

    
    void OnMoneyUpdated()
    {
        updateCostBox();
    }
    void OnDestroy()
    {
        ScoreManager scoremanager = ScoreManager.Instance;
        scoremanager.OnMoneyUpdated -= OnMoneyUpdated;
    }
}
