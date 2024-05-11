using System.Collections;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] public float speed = 1f;
    [SerializeField] public float hp;
    [SerializeField] public float maxHp;
    [SerializeField] float scaleX = 1;
    [SerializeField] float scaleY = 1;
    [SerializeField] float scaleZ = 1;
    [SerializeField] bool IsBoss = false;
    [SerializeField] AudioClip Impact1;
    [SerializeField] AudioClip Impact2;
    [SerializeField] AudioClip Impact3;
    [SerializeField] GameObject xpCrystalPrefab;
    [SerializeField] GameObject goldCoin;
    [SerializeField] GameObject weakpointPrefab;
    [SerializeField] float damage;
    protected PlayerController player;
    protected bool isInvincible = false;
    protected AudioClip Impact;
    protected AudioClip dying;
    protected Color baseColor;
    public Rigidbody2D rb;
    public bool isTrackingPlayer = true;
    private float lifeSpan = 30f;
    private float lifetime = 0f;
    private WeakPointEnnemy weakpoint;
    private LineRenderer lineRenderer;

    private static Monster instance;

    public static Monster GetInstance() => instance;


    //--------------------------------------------------------------------------------------------------------------------------------------------

    private void Start()
    {
        instance = this;
        rb = GetComponent<Rigidbody2D>();
        player = PlayerController.GetInstance();
        hp = maxHp + player.Level / 2;
        baseColor = spriteRenderer.color;
        weakpoint = null;
        if (IsBoss)
        {
            this.transform.localScale *= 1.5f;
            weakpoint = Instantiate(weakpointPrefab, transform.position, Quaternion.identity).GetComponent<WeakPointEnnemy>();
            weakpoint.original = this;
            lineRenderer = GetComponent<LineRenderer>();
            UpdateLine();
        }
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------


    //--------------------------------------------------------------------------------------------------------------------------------------------

    protected virtual void FixedUpdate()
    {
        if (!isTrackingPlayer)
        {
            lifetime += Time.deltaTime;
            if (lifetime >= lifeSpan)
            {
                Destroy(gameObject);
            }
        }
        if(IsBoss)
        {
            UpdateLine();
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

    bool getHp(float damage)
    {
        return hp - damage > 0 ? true : false;                       //return true if the hp is higher than 0 otherwise false
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------

    public virtual void getHit(float damage)
    {
        if (getHp(damage) && !isInvincible)
        {
            hp -= damage;
            StartCoroutine(InvincibilityCoroutine());
            if (IsBoss && hp < (maxHp / 2))
            {
                speed = speed + 0.3f;
            }
        }
        if (!getHp(damage))                                         //If Hp <= 0 so gameObject dies
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
            if (!IsBoss)
            {
                Death();
            }
        }
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------

    public void Death()
    {
        AudioManager.instance.PlayDeathSound(transform.position);
        GameManager.ennemyKilled++;

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
            spriteRenderer.color = Color.magenta;
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

    //--------------------------------------------------------------------------------------------------------------------------------------------

    void UpdateLine()
    {
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, weakpoint.transform.position);
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------

}
