using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Path : MonoBehaviour
{
    public List<Vector2> path = new();
    public bool loops;

    private List<float> segmentLengths = new();
    private float totalLength;

    void Start()
    {
        Vector2 segment;
        for (int i = 1; i < path.Count; i++)
        {
            segment = path[i - 1] - path[i];
            segmentLengths.Add(segment.magnitude);
            totalLength += segment.magnitude;
        }
        
        segment = path[^1] - path[0];
        segmentLengths.Add(segment.magnitude);
        if (loops) totalLength += segment.magnitude;
    }
    
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        for (int i = 0; i < path.Count; i++)
        {
            if (i == 0 && loops)
            {
                Gizmos.DrawLine(path[^1], path[i]);
            }
            else if (i > 0)
            {
                Gizmos.DrawLine(path[i - 1], path[i]);
            }
            Gizmos.DrawSphere(path[i], 0.1f);
        }
    }

    public Vector2 GetPoint(float percent)
    {
        float length = percent * totalLength;
        int i = 0;
        while (length > segmentLengths[i])
        {
            length -= segmentLengths[i];
            i++;
        }

        if (i >= path.Count) i = 0;
        float interpolate = length / segmentLengths[i];
        
        Vector2 point1 = path[i];
        Vector2 point2;
        if (i < path.Count - 1)
        {
            point2 = path[i + 1];
        }
        else
        {
            if (loops) point2 = path[0]; 
            else point2 = path[i];
        }

        return Vector2.Lerp(point1, point2, interpolate);
    }
}