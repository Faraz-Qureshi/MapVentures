using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEnemySpawn : MonoBehaviour
{
    GameObject go;
    //public Transform[] spawnPoints;
    private GameObject[] enemyPrefabs;
    public int height;
    public int width;
    public int Enemy_count; 
    //Preserving enemy count based on player's performance
    private int count;
    private void Start()
    {
        SpawnRandomEnemies();
    }


   /* private void Update()
    {
        int randEnemy = Random.Range(0, enemyPrefabs.Length);
        int randSpawnPoint = Random.Range(0, spawnPoints.Length);
        if (count < 5)
        {

            Instantiate(enemyPrefabs[randEnemy], spawnPoints[randSpawnPoint].position, transform.rotation);

        }
        count++;
    }*/
    
    public void SpawnRandomEnemies()
    {
       

        go = GameObject.Find("MapGen");
        RandomMapGen rnd = go.GetComponent <RandomMapGen>();
        width = rnd.width;

        height = rnd.SpawnHeight;

        Enemy_count = 10; //just for initial runs;
        if (rnd.score > 4500)
        {
            Enemy_count = 15;//increasing enemies in case of player's performance
        }
        int[] mapping = new int[rnd.width]; 
        //the basic idea to use it to ensure not any two enemies reside over each other.

        enemyPrefabs = new GameObject[3]; //currently storing only 3 enemy types;
        enemyPrefabs[0] = Resources.Load("Enemies/Brown Goomba") as GameObject;
        enemyPrefabs[1] = (Resources.Load("Enemies/Green Koopa")) as GameObject;
        enemyPrefabs[2] = (Resources.Load("Enemies/Red Winged Koopa")) as GameObject;

        Vector2 spawnPos; //to store the spawning position

        
        while (Enemy_count > 0)
        {
            int rand_val = UnityEngine.Random.Range(15, width-1);
            if (mapping[rand_val] == 0)
            {
                int selectEnemy = UnityEngine.Random.Range(0, 2);
                spawnPos = new Vector2(rand_val, height);
                    if (selectEnemy == 0)
                    {
                        Instantiate(enemyPrefabs[0], spawnPos, transform.rotation);
                    }
                    if (selectEnemy == 1)
                    {
                        Instantiate(enemyPrefabs[1], spawnPos, transform.rotation);
                    }
                    if (selectEnemy == 2)
                    {
                        Instantiate(enemyPrefabs[2], spawnPos, transform.rotation);
                    }

                mapping[rand_val] = 1;
                Enemy_count--;
            }
            
        }

    }
}
