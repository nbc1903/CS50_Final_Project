using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject level;
    public GameObject ground_lvl_0;
    public GameObject ground_lvl_1;
    public GameObject ground_lvl_2;
    public GameObject black_tile;
    public GameObject side_ground_lvl_0;
    public GameObject side_ground_lvl_1;

    const int TILE_SIZE = 1;
    const float GROUND_BC_Y_OFFSET = -0.7f;
    const float GROUND_BC_Y_SIZE = 0.9f;

    const float ERROR_CORRECTION_OFFSET = 0.01f;

    const int WIDTH_TIMES_TILES = 150;
    const int HEIGHT_TIMES_TILES = 20;

    private bool[][] mapData;

    // Start is called before the first frame update
    void Start()
    {
        
        
        int tile_x = -35;
        GameObject Platform1 = new GameObject("Platform1");
        Platform1.layer = 8;
        Platform1.transform.Translate(new Vector3(tile_x, 0, 0));

        for (int j = 20; j < HEIGHT_TIMES_TILES; j--)
        {
            int ground_tile = Random.Range(0, ground_lvl_0.transform.childCount);
            CreateChildPrefab(ground_lvl_0.transform.GetChild(ground_tile).gameObject, Platform1, tile_x, 0, -1);
            CreateChildPrefab(ground_lvl_1.transform.GetChild(ground_tile).gameObject, Platform1, tile_x, -TILE_SIZE, -1);
            CreateChildPrefab(ground_lvl_2.transform.GetChild(ground_tile).gameObject, Platform1, tile_x, -TILE_SIZE * 2, -1);
        }

        /*
        for (int i = 0; i <= WIDTH_TIMES_TILES; i++)
        {
            int ground_tile = Random.Range(0, ground_lvl_0.transform.childCount);
            CreateChildPrefab(ground_lvl_0.transform.GetChild(ground_tile).gameObject, Platform1, tile_x, 0, -1);
            CreateChildPrefab(ground_lvl_1.transform.GetChild(ground_tile).gameObject, Platform1, tile_x, -TILE_SIZE, -1);
            CreateChildPrefab(ground_lvl_2.transform.GetChild(ground_tile).gameObject, Platform1, tile_x, -TILE_SIZE * 2, -1);

            float tile_y = -TILE_SIZE * 3;
            for (int j = 0; j < HEIGHT_TIMES_TILES; j++)
            {
                CreateChildPrefab(black_tile, Platform1, tile_x, tile_y, -1);
                tile_y -= TILE_SIZE;
            }
            tile_x += 1;
        }
        createBoxColliderGround(Platform1);
        */
        //Platform1.transform.Translate(new Vector3(ERROR_CORRECTION_OFFSET, 0, 0));
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void generateMapData()
    {
        for (int i = 0; i <= WIDTH_TIMES_TILES; i++)
        {
            int x_platform_distance = Random.Range(2, 10);
            int y_platform_distance = Random.Range(-18, 18);
            for (int j = 20; j < HEIGHT_TIMES_TILES; j--)
            {

            }
        }
    }
    private void createBoxColliderGround(GameObject ground)
    {
        ground.AddComponent<BoxCollider2D>();
        BoxCollider2D groundBC = ground.GetComponent<BoxCollider2D>();
        groundBC.size = new Vector2((TILE_SIZE * WIDTH_TIMES_TILES) + TILE_SIZE, GROUND_BC_Y_SIZE);
        groundBC.offset = new Vector2(TILE_SIZE * WIDTH_TIMES_TILES / 2, GROUND_BC_Y_OFFSET);

    }

    GameObject CreateChildPrefab(GameObject prefab, GameObject parent, float x, float y, float z)
    {
        var myPrefab = Instantiate(prefab, new Vector3(x, y, z), Quaternion.identity);
        myPrefab.transform.parent = parent.transform;
        return myPrefab;
    }
}
