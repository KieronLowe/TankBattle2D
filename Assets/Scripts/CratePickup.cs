using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CratePickup : MonoBehaviour
{
    public float PickupHealth = 25;

    private PlayerShoot shootScript_P1, shootScript_P2;
    private GameObject tankPlayer_P1, tankPlayer_P2;
    private TankHealth tank;
    private int randPickupVal;

    void Start()
    {
        tankPlayer_P1 = GameObject.FindGameObjectWithTag("TankPlayer1");
        tankPlayer_P2 = GameObject.FindGameObjectWithTag("TankPlayer2");
        shootScript_P1 = tankPlayer_P1.GetComponent<PlayerShoot>();
        if (!MainMenu.IsGameOnePlayer)
            shootScript_P2 = tankPlayer_P2.GetComponent<PlayerShoot>();
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        tank = collider.GetComponent<TankHealth>();
        AudioManager.PlaySoundEffect("CratePickup");

        if (collider.tag == "TankPlayer1" || collider.tag == "TankPlayer2")
        {
            CrateSpawn.IsCrateSpawned = false;

            if (tank.Health > 75)
                randPickupVal = 1;
            else
                randPickupVal = Random.Range(1, 3);

            Destroy(gameObject);

            switch (randPickupVal)
            {
                case 1:
                    if (collider.tag == "TankPlayer1")
                        shootScript_P1.PlayerNumOfMines++;
                    else if (collider.tag == "TankPlayer2" && MainMenu.IsGameOnePlayer)
                        EnemyAIShoot.EnemyNumOfMines++;
                    else if (collider.tag == "TankPlayer2" && !MainMenu.IsGameOnePlayer)
                        shootScript_P2.PlayerNumOfMines++;
                    break;
                case 2:
                    tank.AddHealth(PickupHealth);
                    break;
                default:
                    break;
            }              
        }
    }   
}
