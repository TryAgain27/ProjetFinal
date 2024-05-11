using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 8f;          
    public float lifetime = 3f;
    public float damage = .5f;

    private Vector3 moveDirection;

    //--------------------------------------------------------------------------------------------------------------------------------------------

    void Start()
    {
        Destroy(gameObject, lifetime); 
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------

    public void Initialize(Vector3 direction)
    {
        moveDirection = direction.normalized;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------

    void Update()
    {
        transform.position += moveDirection * speed * Time.deltaTime;
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController collisionPlayer = other.GetComponent<PlayerController>();
            collisionPlayer.getHit(damage);
            Destroy(gameObject); 
        }
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------

}