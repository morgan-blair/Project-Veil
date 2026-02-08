using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] float damage;
    private PlayerBattleController player;

    private void Start()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");
        player = playerObject.GetComponent<PlayerBattleController>();
    }

    public void AnimationEnd()
    {
        player.health -= damage;
        Destroy(gameObject);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerAttack"))
        {
            SoundManager2.Instance.PlaySound2D("Parry");
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
