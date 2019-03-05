using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    private int lives = 10;
    private int money = 50;

    public Text moneyText;
    public Text livesText;

    public delegate void OnMoneyUpdatedHandler();

    public event OnMoneyUpdatedHandler OnMoneyUpdated;

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
    }

    void Start()
    {
        moneyText.text = "Money: " + money.ToString();
        livesText.text = "Lives: " + lives.ToString();
    }

    public void LoseLife(int l = 1)
    {
        lives -= l;
        livesText.text = "Lives: " + lives.ToString();
        if (lives <= 0)
        {
            GameOver();
        }
    }

    public void AddMoney(int amount)
    {
        money += amount;
        moneyText.text = "Money: " + money.ToString();
        if (OnMoneyUpdated != null)
        {
            OnMoneyUpdated();
        }
    }

    public void SpendMoney(int amount)
    {
        money -= amount;
        moneyText.text = "Money: " + money.ToString();
        
        if (OnMoneyUpdated != null)
        {
            OnMoneyUpdated();
        }
    }

    public bool isEnoughMoney(int amount)
    {
        if (money - amount < 0)
            return false;
        else
            return true;
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
