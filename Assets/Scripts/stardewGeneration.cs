using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class stardewGeneration : MonoBehaviour
{
    // Start is called before the first frame update

    public Tile wallTile;
    public Tilemap tilemap;
    public float wallChance;


    void Start()
    {
        map = new int[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                map[x, y] = (Random.value < wallChance) ? 1 : 0;
            }
        }
        for (int i = 0; i < 5; i++)
        {
            map = SmoothMap(map);
        }
        string output = "\n";
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                output += " " + map[i, j];
                if (map[i, j] == 1)
                {
                    tilemap.SetTile(new Vector3Int(i, j, 0), wallTile);
                }
            }
            output += "\n";
        }


    }
    int width = 80;
    int height = 60;
    int[,] map;

    // Step 1: Initialize random map
    // Update is called once per frame
    void Update()
    {
    }

    int[,] SmoothMap(int[,] oldMap)
    {
        int[,] newMap = new int[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                int neighborWalls = CountWallsAround(x, y, oldMap);
                newMap[x, y] = (neighborWalls > 4) ? 1 : 0;
            }
        }
        return newMap;
    }

    int CountWallsAround(int gridX, int gridY, int[,] map)
    {
        int wallCount = 0;
        int width = map.GetLength(0);
        int height = map.GetLength(1);

        for (int x = gridX - 1; x <= gridX + 1; x++)
        {
            for (int y = gridY - 1; y <= gridY + 1; y++)
            {
                if (x == gridX && y == gridY) continue;

                if (x < 0 || y < 0 || x >= width || y >= height)
                {
                    wallCount++;
                }
                else if (map[x, y] == 1)
                {
                    wallCount++;
                }
            }
        }

        return wallCount;
    }


    



}
