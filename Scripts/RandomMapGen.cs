using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;
using UnityEngine.SocialPlatforms;


#if UNITY_EDITOR
using UnityEditor;
#endif

public class RandomMapGen : MonoBehaviour
{

    GameObject go;
    //GameObject[] enemies;
    GameObject[] blocks;
    GameObject coins;

    private GameStateManager t_GameStateManager;
    public int score;
    public int height;
    public int width;
    public int start_index;
    public int start_height;
    private int[,] map;
    private int[] spawnMap; //Different than the 2-d map.

    [Tooltip("Provides the height for objects and enemies spawning.")]
    public int SpawnHeight;
    // Start is called before the first frame update


    void Start()
    {
        t_GameStateManager = FindObjectOfType<GameStateManager>();
        RetrieveGameState();
        if (score > 5000)
        {
            CreateMap();
            addJumps();
            mapPruning();
            GenerateMap();
        }
        else if (score > 8000)
        {
            CreateMap();
            addJumps();
            GenerateMap();
            
        }
        else
        {
            CreateMap();
            fillGaps();
            mapPruning();
            mapPruning();
            fillGaps();
            GenerateMap();
        }
        /////////OBJECT SPAWNING/////////////
        CreateSpawnMap();
        SpawnObjects();

    }

    /*  // Update is called once per frame
      void Update()
      {

      }*/



    public void CreateSpawnMap()
    {
        int totalBlocks = 10;
        int totalCoins = 15;
        spawnMap = new int[width];
        //since the spawn width will be the same for both
        for(int i=0;i<width; i++)
        {
            int value = UnityEngine.Random.Range(0, 4);
            //spawnMap[i] = value;
            /*
            This Random value between 1,2 and 3 will define whether we will have 
            1: Coins
            2: Enemies
            3: Blocks
            */
          
            if(value == 1 && totalCoins > 0)
            {
                spawnMap[i] = value;
                totalCoins--;
            }
            if (value == 2 && totalBlocks > 0)
            {
                spawnMap[i] = value;
                totalBlocks--;
            }


        }
    }
    public void CreateMap()
    {

       /* height = 10;
        width = 50;*/
       if(height==0 && width == 0)
        {
            height = 5;
            width = 85;
        }

        map = new int[height, width]; //generating a 2d array;

        for(int i = 0; i < height; i++)
        {
            for(int j = 0; j < width; j++)
            {
                int value = UnityEngine.Random.Range(0, 2);
                map[i, j] = value;
            }
        }

    }

    void addJumps()
    {
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                if (i == height-1)
                {
                    if (map[i, j] == 0)
                    {
                        int temp = i;
                        while (temp > 0)
                        {
                            map[temp, j] = 0;
                            temp--;
                        }
                    }
                }
            }
        }
    }
    
    void fillGaps()
    {
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                if (i == height - 1) // if you are  in last row
                {
                    if (map[i, j] != 0)
                    {
                        int temp = i;
                        while (temp > 0)
                        {
                            if(map[temp, j] == 0)
                            {
                                map[temp, j] = 1;
                            }
                            temp--;
                        }
                    }
                }
            }
        }
    }
    public void mapPruning()
    {
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                if (j + 3 < width)
                {

                    if (map[i, j] == 0 && map[i, j + 3] == 0)
                    {
                        map[i, j + 3] = 1;
                    }
                }
                if (i + 3 < height)
                {
                    if (map[i, j] == 1 && map[i + 3, j] == 1)
                    {
                        map[i + 3, j] = 0;
                    }
                }
               
            }
        }
    }
    public void GenerateMap()
    {
        //Generating a map using GameObjects instead of tiles, so that you don't 
        //have to manage all that prefab shit (WIP)
        
       
        Vector2 pos;

        //MeshCollider c = quad.GetComponent<MeshCollider>();

        // pos = new Vector2(i, 5);
        //Instantiate(MapPrefab, pos, transform.rotation);
        go = (Resources.Load("Ground/Brown Ground 1x1")) as GameObject;
        //Instantiate(go, pos, transform.rotation);
        for (int i = 0; i < height; i++)
        {
            for(int j = 0; j < width; j++)
            {
               // pos = new Vector2(j, i);
                if (map[i, j] == 1)
                {
                    pos = new Vector2(start_index+j, i-start_height);
                    Instantiate(go, pos, transform.rotation);
                }
            }
        }
        
    }
    public void SpawnObjects()
    {
        Vector2 spawnPos; //to store the vector space position for the object to spawn
                          //Instantiating enemy GameObjects
         //enemies = new GameObject [3];
        

        //Instantiating Coins
        coins = (Resources.Load("Collectibles/Brown Coin")) as GameObject;

        //Instantiating Bricks and PowerUps
        blocks = new GameObject[2];
        //Currently I'm compacting it to two but I've other plans as well
        blocks[0] = (Resources.Load("Blocks/Brown Brick Block- Regular")) as GameObject;
        blocks[1] = (Resources.Load("Blocks/Brown Question Block- Powerup")) as GameObject;

        ///////////////MISC TWEAKS HERE/////////////////
        ///
        int Power_count = 3;
        //Basically we are restricting the number of power-ups supplied to the player, as its cheating :p

        ///////////////MISC TWEAKS END//////////////////

        for (int j = 0; j < width; j++)
        {
            // pos = new Vector2(j, i);
            if (spawnMap[j] == 1)
            {
                spawnPos = new Vector2(start_index + j, SpawnHeight);
                Instantiate(coins, spawnPos, transform.rotation);
            }
            if (spawnMap[j] == 2)
            {
                spawnPos = new Vector2(start_index + j, SpawnHeight);
                int r_val = UnityEngine.Random.Range(0, 2);
                int l_val = UnityEngine.Random.Range(0, 100);
                if (r_val == 1 && Power_count > 0)
                {
                    Instantiate(blocks[1], spawnPos, transform.rotation);
                    Power_count--;
                }
                else if (r_val == 0)
                {
                    Instantiate(blocks[0], spawnPos, transform.rotation);
                }
                if(l_val == 55)
                {
                    Instantiate(blocks[1], spawnPos, transform.rotation);
                }

            }
        }


    }

 

    private void RetrieveGameState()
    {
        score = t_GameStateManager.scores;

        /* marioSize = t_GameStateManager.marioSize;
         lives = t_GameStateManager.lives;
         coins = t_GameStateManager.coins;
         timeLeft = t_GameStateManager.timeLeft;
         hurryUp = t_GameStateManager.hurryUp;*/
    }
}


[CustomEditor(typeof(RandomMapGen))]
public class MapGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        RandomMapGen levelGen = (RandomMapGen)target;


        //Only show the mapsettings UI if we have a reference set up in the editor
        
        {
            //Editor mapSettingEditor = CreateEditor(levelGen.mapSetting);
            // mapSettingEditor.OnInspectorGUI();

            if (GUILayout.Button("Generate"))
            {
                //levelGen.GenerateMap();
                levelGen.CreateMap();
                levelGen.GenerateMap();
                /*
                levelGen.CreateSpawnMap();
                levelGen.SpawnObjects();*/
            }

            if (GUILayout.Button("Clear"))
            {
                //levelGen.clearMap();
            }
        }
    }
}