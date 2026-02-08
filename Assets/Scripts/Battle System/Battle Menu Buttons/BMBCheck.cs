using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BMBCheck : BattleMenuButton
{
    [SerializeField] private BattleCamera cam;
    [SerializeField] private BattleMenu menu;
    [SerializeField] private Vector3 menuTarget;

    public override void OnClick()
    {
        cam.SetTarget(1);
        menu.target = menuTarget;
    }
}