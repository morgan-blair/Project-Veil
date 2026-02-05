using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCamera : MonoBehaviour
{
    [SerializeField] private List<Transform> targets = new();
    private Transform currentTarget;
    private Camera cam;
    private float baseSize;
    
    // Start is called before the first frame update
    void Start()
    {
        if (targets.Count <= 0)
        {
            Debug.LogError("Battle Camera: No targets designated. Please add at least 1 target to the Battle Camera.");
            enabled = false;
        }
        
        cam = GetComponent<Camera>();
        currentTarget = targets[0];
        baseSize = cam.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position != currentTarget.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, currentTarget.position, 0.1f);
        }

        if (!Mathf.Approximately(cam.orthographicSize, currentTarget.localScale.x * baseSize))
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, currentTarget.localScale.x * baseSize, 0.1f);
        }
    }

    public void SetTarget(int i)
    {
        if (i < 0 || i >= targets.Count)
        {
            Debug.LogError("Battle Camera: Invalid target index: " + i);
            return;
        }
        
        currentTarget = targets[i];
    }
}
