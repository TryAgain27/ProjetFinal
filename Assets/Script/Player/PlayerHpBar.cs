using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpBar : MonoBehaviour
{
    [SerializeField] PlayerController player;
    [SerializeField] Image foreGround;
    [SerializeField] TMP_Text hp_maxHp;



    //--------------------------------------------------------------------------------------------------------------------------------------------

    void Update()
    {
        float currentHP = player.hp;
        string hpString = currentHP <= 0 ? "0" : string.Format("{0:F2}", currentHP);


        transform.position = player.transform.position + new Vector3(0, 0.75f, 0);        
        float hpRatio = (float)player.hp / player.maxHp;                                
        foreGround.transform.localScale = new Vector3(hpRatio, 1, 1);                   
        hp_maxHp.text = $"{hpString} / {player.maxHp},00";
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------
}

