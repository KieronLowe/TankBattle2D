using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateSpawn : MonoBehaviour
{
    public GameObject Crate;
    public Transform[] CrateSpawns;
    public static bool IsCrateSpawned;

    private int randSpawn;

    void Start()
    {
        IsCrateSpawned = false;
        if (!IsCrateSpawned)
            StartCoroutine(respawnCrate(15.0f));
    }

    private IEnumerator respawnCrate(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            if (!IsCrateSpawned)
            {
                randSpawn = Random.Range(0, CrateSpawns.Length);
                Instantiate(Crate, CrateSpawns[randSpawn].position, Quaternion.identity);
                IsCrateSpawned = true;
            }
        }
    }
}
