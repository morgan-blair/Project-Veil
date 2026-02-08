using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum MenuOption
{
   Fight,
   Items,
   Check,
   Flee
}

public class BattleMenu : MonoBehaviour
{
    [SerializeField] List<GameObject> menu = new();
    [SerializeField] float selectedScale;
    private int selected = 0;
    private int selectedPrev = 0;
    private RectTransform rectTransform;

    public bool locked = false;
    
    public Vector3 target;
    private Vector3 originalPosition;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        originalPosition = rectTransform.anchoredPosition;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject obj;
        Vector3 scale;
        float t = 0.1f;
        if (selectedPrev != selected)
        {
            obj = menu[selectedPrev];
            scale = obj.transform.localScale;
            obj.transform.localScale = new Vector3(
                Mathf.Lerp(scale.x, 1, t),
                Mathf.Lerp(scale.y, 1, t),
                Mathf.Lerp(scale.z, 1, t)
            );
        }
        
        obj = menu[selected];
        scale = obj.transform.localScale;
        obj.transform.localScale = new Vector3(
            Mathf.Lerp(scale.x, selectedScale, t),
            Mathf.Lerp(scale.y, selectedScale, t),
            Mathf.Lerp(scale.z, selectedScale, t)
        );
        
        rectTransform.anchoredPosition = Vector3.Lerp(rectTransform.anchoredPosition, target, 10f * Time.deltaTime);
    }

    public void Next()
    {
        if (locked) return;
        
        if (selected < menu.Count - 1)
        {
            selectedPrev = selected;
            selected++;
        }
    }

    public void Prev()
    {
        if (locked) return;
        
        if (selected > 0)
        {
            selectedPrev = selected;
            selected--;
        }
    }

    public void ResetPosition()
    {
        IEnumerator Reset() {
            yield return new WaitForEndOfFrame();
            target = originalPosition;
            locked = false;
        }
        StartCoroutine(Reset());
    }
    

    public void Click()
    {
        menu[selected].GetComponent<BattleMenuButton>().OnClick();
        locked = true;
    }
}
