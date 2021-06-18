using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject level;
    public GameObject player;
    public GameObject redSlime;
    public GameObject blueSlime;
    public GameObject greenSlime;

    public GameObject ground_lvl_0;
    public GameObject ground_lvl_1;
    public GameObject ground_lvl_2;
    public GameObject black_tile;

    public GameObject left_ground_lvl_0;
    public GameObject right_ground_lvl_0;

    public GameObject left_ground_lvl_1;
    public GameObject right_ground_lvl_1;

    public GameObject corner_right_up_lvl_0;
    public GameObject corner_left_up_lvl_0;
    public GameObject corner_left_down_lvl_0;
    public GameObject corner_right_down_lvl_0;

    public GameObject corner_left_lvl_1;
    public GameObject corner_right_lvl_1; 
    public GameObject corner_down_left_lvl_1;
    public GameObject corner_left_down_lvl_1;
    public GameObject corner_right_down_lvl_1;
    public GameObject corner_down_right_lvl_1;

    public GameObject corner_up_left_lvl_1;
    public GameObject corner_up_right_lvl_1;

    public GameObject roof_bush_0;
    public GameObject roof_bush_1;
    public GameObject roof_bush_2;
    public GameObject roof_bush_3;

    public GameObject spikes;

    GameObject platformsParent;
    GameObject tilesParent;
    GameObject enemiesParent;

    //-------------
    // CONSTANTS

    const int TILE_SIZE = 1;

    // Gap Values

    const int MIN_GAP_WIDTH = 4;
    const int MAX_GAP_WIDTH = 8;


    // Platform Values

    const int MIN_PLATFORM_WIDTH = 5;
    const int MAX_PLATFORM_WIDTH = 13;

    const int MIN_PLATFORM_HEIGHT = 2;
    const int MAX_PLATFORM_HEIGHT = 8;

    const int MIN_Y_TILE_MAP = 4;
    const int MAX_Y_TILE_MAP = 30;

    // Map Values

    const int ARRAY_TO_MAP_X_OFFSET = -28;
    const int ARRAY_TO_MAP_Y_OFFSET = -22;

    const int MAP_WIDTH = 136;
    const int MAP_HEIGHT = 45;

    //---------------
    // PRIVATE DATA

    private float player_init_y;
    private List<int> roof_slimes_x;
    private List<float> roof_bush_slimes_x;
    private int[,] mapData;
    private GameObject[] tiles;
    private GameObject[] slimes;
    private enum tileNames
    {
        empty,
        ground_lvl_0, //1 --
        ground_lvl_1, //2 --
        ground_lvl_2, //3 --
        black_tile, //4 

        right_ground_lvl_0, //5 --
        left_ground_lvl_0, //6 --

        right_ground_lvl_1, //7 --
        left_ground_lvl_1, //8 --

        corner_right_up_lvl_0, //9
        corner_left_up_lvl_0, //10
        corner_left_down_lvl_0, //11
        corner_right_down_lvl_0, //12

        corner_left_lvl_1, //13
        corner_right_lvl_1, //14
        corner_down_left_lvl_1, //15
        corner_left_down_lvl_1, //16
        corner_right_down_lvl_1, //17
        corner_down_right_lvl_1, //18
        corner_up_left_lvl_1, // 19
        corner_up_right_lvl_1, //20
        roof_lvl_0, //21
        roof_lvl_1, //22
        roof_lvl_2, //23
        roof_bush_0, //24
        roof_bush_1, //25
        roof_bush_2, //26
        roof_bush_3 //27
    }

    // Start is called before the first frame update
    void Start()
    {

        tiles = new GameObject[]{
            null,
            ground_lvl_0,
            ground_lvl_1,
            ground_lvl_2,
            black_tile,

            right_ground_lvl_0,
            left_ground_lvl_0,

            right_ground_lvl_1,
            left_ground_lvl_1,

            corner_right_up_lvl_0,
            corner_left_up_lvl_0,
            corner_left_down_lvl_0,
            corner_right_down_lvl_0,

            corner_left_lvl_1,
            corner_right_lvl_1,
            corner_down_left_lvl_1,
            corner_left_down_lvl_1,
            corner_right_down_lvl_1,
            corner_down_right_lvl_1,
            corner_up_left_lvl_1,
            corner_up_right_lvl_1,
            null,
            null,
            null,
            roof_bush_0,
            roof_bush_1,
            roof_bush_2,
            roof_bush_3
        };

        slimes = new GameObject[]{
            redSlime,
            greenSlime,
            blueSlime
        };

        tilesParent = new GameObject("Tiles");
        platformsParent = new GameObject("Colliders");
        enemiesParent = new GameObject("Enemies");
        roof_slimes_x = new List<int>();
        roof_bush_slimes_x = new List<float>();
        generateMapData();

        int roof_bush_count = 3;
        for (int i = 0; i < MAP_WIDTH; i++)
        {
            for (int j = 0; j < MAP_HEIGHT; j++)
            {
                int tileNum = mapData[i, j];
                if (tileNum != 0)
                {
                    if (tileNum == (int)tileNames.ground_lvl_0 || tileNum == (int)tileNames.ground_lvl_1 || tileNum == (int)tileNames.ground_lvl_2)
                    {
                        int ground_tile = Random.Range(0, 6);
                        CreateChildPrefab(tiles[tileNum].transform.GetChild(ground_tile).gameObject, tilesParent, i + ARRAY_TO_MAP_X_OFFSET, j + ARRAY_TO_MAP_Y_OFFSET, -1, Quaternion.identity);
                        if (tileNum == (int)tileNames.ground_lvl_0 && Random.Range(0, 15) == 0)
                        {
                            CreateChildPrefab(spikes.transform.GetChild(Random.Range(0, 4)).gameObject, tilesParent, i + ARRAY_TO_MAP_X_OFFSET, j + ARRAY_TO_MAP_Y_OFFSET, -1, Quaternion.identity);
                        }
                    }
                    else if (tileNum == (int)tileNames.right_ground_lvl_0 || tileNum == (int)tileNames.left_ground_lvl_0 || tileNum == (int)tileNames.right_ground_lvl_1 || tileNum == (int)tileNames.left_ground_lvl_1)
                    {
                        int ground_tile = Random.Range(0, 4);
                        CreateChildPrefab(tiles[tileNum].transform.GetChild(ground_tile).gameObject, tilesParent, i + ARRAY_TO_MAP_X_OFFSET, j + ARRAY_TO_MAP_Y_OFFSET, -1, Quaternion.identity);
                    }
                    else if (tileNum == (int)tileNames.roof_lvl_0 || tileNum == (int)tileNames.roof_lvl_1 || tileNum == (int)tileNames.roof_lvl_2)
                    {
                        roof_bush_count = 3;
                        int ground_tile = Random.Range(0, 6);
                        CreateChildPrefab(tiles[tileNum - 20].transform.GetChild(ground_tile).gameObject, tilesParent, i + ARRAY_TO_MAP_X_OFFSET, j + ARRAY_TO_MAP_Y_OFFSET, -1, Quaternion.Euler(0f, 0f, 180f));
                    }
                    else if (tileNum == (int)tileNames.roof_bush_0 || tileNum == (int)tileNames.roof_bush_1 || tileNum == (int)tileNames.roof_bush_2 || tileNum == (int)tileNames.roof_bush_3)
                    {
                        //Debug.Log("roof_bush_count: " + roof_bush_count);
                        CreateChildPrefab(tiles[tileNum].transform.GetChild(roof_bush_count).gameObject, tilesParent, i + ARRAY_TO_MAP_X_OFFSET, j + ARRAY_TO_MAP_Y_OFFSET, -1, Quaternion.identity);
                        roof_bush_count--;
                        if (roof_bush_count < 0)
                        {
                            roof_bush_count = 3;
                        }
                    }
                    else
                    {
                        CreateChildPrefab(tiles[tileNum].gameObject, tilesParent, i + ARRAY_TO_MAP_X_OFFSET, j + ARRAY_TO_MAP_Y_OFFSET, -1, Quaternion.identity);
                    }

                }
            }
        }
        foreach (int i in roof_slimes_x)
        {
            GameObject slime = CreateChildPrefab(slimes[Random.Range(0, 3)], enemiesParent, i + ARRAY_TO_MAP_X_OFFSET, 39 + ARRAY_TO_MAP_Y_OFFSET, -2, Quaternion.identity);
            slime.GetComponent<SlimeController>().type = SlimeController.types.moving;
        }
        foreach (float i in roof_bush_slimes_x)
        {
            GameObject slime = CreateChildPrefab(slimes[Random.Range(0, 3)], enemiesParent, i + ARRAY_TO_MAP_X_OFFSET, 37.5f + ARRAY_TO_MAP_Y_OFFSET, -2, Quaternion.identity);
            slime.GetComponent<SlimeController>().type = SlimeController.types.still;
        }
    
        player.transform.position = new Vector3(-25, player_init_y + ARRAY_TO_MAP_Y_OFFSET, player.transform.position.z);
        tilesParent.transform.parent = level.transform;
        platformsParent.transform.parent = level.transform;
        enemiesParent.transform.parent = level.transform;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void generateMapData()
    {
        int x_init_platform = 0;
        int y_platform_distance = 0;
        int x_platform_distance = 0;
        int prev_y_platform_distance = 0;
        int gap_width = 0;
        int roof_x_init = 0;
        int tiles_since_bush = 0;

        bool roof_bush = false;
        bool gap = false;
        bool up_start_platform = false;
        bool down_start_platform = false;

        mapData = new int[MAP_WIDTH, MAP_HEIGHT];

        for (int i = 0; i < MAP_WIDTH; i++)
        {
            //first column
            if (i == 0)
            {

                x_platform_distance = Random.Range(MIN_PLATFORM_WIDTH, MAX_PLATFORM_WIDTH);
                prev_y_platform_distance = y_platform_distance;
                y_platform_distance = Random.Range(MIN_Y_TILE_MAP, MAX_Y_TILE_MAP);
                player_init_y = y_platform_distance;
                createPlatformCollider(x_platform_distance, y_platform_distance, x_init_platform);
                up_start_platform = true;
            }
            //when gap end
            else if (gap && i == x_init_platform + gap_width)
            {
                x_init_platform = i;
                up_start_platform = true;
                gap = false;
                x_platform_distance = Random.Range(MIN_PLATFORM_WIDTH, MAX_PLATFORM_WIDTH);
                y_platform_distance = getRandomYDistance(MIN_Y_TILE_MAP, MAX_Y_TILE_MAP, prev_y_platform_distance);
                prev_y_platform_distance = 0;
                createPlatformCollider(x_platform_distance, y_platform_distance, x_init_platform);
            }
            //when platform end
            else if (i == x_init_platform + x_platform_distance - 1 && !gap)
            {

                gap = Random.Range(0, 3) == 0 ? true : false;
                x_init_platform = i;

                if (gap)
                {
                    gap_width = Random.Range(MIN_GAP_WIDTH, MAX_GAP_WIDTH);
                    prev_y_platform_distance = y_platform_distance;
                    y_platform_distance = 0;
                }
                else
                {
                    x_platform_distance = Random.Range(MIN_PLATFORM_WIDTH, MAX_PLATFORM_WIDTH);
                    prev_y_platform_distance = y_platform_distance;
                    y_platform_distance = getRandomYDistance(MIN_Y_TILE_MAP, MAX_Y_TILE_MAP, y_platform_distance);
                    createPlatformCollider(x_platform_distance, y_platform_distance, x_init_platform);
                }
                
                
               

                if (y_platform_distance < prev_y_platform_distance)
                {
                    down_start_platform = true;
                }
                else
                {
                    up_start_platform = true;
                }
            }
            //to mantain booleans for one more column
            else if (!(down_start_platform && i == x_init_platform +1) && !(up_start_platform && i == x_init_platform + 1))
            {
                down_start_platform = false;
                up_start_platform = false;
            }
            else
            {

                
            }

            //roof generation

            if (!roof_bush) {

                roof_bush = Random.Range(0, 12) == 0;

                if (roof_bush)
                {
                    tiles_since_bush = 0;
                    roof_x_init = i;
                    if(Random.Range(0,2) == 0)
                    {
                        roof_bush_slimes_x.Add(i+1.5f);
                    }
                }
                else
                {
                    tiles_since_bush++;
                    if(tiles_since_bush >= 6)
                    {
                        if (Random.Range(0, 2) == 0)
                        {
                            roof_slimes_x.Add(i);
                        }
                        tiles_since_bush = 0;
                    }
                }
            }
            else if(i == roof_x_init + 4)
            {
                roof_bush = false;
            }



            //---------------------------------
            //Assigning map Data
            for (int j = MAP_HEIGHT - 1; j >= 0; j--)
            {
                
                if (down_start_platform)
                {
                    if (j <= prev_y_platform_distance && j >= y_platform_distance - 2)
                    {
                        if (i == x_init_platform + 1)
                        {
                            if (j == prev_y_platform_distance)
                            {
                                mapData[i, j] = (int)tileNames.ground_lvl_0;
                            }
                            else if (j == prev_y_platform_distance - 1)
                            {
                                mapData[i, j] = (int)tileNames.corner_right_up_lvl_0;
                            }
                            else if (j == y_platform_distance - 1)
                            {
                                mapData[i, j] = (int)tileNames.corner_right_down_lvl_0;
                            }
                            else if (j == y_platform_distance - 2)
                            {
                                mapData[i, j] = (int)tileNames.corner_down_right_lvl_1;
                            }
                            else
                            {
                                mapData[i, j] = (int)tileNames.right_ground_lvl_0;
                            }
                        }
                        else
                        {
                            if (j == prev_y_platform_distance)
                            {
                                mapData[i, j] = (int)tileNames.ground_lvl_0;
                            }
                            else if (j == prev_y_platform_distance - 1)
                            {
                                mapData[i, j] = (int)tileNames.ground_lvl_1;
                            }
                            else if (j == prev_y_platform_distance - 2)
                            {
                                mapData[i, j] = (int)tileNames.corner_up_right_lvl_1;
                            }
                            else if (j == y_platform_distance - 1)
                            {
                                mapData[i, j] = (int)tileNames.corner_right_lvl_1;
                            }
                            else if (j == y_platform_distance - 2)
                            {
                                mapData[i, j] = (int)tileNames.corner_right_down_lvl_1;
                            }
                            else
                            {
                                mapData[i, j] = (int)tileNames.right_ground_lvl_1;
                            }
                        }
                    }
                    else if (j < y_platform_distance)
                    {
                        mapData[i, j] = (int)tileNames.black_tile;
                    }
                }

                else if (up_start_platform)
                {

                    if (j >= prev_y_platform_distance -2 && j <= y_platform_distance)
                    {
                        if (i == x_init_platform)
                        {
                            if (j == y_platform_distance)
                            {
                                mapData[i, j] = (int)tileNames.ground_lvl_0;
                            }
                            else if (j == y_platform_distance - 1)
                            {
                                mapData[i, j] = (int)tileNames.corner_left_up_lvl_0;
                            }
                            else if (j == prev_y_platform_distance - 1)
                            {
                                mapData[i, j] = (int)tileNames.corner_left_down_lvl_0;
                            }
                            else if (j == prev_y_platform_distance - 2)
                            {
                                mapData[i, j] = (int)tileNames.corner_down_left_lvl_1;
                            }
                            else
                            {
                                mapData[i, j] = (int)tileNames.left_ground_lvl_0;
                            }
                        }
                        else
                        {
                            if (j == y_platform_distance)
                            {
                                mapData[i, j] = (int)tileNames.ground_lvl_0;
                            }
                            else if (j == y_platform_distance - 1)
                            {
                                mapData[i, j] = (int)tileNames.ground_lvl_1;
                            }
                            else if (j == y_platform_distance - 2)
                            {
                                mapData[i, j] = (int)tileNames.corner_up_left_lvl_1;
                            }
                            else if (j == prev_y_platform_distance - 1)
                            {
                                mapData[i, j] = (int)tileNames.corner_left_lvl_1;
                            }
                            else if (j == prev_y_platform_distance - 2)
                            {
                                mapData[i, j] = (int)tileNames.corner_left_down_lvl_1;
                            }
                            else
                            {
                                mapData[i, j] = (int)tileNames.left_ground_lvl_1;
                            }
                        }
                    }
                    else if (j < prev_y_platform_distance)
                    {
                        mapData[i, j] = (int)tileNames.black_tile;
                    }
                }
                else if (gap)
                {
                    mapData[i, j] = (int)tileNames.empty;
                }
                
                /*
                else if(j == MAP_HEIGHT - 2)
                {

                }
                */

                else if (j == y_platform_distance)
                {
                    mapData[i,j] = (int)tileNames.ground_lvl_0;
                }

                else if (j == y_platform_distance - 1)
                {
                    mapData[i,j] = (int)tileNames.ground_lvl_1;
                    if (down_start_platform)
                    {
                        mapData[i, j] = (int)tileNames.right_ground_lvl_0;
                    }
                    else if (up_start_platform)
                    {
                        mapData[i, j] = (int)tileNames.left_ground_lvl_0;
                    }
                }

                else if (j == y_platform_distance - 2)
                {
                    mapData[i,j] = (int)tileNames.ground_lvl_2;

                }

                else if (j <= y_platform_distance - 3)
                {
                    mapData[i,j] = (int)tileNames.black_tile;
                }

                else
                {
                    mapData[i,j] = (int)tileNames.empty;
                }



                if (roof_bush)
                {
                    if (j >= MAP_HEIGHT - 6 && j< MAP_HEIGHT - 2)
                    {
                        if (i == roof_x_init)
                        {
                            mapData[i, j] = (int)tileNames.roof_bush_0;
                        }
                        else if (i == roof_x_init + 1)
                        {
                            mapData[i, j] = (int)tileNames.roof_bush_1;
                        }
                        else if (i == roof_x_init + 2)
                        {
                            mapData[i, j] = (int)tileNames.roof_bush_2;
                        }
                        else if (i == roof_x_init + 3)
                        {
                            mapData[i, j] = (int)tileNames.roof_bush_3;
                        }
                    }
                }
                else
                {
                    if (j == MAP_HEIGHT - 5)
                    {
                        mapData[i, j] = (int)tileNames.roof_lvl_0;
                    }
                    else if (j == MAP_HEIGHT - 4)
                    {
                        mapData[i, j] = (int)tileNames.roof_lvl_1;
                    }
                    else if (j == MAP_HEIGHT - 3)
                    {
                        mapData[i, j] = (int)tileNames.roof_lvl_2;
                    }

                }

                if (j >= MAP_HEIGHT - 2)
                {
                    mapData[i, j] = (int)tileNames.black_tile;
                }

                //Debug.Log(mapData[i,j]);
            }
        }
    }
    private void createPlatformCollider(int x, int y, int x_init)
    {
        GameObject platform = new GameObject("Platform");
        platform.layer = 8;
        platform.transform.Translate(new Vector3(x_init + ARRAY_TO_MAP_X_OFFSET, ARRAY_TO_MAP_Y_OFFSET, 0));
        platform.AddComponent<BoxCollider2D>();
        BoxCollider2D groundBC = platform.GetComponent<BoxCollider2D>();
        groundBC.size = new Vector2(x + 1, y - 0.5f);
        groundBC.offset = new Vector2((float)x / 2, ((float)y / 2));
        platform.transform.parent = platformsParent.transform;
    }

    GameObject CreateChildPrefab(GameObject prefab, GameObject parent, float x, float y, float z, Quaternion rotation)
    {
        var myPrefab = Instantiate(prefab, new Vector3(x, y, z), rotation);
        myPrefab.transform.parent = parent.transform;
        return myPrefab;
    }

    private int getRandomYDistance(int min, int max, int prevY)
    {
        int sum = Random.Range(MIN_PLATFORM_HEIGHT, MAX_PLATFORM_HEIGHT) * (Random.Range(0, 2) == 0 ? -1 : 1);
        int newY = prevY + sum;

        if (newY > max || newY < min)
        {
            newY = getRandomYDistance(min, max, prevY);
        }

        return newY;
        
    }

}


