using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinePickup : MonoBehaviour
{
    public float MineDamage = 50f;

    private TankHealth tank;

    public void OnTriggerEnter2D(Collider2D collider)
    {
        tank = collider.GetComponent<TankHealth>();
        if (collider.tag == "TankPlayer2" || collider.tag == "TankPlayer1")
        {
            AudioManager.PlaySoundEffect("MineExplosion");
            Destroy(gameObject);
            tank.TakeDamage(MineDamage);
        }
    }
}
