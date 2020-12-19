using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCoinGenerator : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject CoinPrefabs;
    
    private int max;
    private int count = 0;
   
    private void Start()
    {
/*        if (count < spawnPoints.Length)
        {
            this.enabled = true;
        }
        else
        {
            this.enabled = false;
        }*/
        if(count >= spawnPoints.Length)
        {
            this.enabled = false;
        }
    }


    private void Update()
    {
        //int randEnemy = Random.Range(0, CoinPrefabs.Length);
        int randSpawnPoint = Random.Range(0, spawnPoints.Length);

        if (count < spawnPoints.Length)
        {

            Instantiate(CoinPrefabs, spawnPoints[randSpawnPoint].position, transform.rotation);

        }
        count++;
    }
}
