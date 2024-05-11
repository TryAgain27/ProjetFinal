using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossHpBar : MonoBehaviour
{
    [SerializeField] Monster monster;
    [SerializeField] Image foreGround;


    //--------------------------------------------------------------------------------------------------------------------------------------------

    void Update()
    {
        float currentHP = monster.hp;

        transform.position = monster.transform.position + new Vector3(0, 1.2f, 0);
        float hpRatio = (float)monster.hp / monster.maxHp;
        foreGround.transform.localScale = new Vector3(hpRatio, 1, 1);
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------
}