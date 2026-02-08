using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [Header("Dialogue (optional)")]
    [SerializeField] private string dialogueKnotName;

    void Interact(GameObject source)
    {
        source.GetComponent<Animator>().SetFloat("MoveMagnitude", 0);

        DialogueManager dialogueManager = FindObjectOfType<DialogueManager>();
        DialoguePanelUI dialoguePanelUI = FindObjectOfType<DialoguePanelUI>();
        if (dialogueManager == null)
            return;

        if (dialogueManager.IsDialoguePlaying)
        {
            if (dialoguePanelUI.isTyping)
            {
                dialoguePanelUI.SkipTyping();
            }
            else
            {
                dialogueManager.SubmitPressed();
            }
        }
        else
        {
            if (!string.IsNullOrEmpty(dialogueKnotName))
            {
                GameEventsManager.instance.dialogueEvents.EnterDialogue(dialogueKnotName);
            }
        }
    }
}
