using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public int minX;
    public int maxX;
    public int minY;
    public int maxY;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Mathf.Max(minX, Mathf.Min(maxX, player.transform.position.x)), Mathf.Max(minY, Mathf.Min(maxY, player.transform.position.y)), transform.position.z);
    }
}
