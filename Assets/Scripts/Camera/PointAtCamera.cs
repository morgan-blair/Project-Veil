using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAtCamera : MonoBehaviour
{
    Camera cam;
    [SerializeField] SpriteRenderer spriteRenderer;

    void Start()
    {
        cam = Camera.main;
    }
    
    // Update is called once per frame
    void Update()
    {
        spriteRenderer.transform.eulerAngles = cam.transform.eulerAngles;
    }
}
