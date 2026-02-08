using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BMBItems : BattleMenuButton
{
    [SerializeField] private BattleCamera cam;
    [SerializeField] private BattleMenu menu;
    [SerializeField] private Vector3 menuTarget;

    public override void OnClick()
    {
        if (menu.locked)
        {
            menu.ResetPosition();
            cam.SetTarget(0);
        }
        else 
        {
            cam.SetTarget(2);
            menu.target = menuTarget;
        }
        
    }
}