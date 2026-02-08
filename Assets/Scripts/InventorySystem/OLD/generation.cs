using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class generation : MonoBehaviour
{
    // Start is called before the first frame update
    public bool dontMakeALevel;
    public List<GameObject> segs0101 = new List<GameObject>();
    private List<Vector2> mainPathDirections = new List<Vector2>();
    public int numberOfSegments;
    public Vector3 placeToSpawn;
    private AstarPath pathfinding;
    private Vector2 currentMapPos;
    public int mapSize;
    public string filledChar;
    public string emptyChar;
    private Transform wizTransform;
    void Start()
    {
        if (dontMakeALevel)
        {
            numberOfSegments = 0;
        }
        matrix = new string[mapSize, mapSize];
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                matrix[i, j] = emptyChar;
            }
        }
        //  Center is a thing.
        currentMapPos = new Vector2(matrix.GetLength(0) / 2, matrix.GetLength(1) / 2);
        matrix[((int)currentMapPos.x), ((int)currentMapPos.y)] = "S";
        int segmentCount = 0;
        while (segmentCount < numberOfSegments)
        {
            List<Vector2> potentialMoves = new List<Vector2> { new Vector2(1, 0), new Vector2(-1, 0), new Vector2(0, 1), new Vector2(0, -1) };
            List<Vector2> bestPotentialMoves = new List<Vector2>();
            for (int i = potentialMoves.Count - 1; i >= 0; i--)
            {
                if (currentMapPos.x + (int)potentialMoves[i].x >= matrix.GetLength(0) || currentMapPos.x + (int)potentialMoves[i].x < 0 ||
                    currentMapPos.y + (int)potentialMoves[i].y >= matrix.GetLength(0) || currentMapPos.y + (int)potentialMoves[i].y < 0)
                {
                    potentialMoves.RemoveAt(i);
                }
                else if (matrix[(int)currentMapPos.y + (int)potentialMoves[i].y, (int)currentMapPos.x + (int)potentialMoves[i].x] != emptyChar)
                {
                    potentialMoves.RemoveAt(i);
                }
            }
            if (potentialMoves.Count == 0)
            {
                segmentCount = numberOfSegments;
            }
            else
            {
                int mostPaths = 0;
                for (int i = potentialMoves.Count - 1; i >= 0; i--)
                {
                    Vector2 movingToPos = new Vector2(currentMapPos.x + potentialMoves[i].x, currentMapPos.y + potentialMoves[i].y);
                    List<Vector2> potentialMoves2 = new List<Vector2> { new Vector2(1, 0), new Vector2(-1, 0), new Vector2(0, 1), new Vector2(0, -1) };
                    for (int j = potentialMoves2.Count - 1; j >= 0; j--)
                    {
                        if (movingToPos.x + (int)potentialMoves2[j].x >= matrix.GetLength(0) || movingToPos.x + (int)potentialMoves2[j].x < 0 ||
                            movingToPos.y + (int)potentialMoves2[j].y >= matrix.GetLength(0) || movingToPos.y + (int)potentialMoves2[j].y < 0)
                        {
                            potentialMoves2.RemoveAt(j);
                        }
                        else if (matrix[(int)movingToPos.y + (int)potentialMoves2[j].y, (int)movingToPos.x + (int)potentialMoves2[j].x] != emptyChar)
                        {
                            potentialMoves2.RemoveAt(j);
                        }
                    }
                    if (potentialMoves2.Count > mostPaths)
                    {
                        mostPaths = potentialMoves2.Count;
                        bestPotentialMoves.Clear();
                        bestPotentialMoves.Add(potentialMoves[i]);
                    }
                    else if (potentialMoves2.Count == mostPaths)
                    {
                        bestPotentialMoves.Add(potentialMoves[i]);
                    }
                }


                Vector2 placeToMove = bestPotentialMoves[Random.Range(0, bestPotentialMoves.Count)];
                currentMapPos = new Vector2(currentMapPos.x + placeToMove.x, currentMapPos.y + placeToMove.y);
                segmentCount += 1;
                if (segmentCount == numberOfSegments)
                {
                    matrix[(int)currentMapPos.y, (int)currentMapPos.x] = "E";
                }
                else
                {
                    matrix[(int)currentMapPos.y, (int)currentMapPos.x] = filledChar;
                    mainPathDirections.Add(placeToMove);
                }
            }

        }

        Vector2 mapGuider = new Vector2(matrix.GetLength(0) / 2, matrix.GetLength(1) / 2);
        for (int i = 0; i < mainPathDirections.Count; i++)
        {
            mapGuider += mainPathDirections[i];
            if (Random.Range(0, 5) == 0)
            {
                List<Vector2> potentialMoves = new List<Vector2> { new Vector2(1, 0), new Vector2(-1, 0), new Vector2(0, 1), new Vector2(0, -1) };
                for (int j = potentialMoves.Count - 1; j >= 0; j--)
                {
                    if (mapGuider.x + (int)potentialMoves[j].x >= matrix.GetLength(0) || mapGuider.x + (int)potentialMoves[j].x < 0 ||
                        mapGuider.y + (int)potentialMoves[j].y >= matrix.GetLength(0) || mapGuider.y + (int)potentialMoves[j].y < 0)
                    {
                        potentialMoves.RemoveAt(j);
                    }
                    else if (matrix[(int)mapGuider.y + (int)potentialMoves[j].y, (int)mapGuider.x + (int)potentialMoves[j].x] != emptyChar)
                    {
                        potentialMoves.RemoveAt(j);
                    }
                }
                if (potentialMoves.Count > 0)
                {
                    Vector2 moveThisWay = potentialMoves[Random.Range(0, potentialMoves.Count)];
                    int branchLength = Random.Range(1, 4);

                    Vector2 branchPos = mapGuider;
                    for (int j = 0; j < branchLength; j++)
                    {
                        branchPos += moveThisWay;

                        if (branchPos.x < 0 || branchPos.x >= matrix.GetLength(0) ||
                            branchPos.y < 0 || branchPos.y >= matrix.GetLength(1))
                        {
                            break;
                        }
                        if (matrix[(int)branchPos.y, (int)branchPos.x] != emptyChar)
                        {
                            break;
                        }

                        matrix[(int)branchPos.y, (int)branchPos.x] = "b";
                    }
                }
            }
        }




        Vector3 centerPos = new Vector3(0, 0, 0);
        Vector3 instantiatePos = centerPos;


        Debug.Log("PENIS!");
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                if (matrix[i, j] != emptyChar)
                {
                    Debug.Log("TEST!");
                    Instantiate(segs0101[0], instantiatePos, Quaternion.identity, transform);
                }
                instantiatePos = new Vector3(instantiatePos.x + 26, instantiatePos.y, instantiatePos.z);
            }
            instantiatePos = new Vector3(0, instantiatePos.y - 15, instantiatePos.z);
        }



        printMatrix();


        pathfinding = GameObject.FindGameObjectWithTag("PathFinder").GetComponent<AstarPath>();
        pathfinding.Scan();
    }

    public string[,] matrix;

    //  KEY:
    //  0 - Empty square
    //  1 - up entrance

    // 0 0 0 0 0 0 0 0 0 0 0
    // 0 0 0 0 0 0 0 0 0 0 0
    // 0 0 0 0 0 0 0 0 0 0 0
    // 0 0 0 0 0 0 0 0 0 0 0
    // 0 0 0 0 0 0 0 0 0 0 0
    // 0 0 0 0 0 1 0 0 0 0 0
    // 0 0 0 0 0 0 0 0 0 0 0
    // 0 0 0 0 0 0 0 0 0 0 0
    // 0 0 0 0 0 0 0 0 0 0 0
    // 0 0 0 0 0 0 0 0 0 0 0
    // 0 0 0 0 0 0 0 0 0 0 0




    // Update is called once per frame
    void Update()
    {
    }

    private void printMatrix()
    {
        string output = "\n";
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                output += " " + matrix[i, j];
            }
            output += "\n";
        }
        Debug.Log(output);
    }
}
