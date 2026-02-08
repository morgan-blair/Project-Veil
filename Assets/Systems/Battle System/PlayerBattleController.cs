using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerBattleController : MonoBehaviour
{
    public float health;
    [SerializeField] private Weapon weapon;
    [SerializeField] private GameObject attackObject;
    private bool attacking = false;
    private float attackTimer = 0;
    private Vector2 attackStart = Vector2.zero;
    [SerializeField] private Vector2 cursorPos = Vector2.zero;
    private Vector2 cursorVelocity = Vector2.zero;
    private Camera cam;
    private LineRenderer lr;
    [SerializeField] private GameObject cursorObject;
    [SerializeField] private Image cursorImage;
    private Image cooldownImage;

    private const float MIN_SLASH = 0.02f;

    private void Start()
    {
        cam = Camera.main;
        lr = GetComponent<LineRenderer>();
        cooldownImage = cursorObject.GetComponent<Image>();
        cursorPos = new Vector2(Screen.width/2, Screen.height/2);
        Cursor.lockState =  CursorLockMode.Locked;
    }

    private void Update()
    {
        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
            cursorImage.color = Color.red;
            cooldownImage.fillAmount = 1 - attackTimer / weapon.attackSpeed;
        }
        else
        {
            cooldownImage.fillAmount = 0;
            cursorImage.color = Color.white;
        }
        
        cursorPos += cursorVelocity;
        cursorObject.transform.position = cursorPos;
        cursorPos.x = Mathf.Clamp(cursorPos.x, 0, Screen.width);
        cursorPos.y = Mathf.Clamp(cursorPos.y, 0, Screen.height);
        
        if (attacking)
        {
            Vector2 fullTranslate = GetCursorPos() - attackStart;
            fullTranslate = fullTranslate.normalized * Mathf.Min(fullTranslate.magnitude, weapon.attackSize);
            Vector2 lrEndPoint = attackStart + fullTranslate;
            lr.SetPosition(1, lrEndPoint);
        }
    }

    private Vector2 GetCursorPos()
    {
        return cam.ScreenToWorldPoint(new Vector3(cursorPos.x, cursorPos.y, cam.nearClipPlane));
    }
    
    private void OnAttack(InputValue value)
    {
        bool pressed = value.Get<float>() > 0.5f;
        if (!pressed && attacking)
        {
            // Release
            lr.enabled = false;
            Vector2 fullTranslate = GetCursorPos() - attackStart;
            fullTranslate = fullTranslate.normalized * Mathf.Min(fullTranslate.magnitude, weapon.attackSize);
            if (fullTranslate.magnitude < MIN_SLASH) return;
            GameObject attack = Instantiate(attackObject, attackStart, Quaternion.identity);
            PlayerAttack attackScript = attack.GetComponent<PlayerAttack>();
            attackScript.target = attackStart + fullTranslate;
            attackScript.damage = weapon.damage;
            SoundManager2.Instance.PlaySound2D("Slash");
            attacking = false;
            attackTimer = weapon.attackSpeed;
            return;
        }
            
        if (!pressed || attackTimer > 0) return;

        // Press
        attacking = true;
        attackStart = GetCursorPos();
        lr.enabled = true;
        lr.SetPosition(0, GetCursorPos());
    }
    
    private void OnAttackMove(InputValue value)
    {
        cursorVelocity = value.Get<Vector2>();
    }

    private void OnMousePos(InputValue value)
    {
        //cursorPos = value.Get<Vector2>();
        //cursorObject.transform.position = cursorPos;
        //cursorObject.transform.position = GetCursorPos();
    }
}
