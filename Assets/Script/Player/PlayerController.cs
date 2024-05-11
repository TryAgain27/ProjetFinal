using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject scythePrefab;
    [SerializeField] GameObject EnergyBall;
    [SerializeField] GameObject swordPrefab;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] float speed = 350f;    //Vitesse pour le player
    [SerializeField] float scytheTimer = 2f;
    [SerializeField] float energyBallTimer = 4f;
    public GameObject deathUI;
    public int Level = 1;
    internal int expToLevel = 5;
    internal int currentExp = 0;
    public int goldCoin;
    public float hp;
    public float maxHp = 6;

    public delegate void PlayerDeathHandler();
    public event PlayerDeathHandler OnPlayerDeath;
    protected Color baseColor;

    Animator animator;
    bool isRunning = false;
    private bool isInvincible = false;

    private static PlayerController instance;

    public static PlayerController GetInstance() => instance;


    //--------------------------------------------------------------------------------------------------------------------------------------------

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------

    void Start()
    {
        currentExp = 0;
        Level = 1;
        hp = maxHp;
        animator = GetComponent<Animator>();
        baseColor = spriteRenderer.color;
        GameObject sword = Instantiate(swordPrefab);                                //Spawn des armes. TODO merge dans un script weapon spawner
        sword.GetComponent<FloatingWeapon>().Initialize(transform);
        StartCoroutine(energyBallSpawn());
        StartCoroutine(ScytheSpawn());
    }

    ////--------------------------------------------------------------------------------------------------------------------------------------------
    void Update()
    {
        playerMovement();
        animator.SetBool("IsRunning", isRunning);
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------

    void playerMovement()
    {
        float inputX = Input.GetAxisRaw("Horizontal");                               // Get the axis X for further modification of the position
        float inputY = Input.GetAxisRaw("Vertical");                                 // Get the axis y for further modification of the position   

        transform.position += new Vector3(inputX, inputY) * speed * Time.deltaTime;  //Uses the position and modifie it with input of x, y * speed of player
        //Delta time help to consolidate the frame for equalize speed of the player whatever the machine used=
        if (inputX != 0 && Time.timeScale != 0)
        {
            transform.localScale = new Vector3(inputX > 0 ? -1 : 1, 1, 1);          //Flip the player if turn left-right
        }

        isRunning = false;                                                          //Bring back isRunning false so Idle


        if ((inputX != 0 || inputY != 0) && Time.timeScale != 0)                                               //If the player input in the axis x or y is greater than 0, start animation running
        {
            isRunning = true;

        }

    }

    //--------------------------------------------------------------------------------------------------------------------------------------------

    bool getHp()
    {
        return hp > 0 ? true : false;                       //return true if the hp is higher than 0 otherwise false
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------

    public void getHit(float damage)
    {
        if (!isInvincible)
        {
            hp = hp - damage;
            StartCoroutine(InvincibilityCoroutine());
            Debug.Log("Player's Hp: " + hp.ToString());
        }
        if (!getHp())                                         //If Hp == 0 so gameObject dies
        {
            Death();
        }
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------

    public IEnumerator energyBallSpawn()
    {
        do
        {
            for (int i = 0; i < Level; i++)
            {
                GameObject energyBall = ObjectPool.GetInstance().GetObject(EnergyBall);
                energyBall.GetComponent<EnergyBall>().Initialize(transform);
                energyBall.SetActive(true);
            }
            yield return new WaitForSeconds(energyBallTimer);
        } while (true);
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------

    public IEnumerator ScytheSpawn()
    {
        do
        {
            for (int i = 0; i <= Level + 2; i++)
            {
                GameObject scythe = ObjectPool.GetInstance().GetObject(scythePrefab);
                scythe.GetComponent<Scythe>().Initialize(transform);
                scythe.SetActive(true);
            }
            yield return new WaitForSeconds(scytheTimer);
        } while (true);
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------

    public void addExp()
    {
        currentExp++;
        if (currentExp == expToLevel)
        {
            LevelUp();
        }
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------

    public void LevelUp()
    {
        currentExp = 0;
        expToLevel += 5;
        Level++;
        maxHp += 1;
        hp += 1;
        Debug.Log("Le joueur est devenu niveau: " + Level);
        if(Level%10 ==0)
        {
            GameObject sword = Instantiate(swordPrefab);                                
            sword.GetComponent<FloatingWeapon>().Initialize(transform);
        }
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------

    IEnumerator InvincibilityCoroutine()                    //Give Monster invincibility if touched so didnt dies instantly
    {
        isInvincible = true;
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.8f);
        spriteRenderer.color = baseColor;
        isInvincible = false;
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------

    public void Death()
    {
        Time.timeScale = 0;
        OnPlayerDeath?.Invoke();

        if (deathUI != null)
        {
            deathUI.SetActive(true);
        }
        else
        {
            Debug.LogError("Le UI DeathUi nest pas assigne!");
        }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        GameObject musicManagerObject = GameObject.Find("MusicManager");
        if (musicManagerObject != null)
        {
            AudioSource audioSource = musicManagerObject.GetComponent<AudioSource>();
            if (audioSource != null)
            {
                audioSource.Stop(); // Arrête l'AudioSource
            }
            else
            {
                Debug.LogError("Aucun composant AUdio!");
            }
        }
        else
        {
            Debug.LogError("MusicManager n'est pas dans la scene!");
        }

    }

    //--------------------------------------------------------------------------------------------------------------------------------------------
}
