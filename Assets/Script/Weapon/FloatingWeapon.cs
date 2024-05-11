using UnityEngine;

public class FloatingWeapon : MonoBehaviour
{

    public float orbitRayon = 1.5f;
    public float rotationSpeed = 250f;
    public float weaponRotation = 1400f;
    public float damage = 3f;

    private Transform playerTransform;
    private float currentAngle = 0f;

    //--------------------------------------------------------------------------------------------------------------------------------------------

    public void Initialize(Transform playerTransform)
    {
        this.playerTransform = playerTransform;
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------

    void Start()
    {
        if (playerTransform == null)
        {
            Debug.LogError("Player transform reference not set!");
            return;
        }
        currentAngle = Random.Range(0, 360);
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------

    void Update()
    {

        float radian = currentAngle * Mathf.Deg2Rad;
        Vector2 offset = new Vector2(Mathf.Cos(radian), Mathf.Sin(radian)) * orbitRayon;

        transform.position = (Vector2)playerTransform.position + offset;
        transform.Rotate(0, 0, weaponRotation * Time.deltaTime);
        currentAngle += rotationSpeed * Time.deltaTime;

        if (currentAngle >= 360f) currentAngle -= 360f;

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
            }
        }
        if (collision.CompareTag("ElCombator"))
        {
            ElCombator elCombator = collision.GetComponent<ElCombator>();
            if (elCombator)
            {
                elCombator.GetHit(damage);
            }
        }
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------

}
