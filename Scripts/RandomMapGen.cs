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
    GameObject[] powers;
    GameObject coins;

    private GameStateManager t_GameStateManager;
    [Tooltip("Stores the score from previous games, don't change it tho")]
    public int score;

    [Tooltip("Stores the number of lives of Mario")]
    public int lives;

    [Tooltip("Checks the height of the map. although current iteration can be severely affected by this, let's keep it default if you don't know what you're doing")]
    public int height;

    [Tooltip("Provides the Total Width for the Map, the more the length, the longer the map will be")]
    public int width;

    [Tooltip("Provides the horizontal X value for the map to start producing, NO CHANGE INTENDED")]
    public int start_index;

    [Tooltip("Provides the vertical X value for the map to start producing, NO CHANGE INTENDED")]
    public int start_height;

    //Used to create a traditional 2d map as a mask
    private int[,] map;
    private int[,] spawnMap; //A 2d spawn map with different properties and functionalities

    [Tooltip("Provides the height for objects and enemies spawning.")]
    public int SpawnHeight;

    [Tooltip("Gap Size provides the maximun bounded horizontal size for a gap, increase it to make the jumps more difficult")]
    public int gap_size;

    [Tooltip("Provides the max number of gaps in a given map, the higher the number, the higher the chance for map to produce the gaps")]
    public int n_gap;





    // Start is called before the first frame update
    void Start()
    {
        t_GameStateManager = FindObjectOfType<GameStateManager>();
        RetrieveGameState();


        if (score > 5000)
        {
            if (lives <= 1 && n_gap > 2)
            {
                n_gap--;
            }
            else
            {
                n_gap++;
            }
            PCG();
            addGaps();
            GenerateMap();
        }
        else if (score > 12000)
        {
            n_gap = n_gap + 2;
            PCG();
            addGaps();
            GenerateMap();
        }
        else
        {

            PCG();
            addGaps();
            ver_size();
            hor_size();
            GenerateMap();
            /* mapPruning();
             mapPruning();
             fillGaps();
            */
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
        int totalBlocks = 40;
        int totalCoins = 35;
        //Unlike the previous method of 1-dimensional spawning, we are taking things a bit far this time
        spawnMap = new int[3, width];
        //since the spawn width will be the same for both
        int y;

        
        for (int j = 0; j < width - 1; j++)
        {

            //What does this value does? this value represents the different types of spawn objects
            /*This Random value between 1,2 and 3 will define whether we will have
                   1: Coins
                   2: Enemies
                   3: Blocks
                   
             */
            int value = UnityEngine.Random.Range(1, 3);
            if(value==2)
            {
                y = UnityEngine.Random.Range(0, 8);
            }
            else {
                y = UnityEngine.Random.Range(0, 5);
            }

            //for (int k = 0; k < 3 && h!= 0; k++)
            {
                int h = UnityEngine.Random.Range(1, 3);

                for (int l = 0; l < y && j < width; l++)
                {

                    
                    /*//spawnMap[i] = value;
                    *//*
                    This Random value between 1,2 and 3 will define whether we will have
                   1: Coins
                   2: Enemies
                   3: Blocks
                   *//**/

                   if (value == 1 && totalCoins > 0)
                    {
                        spawnMap[h,j] = value;
                        totalCoins--;
                    }
                    if (value == 2 && totalBlocks > 0)
                    {
                        spawnMap[h, j] = value;
                        totalBlocks--;
                    }
                    j++;
                    /*int r = k;
                    while (r >= 0)
                    {
                        map[r, j] = 1;
                        r--;
                    }*/
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
        blocks = new GameObject[3];
        powers = new GameObject[4];
        //Currently I'm compacting it to two but I've other plans as well
        blocks[0] = (Resources.Load("Blocks/Brown Brick Block- Regular")) as GameObject;
        blocks[1] = (Resources.Load("Blocks/Brown Brick Block- MultiCoin")) as GameObject;
        blocks[2] = (Resources.Load("Blocks/Wall Brown Brick Block- Regular")) as GameObject;
        
        powers[0] = (Resources.Load("Blocks/Brown Brick Block- Starman")) as GameObject;
        powers[1] = (Resources.Load("Blocks/Brown Question Block- Powerup")) as GameObject;
        powers[2] = (Resources.Load("Blocks/Brown Question Block- Oneup Light")) as GameObject;
        powers[3] = (Resources.Load("Blocks/Brown Question Block- Coin")) as GameObject;


        ///////////////MISC TWEAKS HERE/////////////////
        // here you can change the number of 
        // multi coins
        // starman power
        // static bricks
        // destroyable bricks
        // Powerups
        // Oneup Light Power
        // Number of Coins
        //  int Power_count = 3;
        int powerup = 3;
        int life_up = 2;
        int multi_coins = 5;
        int starman = 2;
        
        //Basically we are restricting the number of power-ups supplied to the player, as its cheating :p

        ///////////////MISC TWEAKS END//////////////////
        for(int i=2;i>= 0;i--)
        {
            for (int j = 0; j < width; j++)
            {
                // pos = new Vector2(j, i);
                if (spawnMap[i, j] == 1)
                {
                    spawnPos = new Vector2(start_index + j, i + SpawnHeight);
                    Instantiate(coins, spawnPos, transform.rotation);
                }
                if (spawnMap[i, j] == 2)
                {
                    //stores the spawn position for the given powerup or brick

                    spawnPos = new Vector2(start_index + j,i + SpawnHeight);

                    //Since there are 7 different types of spawning objects therefore we 
                    //can make it random for all the possible combinations
                    int spawn_type = UnityEngine.Random.Range(0, 7);
                    if(spawn_type == 0)
                    {
                        Instantiate(blocks[0], spawnPos, transform.rotation);
                    }
                    if(spawn_type == 1 && multi_coins >0)
                    {
                        //Instantiate a multi-coin
                        Instantiate(blocks[1], spawnPos, transform.rotation);
                        multi_coins--;
                    }
                    if(spawn_type == 2)
                    {
                        Instantiate(blocks[2], spawnPos, transform.rotation);
                    }
                    if(spawn_type == 3 && starman >0)
                    {
                        Instantiate(powers[0], spawnPos, transform.rotation);
                        starman--;
                    }
                    if(spawn_type == 4 && powerup > 0)
                    {
                        Instantiate(powers[1], spawnPos, transform.rotation);
                        powerup--;
                    }
                    if(spawn_type == 5 && life_up > 0)
                    {
                        Instantiate(powers[2], spawnPos, transform.rotation);
                        life_up--;
                    }
                    if(spawn_type == 6)
                    {
                        Instantiate(powers[3], spawnPos, transform.rotation);
                        
                    }

                    // NOW FOR THE POWER UPS
/*                    int r_val = UnityEngine.Random.Range(0, 2);
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
                    if (l_val == 55)
                    {
                        Instantiate(blocks[1], spawnPos, transform.rotation);
                    }*/

                }
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
            width = 79;
        }

        map = new int[height, width]; //generating a 2d array;

        for(int i = 0; i < height; i++)
        {
            for(int j = 0; j < width; j++)
            {
                if (i > height - 4)
                {
                    int value = UnityEngine.Random.Range(0, 2);
                    map[i, j] = value;
                }
                else
                {
                    map[i, j] = 1;
                }
            }
        }

    }

    public void PCG()
    {
        if (height == 0 && width == 0)
        {
            height = 5;
            width = 79;
        }

        map = new int[height, width]; //generating a 2d array;
        int h, y;

        h = UnityEngine.Random.Range(1, 6);
        for(int j = 0; j<width-1 ; j++)
        {
            int last_h = h;
            h = UnityEngine.Random.Range(1, 6);
            //// Don't want gaps?, Just uncomment the upcoming While
            while(Math.Abs(last_h-h) > 3)
            {
                h = UnityEngine.Random.Range(1, 6);
            }

            y =  UnityEngine.Random.Range(1, 10);

            for (int k = 0;k<h;k++)
            {
                for(int l = 0; l < y && j>= 0; l++)
                {
                    map[h,j] = 1;
                    int r = k;
                    while (r >= 0)
                    {
                        map[r, j] = 1;
                        r--;
                    }
                }
            }
        }

    }

    public void addGaps()
    {
       if(n_gap >= width/3)
        {
            n_gap = width / 3; 
            //can't have so many gaps that it becomes impossible to traverse
        }

        int g_count = 0;
        int r_size = UnityEngine.Random.Range(1, gap_size); //stores the cosecutive gap size;
        Console.WriteLine(g_count);
        while(g_count < n_gap)
        {
            for(int i = 0; i < n_gap; i++)
            {
                int j = UnityEngine.Random.Range(0, width);
                //pick any x number for the map to start pruning
                int temp = r_size;
                //check if two consecutive gap sizes aren't equal
                //it looks bad TBH...xD
                if(temp == r_size)
                {
                    r_size = UnityEngine.Random.Range(1, gap_size);
                }

                //NOW lets start pruning
                for(int k = 0; k < height; k++)
                {
                    int t2 = j;
                    for(int q = 0; q<r_size; q++)
                    {
                        if (t2 < width)
                        {
                            map[k, t2] = 0; //PRUNE the given column
                            t2++;
                        }
                    }
                }
                i = i + r_size;
                //based on the size, we'll shift the X axis
                g_count = g_count + r_size;
            }
        }
    }

    public  void addJumps()
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
     
    public void fillGaps()
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
                        while (temp >= 0)
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

    //checking whether map is traversable or not?
    public void hor_size()
    {
/*        
        for(int j=0;j<width;j++)
        {
            if(map[0, j] == 0) //check the horizontal space size and make it smaller
            {
                int k = j;
                int count = 0;
                while( map[0, k] == 0 && k<width-1)
                {
                    ++count;
                    

                    if(count>= 2) //if jump size is greater than 2 blocks
                    {
                        map[0, k] = 1;
                    }
                    k++;
                }

                j = count + j;
            }
        }
*/

        for (int j = width-1; j >= 0; j--)
        {
            if (map[0, j] == 0) //check the horizontal space size and make it smaller
            {
                int k = j;
                int count = 0;
                while (map[0, k] == 0 && k < width - 1)
                {
                    ++count;


                    if (count >= 3) //if jump size is greater than 2 blocks
                    {
                        map[0, k] = 1;
                    }
                    k--;
                }

                j = count - j;
            }
        }
    }

    public void ver_size()
    {
       /* for (int j = 0; j < width; j++)
        {
            if (map[0, j] == 0) //check the Vertical space size and make it smaller
            {
                int k = j;
                int i = 0;
                int count = 0;
                while (map[i, j] == 0 && i < height - 1)
                {
                    ++count;
     
                    if (count >= 2) //if jump size is greater than 2 blocks
                    {
                        map[i, j] = 1;
                    }
                    ++i;
                }

                j = j + count;
            }
        }*/

        ////////////// PARKOUR /////////////
        for (int j = width-1; j >= 0; j--)
        {
            if (map[0, j] == 0) //check the Vertical space size and make it smaller
            {
                int k = j;
                int i = height - 1;
                int count = 0;
                while (map[i, j] == 0 && i > 0)
                {
                    ++count;

                    if (count >= 4) //if jump size is greater than 2 blocks
                    {
                        map[i, j] = 1;
                        map[0, j] = 1;
                    }
                    --i;
                }

                j = j - count;
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
        int pick = UnityEngine.Random.Range(1, 4);
        if(pick == 1)
        {
            go = (Resources.Load("Ground/Brown Ground 1x1")) as GameObject;
        }
        else if (pick == 2)
        {

            go = (Resources.Load("Ground/Green Ground 1x1")) as GameObject;

        }
        else if(pick >= 3)
        {
            go = (Resources.Load("Ground/Grey Brick Ground 1x1")) as GameObject;
        }
        //Instantiate(go, pos, transform.rotation);
        for (int i = 0; i < height; i++)
        {
            for(int j = 0; j < width; j++)
            {
               // pos = new Vector2(j, i);
                if (map[i, j] == 1)
                {
                    pos = new Vector2(start_index+j, i-start_height);
                    //Pro tip, don't change the rotation, just look up how idiotic the Unity 2d mapping works..xD
                    Instantiate(go, pos, transform.rotation);
                }
            }
        }
        
    }


 

    private void RetrieveGameState()
    {
        score = t_GameStateManager.scores;
        /* marioSize = t_GameStateManager.marioSize;
        
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
                //
                /*
                levelGen.CreateMap();
                levelGen.GenerateMap();
                */

                /*
                levelGen.ver_size();
                levelGen.hor_size();*/
                /*
                                levelGen.PCG();
                                levelGen.addGaps();
                                levelGen.hor_size();
                                levelGen.GenerateMap();
                */


                levelGen.CreateSpawnMap();
                levelGen.SpawnObjects();

            }

            if (GUILayout.Button("Clear"))
            {
                //levelGen.clearMap();
            }
        }
    }
}