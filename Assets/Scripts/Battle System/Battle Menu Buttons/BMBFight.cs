using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BMBFight : BattleMenuButton
{
    private const float BattleDuration = 15f;
    
    [SerializeField] private BattleCamera cam;
    [SerializeField] private BattleMenu menu;
    [SerializeField] private Vector3 menuTarget;
    [SerializeField] private EnemyBattleLogic enemy;
    
    public override void OnClick()
    {
        cam.SetTarget(1);
        menu.target = menuTarget;
        enemy.EnableAttacks();

        StartCoroutine(ReturnFromCombat());
    }

    private IEnumerator ReturnFromCombat()
    {
        yield return new WaitForSeconds(BattleDuration);
        menu.ResetPosition();
        enemy.DisableAttacks();
        cam.SetTarget(0);
    }
}
