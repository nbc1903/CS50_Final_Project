﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{

    private float length;
    private float startPos;
    public float parallaxEffect;

    public float camera_size_offset;
    public GameObject cam;
    

    void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;        
    }

    void Update()
    {

        float temp = cam.transform.position.x * (1 - parallaxEffect);
        float distance = cam.transform.position.x * parallaxEffect;
        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);

        if (temp > startPos + length - camera_size_offset) startPos += length;
        else if (temp < startPos - length + camera_size_offset) startPos -= length;
    }
}
