using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float BulletSpeed = 10f;
    public float BulletDamage = 25f; 

    private Rigidbody2D bulletRigidBody;   
    private TankHealth tank;

    void Start()
    {
        bulletRigidBody = GetComponent<Rigidbody2D>();
        bulletRigidBody.velocity = transform.up * BulletSpeed;
    }

    void Update()
    {
        Destroy(gameObject, 7);
    }   

    public void OnTriggerEnter2D(Collider2D collider)
    {
        tank = collider.GetComponent<TankHealth>();
        if (collider.tag == "TankPlayer1" || collider.tag == "TankPlayer2")
        {
            Destroy(gameObject);
            tank.TakeDamage(BulletDamage);
        }
        if (collider.tag == "Wall")
            return;
    }
}
