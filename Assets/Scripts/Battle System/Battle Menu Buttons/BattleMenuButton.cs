using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BattleMenuButton : MonoBehaviour
{
    [SerializeField] string buttonName;

    public virtual void OnClick() { }
}