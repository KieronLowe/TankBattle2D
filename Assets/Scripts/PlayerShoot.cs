using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShoot : MonoBehaviour
{
    public Transform BulletFire;
    public Transform MineDrop;
    public GameObject BulletPrefab;
    public GameObject MinePrefab;
    public Text MineAmount;
    public int PlayerNumOfMines = 0;
    public float ShootInput_P1 = 0, ShootInput_P2 = 0;
    public float DropMineInput_P1 = 0, DropMineInput_P2 = 0;
    public float FireRate = 2f;
    public float DropRate = 2f;

    private float nextFire = 0;
    private float nextDrop = 0;

    void Start()
    {
        PlayerNumOfMines = 0;
    }

    void FixedUpdate()
    {
        getPlayerInput();
    }

    void Update()
    {
        MineAmount.text = PlayerNumOfMines.ToString();
        checkPlayerInput("TankPlayer1", ShootInput_P1, DropMineInput_P1, ref nextFire, ref nextDrop);
        if (!MainMenu.IsGameOnePlayer)
            checkPlayerInput("TankPlayer2", ShootInput_P2, DropMineInput_P2, ref nextFire, ref nextDrop);
    }

    private void checkPlayerInput(string tankTag, float shootInput, float dropMineInput, ref float nextFire, ref float nextDrop)
    {
        if (gameObject.tag == tankTag && shootInput == 1 && Time.time > nextFire)
        {
            nextFire = Time.time + FireRate;
            SpawnBullet();
        }
        else if (gameObject.tag == tankTag && dropMineInput == 1 && Time.time > nextDrop)
        {
            nextDrop = Time.time + DropRate;
            SpawnMine();
        }
    }

    private void getPlayerInput()
    {
        ShootInput_P1 = Input.GetAxisRaw("Shoot_P1");
        DropMineInput_P1 = Input.GetAxisRaw("DropMine_P1");
        if (!MainMenu.IsGameOnePlayer)
        {
            ShootInput_P2 = Input.GetAxisRaw("Shoot_P2");
            DropMineInput_P2 = Input.GetAxisRaw("DropMine_P2");
        }
    }

    public void SpawnBullet()
    {
        AudioManager.PlaySoundEffect("Shoot");
        Instantiate(BulletPrefab, BulletFire.position, BulletFire.rotation);
    }

    public void SpawnMine()
    {
        if (PlayerNumOfMines > 0)
        {
            AudioManager.PlaySoundEffect("DropMine");
            Instantiate(MinePrefab, MineDrop.position, MineDrop.rotation);
            PlayerNumOfMines--;
        }
    }
}
