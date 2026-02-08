using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class DialogueChoiceButton : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    [Header("Components")]
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI choiceText;
    private string baseText;

    private int choiceIndex = -1;

    public void SetChoiceText(string choiceTextString)
    {
        baseText = choiceTextString;
        choiceText.text = choiceTextString;
        choiceText.color = Color.white;
        if (EventSystem.current.currentSelectedGameObject == this.gameObject)
        {
            choiceText.color = Color.yellow;
            choiceText.text = "> " + baseText + " <";
        }
    }

    public void SetChoiceIndex(int choiceIndex)
    {
        this.choiceIndex = choiceIndex;
/*        choiceText.color = Color.white;*/
    }

    public void SelectButton()
    {
        button.Select();
    }

    public void OnSelect(BaseEventData eventData)
    {
        GameEventsManager.instance.dialogueEvents.UpdateChoiceIndex(choiceIndex);
        choiceText.color = Color.yellow;
        choiceText.text = "> " + baseText + " <";
    }

    public void OnDeselect(BaseEventData eventData)
    {
        choiceText.color = Color.white;
        choiceText.text = baseText;
    }



}
