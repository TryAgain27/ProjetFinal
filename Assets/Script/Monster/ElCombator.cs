using System.Collections;
using UnityEngine;

public class ElCombator : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] AudioClip Impact;
    public float moveSpeed = 5f;
    public float attackRange = 5f;
    public GameObject projectilePrefab;
    public float attackCooldown = 2f;
    public float maxHp = 10f;
    public float hp;
    private Rigidbody2D rb;
    private PlayerController player;
    private Animator animator;
    private float lastAttackTime;
    protected Color baseColor;
    protected bool isInvincible = false;

    //--------------------------------------------------------------------------------------------------------------------------------------------

    void Start()
    {
        player = PlayerController.GetInstance();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        baseColor = spriteRenderer.color;
        lastAttackTime = Time.time - attackCooldown;
        hp = maxHp;
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------

    void Update()
    {
        MoveTowardsPlayer();
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------

    void MoveTowardsPlayer()
    {
        Vector3 directionToPlayer = player.transform.position - transform.position;
        directionToPlayer.Normalize();


        if (Vector3.Distance(transform.position, player.transform.position) > attackRange)
        {
            transform.position += directionToPlayer * moveSpeed * Time.deltaTime;
        }
        else if (Time.time - lastAttackTime >= attackCooldown)
        {
            Attack(directionToPlayer);
            lastAttackTime = Time.time;
        }
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------

    void Attack(Vector3 directionToPlayer)
    {
        //animator.SetTrigger("ElCombatorAttack"); 


        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        projectile.GetComponent<Projectile>().Initialize(directionToPlayer);
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------

    public void GetHit(float damage)
    {
        if(getHp(damage) && !isInvincible)
        {
            hp = hp - damage;
            StartCoroutine(InvincibilityCoroutine());
        }
        if(!getHp(damage))
        {
            Death();
        }
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------

    bool getHp(float damage)
    {
        return hp - damage > 0 ? true : false;
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
        AudioSource.PlayClipAtPoint(Impact, transform.position);
        isInvincible = true;
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        spriteRenderer.color = baseColor;
        isInvincible = false;
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------

}