using System;
using UnityEngine;

public class EnergyBall : MonoBehaviour, IPoolable
{
    [SerializeField] Transform dividingPosition;
    [SerializeField] GameObject miniEnergyBallPrefab;
    public float speed = 4f;
    public float duration = 4f;
    public float deviationAngle = 60f;
    public float spawnDistance = 1.1f;
    public float damage = 4;
    public int hitCount = 0;

    private Vector2 direction;
    private float lifetime = 0f;

    private bool isInstantiating = false;
    private static EnergyBall instance;

    public static EnergyBall GetInstance() => instance;

    //--------------------------------------------------------------------------------------------------------------------------------------------

    private void Awake()
    {
        instance = this;
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------

    public void Reset()
    {
        lifetime = 0f;
        hitCount = 0;
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------

    public void Initialize(Transform playerTransform)
    {

        float randomAngle = UnityEngine.Random.Range(0, 360);
        float radian = randomAngle * Mathf.Deg2Rad;


        Vector2 spawnOffset = new Vector2(Mathf.Cos(radian), Mathf.Sin(radian)) * spawnDistance;


        transform.position = (Vector2)playerTransform.position + spawnOffset;
        direction = spawnOffset.normalized;
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------

    void Update()
    {
        lifetime += Time.deltaTime;

        transform.position += (Vector3)(direction * speed * Time.deltaTime);


        if (lifetime >= duration)
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
                hitCount++;

                monster.getHit(damage);

                if (hitCount == 1)
                {
                    float deviationRadians = deviationAngle * Mathf.Deg2Rad;
                    this.damage = damage / 2;
                    Vector2 deviation = new Vector2(
                    direction.x * Mathf.Cos(deviationRadians) - direction.y * Mathf.Sin(deviationRadians),
                    direction.x * Mathf.Sin(deviationRadians) + direction.y * Mathf.Cos(deviationRadians)
                    );

                    direction = deviation.normalized;
                }
                else if (hitCount >= 2 && duration > lifetime && !isInstantiating)
                {
                    isInstantiating = true;

                    for (int i = 0; i < 2; i++)
                    {
                        GameObject miniEnergyBall = ObjectPool.GetInstance().GetObject(miniEnergyBallPrefab);
                        miniEnergyBall.GetComponent<MiniEnergyBall>().Initialize(transform);
                        miniEnergyBall.SetActive(true);
                    }
                    this.gameObject.SetActive(false);
                }
            }
        }
        if (collision.CompareTag("ElCombator"))
        {
            ElCombator elcombator = collision.GetComponent<ElCombator>();

            if (elcombator)
            {
                hitCount++;

                elcombator.GetHit(damage);

                if (hitCount == 1)
                {
                    float deviationRadians = deviationAngle * Mathf.Deg2Rad;
                    this.damage = damage / 2;
                    Vector2 deviation = new Vector2(
                    direction.x * Mathf.Cos(deviationRadians) - direction.y * Mathf.Sin(deviationRadians),
                    direction.x * Mathf.Sin(deviationRadians) + direction.y * Mathf.Cos(deviationRadians)
                    );

                    direction = deviation.normalized;
                }
                else if (hitCount >= 2 && duration > lifetime && !isInstantiating)
                {
                    isInstantiating = true;

                    for (int i = 0; i < 2; i++)
                    {
                        GameObject miniEnergyBall = ObjectPool.GetInstance().GetObject(miniEnergyBallPrefab);
                        miniEnergyBall.GetComponent<MiniEnergyBall>().Initialize(transform);
                        miniEnergyBall.SetActive(true);
                    }
                    this.gameObject.SetActive(false);
                }
            }
        }
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------

}