using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XpOrbs : MonoBehaviour
{
    public static int xpValue = 1;
    public float lifespan = 120f;
    private float lifetime = 0f;

    //--------------------------------------------------------------------------------------------------------------------------------------------

    public void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();

        if (player)
        {
            player.addExp();
            GameManager.exp++;
            Debug.Log("Xp Du Player" + player.currentExp);
            Destroy(gameObject);
        }
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------

    public void Update()
    {
        lifetime += Time.deltaTime;
        if (lifetime >= lifespan)
        {
            Destroy(gameObject);
        }
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------

}
