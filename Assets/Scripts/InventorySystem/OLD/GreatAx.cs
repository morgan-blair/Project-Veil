using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreatAx : MonoBehaviour
{
    public string Name
    {
        get
        {
            return "Great Axe";
        }
    }

    public Sprite image = null;

    public Sprite Image
    {
        get
        {
            return image;
        }
    }

    public void OnPickup()
    {
        // Later we'll want to add logic for what exactly happens when player grabs a great ax.
        gameObject.SetActive(false);
    }
}
