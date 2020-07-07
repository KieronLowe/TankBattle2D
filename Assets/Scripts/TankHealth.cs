using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TankHealth : MonoBehaviour
{
    public GameObject Tank;
    public Slider Slider;
    public float Health = 100;

    void Update()
    {
        Slider.value = Health;
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
        if (Health <= 0)
            TankDie();
    }

    public void AddHealth(float pickupHealth)
    {
        if (Health <= 75)
            Health += pickupHealth;
    }

    public void TankDie()
    {
        Debug.Log(Tank.tag);
        if(Tank.tag == "TankPlayer1")
            Score.EnemyPoints++;
        else if (Tank.tag == "TankPlayer2")
            Score.PlayerPoints++;
        Destroy(Tank);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
