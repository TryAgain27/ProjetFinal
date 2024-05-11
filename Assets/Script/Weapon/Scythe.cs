using UnityEngine;

public class Scythe : MonoBehaviour, IPoolable
{

    public float speed = 3f; // Vitesse du projectile
    public float damage = 3f;
    public float rotationSpeed = 750f;
    public float spawnDistance = 1.1f;
    public float lifespan = 3f; // Durée de vie du projectile avant destruction
    public float waveFrequency = 5f;
    public float waveAmplitude = 1f;

    private Vector2 startDirection;
    private Vector2 direction;
    private float lifetime = 0f;

    private static Scythe instance;

    public static Scythe GetInstance() => instance;

    //--------------------------------------------------------------------------------------------------------------------------------------------

    private void Awake()
    {
        instance = this;
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------

    public void Reset()
    {
        lifespan = 3f;
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------

    public void Initialize(Transform playerTransform)
    {

        float randomAngle = Random.Range(0, 360);
        float radian = randomAngle * Mathf.Deg2Rad;


        Vector2 spawnOffset = new Vector2(Mathf.Cos(radian), Mathf.Sin(radian)) * spawnDistance;


        transform.position = (Vector2)playerTransform.position + spawnOffset;
        startDirection = spawnOffset.normalized;
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------

    private void Update()
    {

        lifetime += Time.deltaTime;

        Vector2 waveOffset = new Vector2(0, Mathf.Sin(lifetime * waveFrequency) * waveAmplitude);
        direction = startDirection + waveOffset;
        transform.position += (Vector3)(direction.normalized * speed * Time.deltaTime);


        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);


        if (lifetime >= lifespan)
        {
            this.gameObject.SetActive(false);
        }

    }

    //--------------------------------------------------------------------------------------------------------------------------------------------

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ennemy"))
        {
            Monster monster = collision.GetComponent<Monster>();
            if (monster)
            {
                monster.getHit(damage);
                this.gameObject.SetActive(false);
            }
        }
        if (collision.CompareTag("ElCombator"))
        {
            ElCombator elCombator = collision.GetComponent<ElCombator>();
            if (elCombator)
            {
                elCombator.GetHit(damage);
                this.gameObject.SetActive(false);
            }
        }

    }

    //--------------------------------------------------------------------------------------------------------------------------------------------

    public int directionRandomizer()
    {
        int randomNumber = Random.Range(1, 4);
        if (randomNumber == 1)
        {
            return 1;
        }
        if (randomNumber == 2)
        {
            return 2;
        }
        if (randomNumber == 3)
        {
            return 3;
        }
        else
            return 4;
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------
}
