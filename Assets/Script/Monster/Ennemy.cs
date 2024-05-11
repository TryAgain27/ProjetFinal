using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemy : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] public float speed = 1f;
    [SerializeField] public float hp = 1;
    [SerializeField] float scaleX = 1;
    [SerializeField] float scaleY = 1;
    [SerializeField] float scaleZ = 1;
    [SerializeField] bool IsBoss = false;
    [SerializeField] AudioClip Impact1;
    [SerializeField] AudioClip Impact2;
    [SerializeField] AudioClip Impact3;
    [SerializeField] GameObject xpCrystalPrefab;
    [SerializeField] GameObject goldCoin;
    [SerializeField] float damage;
    protected PlayerController player;
    protected bool isInvincible = false;
    protected AudioClip Impact;
    protected AudioClip dying;
    protected Color baseColor;
    private Rigidbody2D rb;
    private float maxHp = 10f;
    public bool isTrackingPlayer = true;
    private float lifeSpan = 30f;
    private float lifetime = 0f;

    //--------------------------------------------------------------------------------------------------------------------------------------------

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        hp = maxHp;
        baseColor = spriteRenderer.color;
        player = PlayerController.GetInstance();
        if (IsBoss)
        {
            this.transform.localScale *= 1.5f;
        }
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------

    protected void Update()
    {
        if (!isTrackingPlayer)
        {
            lifetime += Time.deltaTime;
            if (lifetime >= lifeSpan)
            {
                Destroy(gameObject);
            }
        }

        monsterMoveToPlayer();                              //Give the monster the player as target   
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------

    public void monsterMoveToPlayer()
    {
        //Transform the position of the enemy by the distance between them and the player * their speed
        Vector3 destination = player.transform.position;    //Player position
        Vector3 source = transform.position;                //Where the enemy is

        Vector3 direction = destination - source;           //Direction is the distance between enemey and player
        direction.Normalize();                              //Normalize the speed so its not like a ruber, fast first then slow
        if (!isTrackingPlayer)
        {
            direction = new Vector3(1, 0, 0);
        }

        transform.position += direction * Time.deltaTime * speed;  //Operation to give direction and speed
                                                                   //If distance between player and player is negative flip the sprite
        if (direction.x > 0)                                       //If distance between player and player is negative flip the sprite
        {                                                          //so it face the player
            transform.localScale = new Vector3(-scaleX, scaleY, scaleZ);
        }
        else
            transform.localScale = new Vector3(scaleX, scaleY, scaleZ);
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------

    bool getHp()
    {
        return hp > 0 ? true : false;                       //return true if the hp is higher than 0 otherwise false
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------

    public void getHit(float damage)
    {
        if (getHp() && !isInvincible)
        {
            hp = hp - damage;
            StartCoroutine(InvincibilityCoroutine());
            if (IsBoss && hp < (maxHp / 2))
            {
                speed = speed + 0.5f;
            }
        }
        if (hp <= 0)                                         //If Hp == 0 so gameObject dies
        {
            bool getXP = Randomizer();
            bool getGold = Randomizer();
            if (getXP)
            {
                Instantiate(xpCrystalPrefab, transform.position, Quaternion.identity);
            }
            if (getGold)
            {
                Instantiate(goldCoin, transform.position, Quaternion.identity);
            }
            Death();
        }
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController collisionPlayer = collision.GetComponent<PlayerController>();
        if (collisionPlayer)
        {
            collisionPlayer.getHit(damage);
        }
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------

    public void Death()
    {
        AudioManager.instance.PlayDeathSound(transform.position);
        Destroy(gameObject);
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------

    IEnumerator InvincibilityCoroutine()                    //Give Monster invincibility if touched so didnt dies instantly
    {
        Impact = audioImpactRandomizer();
        AudioSource.PlayClipAtPoint(Impact, transform.position);
        isInvincible = true;
        if (IsBoss)
        {
            spriteRenderer.color = Color.black;
            yield return new WaitForSeconds(0.5f);
            spriteRenderer.color = baseColor;
            isInvincible = false;
        }
        else
            spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        spriteRenderer.color = baseColor;
        isInvincible = false;
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------

    public AudioClip audioImpactRandomizer()
    {
        int randomNumber = UnityEngine.Random.Range(1, 4);
        if (randomNumber == 1)
        {
            return Impact1;
        }
        if (randomNumber == 2)
        {
            return Impact2;
        }
        if (randomNumber == 3)
        {
            return Impact3;
        }
        return null;
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------

    public bool Randomizer()
    {
        int randomNumber = UnityEngine.Random.Range(1, 4);
        if (randomNumber == 1)
        {
            return true;
        }
        else
            return false;
    }
}
