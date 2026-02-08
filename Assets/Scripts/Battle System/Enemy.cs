using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Enemy
{
    public string name;
    // Hit points
    public float health;
    // Attack damage
    public float damage;
    // How many seconds pass between attacks
    public float attackSpeed;
    // How many seconds it takes to complete the path
    public float moveTime;
    public GameObject attackPrefab;
    public Path path;
}
