using UnityEngine;

public class GoldCoin : MonoBehaviour
{
    AudioClip goldSound;                    
    public static int gameGoldCoin = 0;
    public float lifespan = 120f;
    private float lifetime = 0f;


    //--------------------------------------------------------------------------------------------------------------------------------------------

    public void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player =collision.GetComponent<PlayerController>();

        if (player)
        {                                   
            gameGoldCoin++;
            player.goldCoin = gameGoldCoin;
            Debug.Log("GoldCoin:"+player.goldCoin);
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
