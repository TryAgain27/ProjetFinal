using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerController player;
    [SerializeField] TMP_Text timerText;
    [SerializeField] TMP_Text killStreak;
    [SerializeField] TMP_Text currentLevel;
    [SerializeField] TMP_Text goldCoins;
    [SerializeField] TMP_Text Exp;
    
    public static int ennemyKilled = 0;
    public static int exp = 0;


    private static GameManager instance;

    public static GameManager GetInstance() => instance;

    //--------------------------------------------------------------------------------------------------------------------------------------------

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); //Appeler seulement si il existe deja un autre GameManager
        }
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------

    private void Update()
    {
        
        killStreak.text = $"Ennemy kill : {GameManager.ennemyKilled}";
        currentLevel.text = $"Current Level : {player.Level}";
        goldCoins.text = $" {GoldCoin.gameGoldCoin}";
        Exp.text = $"Exp : {exp}"; 
        
        //Explicit Cast, Lose Decimals  
        int second = (int)Time.time;
        int minute = (int)second / 60;
        second = second % 60;
        if (second < 10 && minute < 10)
        {
            timerText.text = "0" + minute + ":0" + second;
        }
        else if (minute < 10)
        {
            timerText.text = "0" + minute + ":" + second;
        }
        else if (minute >=10)
        {
            timerText.text = minute + ":" + second;
        }
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------
}
