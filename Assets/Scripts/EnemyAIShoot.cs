using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAIShoot : MonoBehaviour
{
    public Transform BulletFire;
    public Transform MineDrop;
    public GameObject Target;
    public GameObject BulletPrefab;
    public GameObject MinePrefab;
    public Text MineAmount;
    public static int EnemyNumOfMines = 0;

    private Rigidbody2D enemyRigidBody;

    void Start()
    {
        enemyRigidBody = GetComponent<Rigidbody2D>();
        StartCoroutine(EnemyShootCalculation(1.5f));
        StartCoroutine(enemyDropMine(5.0f));
        EnemyNumOfMines = 0;
    }

    void Update()
    {
        MineAmount.text = EnemyNumOfMines.ToString();
    }

    public IEnumerator EnemyShootCalculation(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            if (Vector2.Distance(transform.position, Target.transform.position) < EnemyAIFieldOfView.ViewDistance)
            {
                Vector2 dirToPlayer = (Target.transform.position - transform.position).normalized;
                if (Vector2.Angle(enemyRigidBody.transform.rotation.eulerAngles, dirToPlayer) < (EnemyAIFieldOfView.Fov / 2f))
                {
                    RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, dirToPlayer, EnemyAIFieldOfView.ViewDistance);
                    if (raycastHit2D.collider.gameObject == Target)
                        spawnBullet();
                }
            }
        }
    }

    private void spawnBullet()
    {
        AudioManager.PlaySoundEffect("Shoot");
        Instantiate(BulletPrefab, BulletFire.position, BulletFire.rotation);
    }

    private IEnumerator enemyDropMine(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            if (EnemyNumOfMines > 0 && enemyRigidBody.velocity.magnitude != 0)
            {
                AudioManager.PlaySoundEffect("DropMine");
                Instantiate(MinePrefab, MineDrop.position, MineDrop.rotation);
                EnemyNumOfMines--;
            }
        }
    }
}
