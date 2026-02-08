using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;

public class DialoguePanelUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject contentParent;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private DialogueChoiceButton[] choiceButtons;

    private void Awake()
    {
        contentParent.SetActive(false);
        ResetPanel();
    }

    private void OnEnable()
    {
        GameEventsManager.instance.dialogueEvents.onDialogueStarted += DialogueStarted;
        GameEventsManager.instance.dialogueEvents.onDialogueFinished += DialogueFinished;
        GameEventsManager.instance.dialogueEvents.onDisplayDialogue += DisplayDialogue;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.dialogueEvents.onDialogueStarted -= DialogueStarted;
        GameEventsManager.instance.dialogueEvents.onDialogueFinished -= DialogueFinished;
        GameEventsManager.instance.dialogueEvents.onDisplayDialogue -= DisplayDialogue;
    }

    private void DialogueStarted()
    {
        contentParent.SetActive(true);
    }

    private void DialogueFinished()
    {
        contentParent.SetActive(false);
        ResetPanel();
    }

    private void DisplayDialogue(string dialogueLine, List<Choice> dialogueChoices)
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        typingCoroutine = StartCoroutine(TypeText(dialogueLine));

        if (dialogueChoices.Count > choiceButtons.Length)
        {
            Debug.LogError("More dialogue choices (" + dialogueChoices.Count + ") came through than are supported (" + choiceButtons.Length + ").");
        }


        foreach (DialogueChoiceButton choiceButton in choiceButtons)
        {
            choiceButton.gameObject.SetActive(false);
        }

        int choiceButtonIndex = dialogueChoices.Count - 1;

        for (int inkChoiceIndex = 0; inkChoiceIndex < dialogueChoices.Count; inkChoiceIndex++)
        {
            Choice dialogueChoice = dialogueChoices[inkChoiceIndex];
            DialogueChoiceButton choiceButton = choiceButtons[choiceButtonIndex];

            choiceButton.gameObject.SetActive(true);
            choiceButton.SetChoiceIndex(inkChoiceIndex);
            choiceButton.SetChoiceText(dialogueChoice.text);

            choiceButtonIndex--;
        }


        if (dialogueChoices.Count > 0)
        {
            DialogueChoiceButton firstButton = choiceButtons[dialogueChoices.Count - 1]; // top-most visible one
            firstButton.SelectButton(); // triggers OnSelect()
            GameEventsManager.instance.dialogueEvents.UpdateChoiceIndex(0);
        }
    }

    private void ResetPanel()
    {
        dialogueText.text = "";
    }



    private Coroutine typingCoroutine;

    private bool skipTyping = false;
    private IEnumerator TypeText(string fullText, float delay = 0.04f)
    {
        float dialogueSpeed = delay;
        dialogueText.text = "";
        skipTyping = false;

        isTyping = true;
        bool skipChars = false;
        bool oneDelay = false;
        string tagText = "";
        foreach (char c in fullText)
        {
            if (skipTyping)
            {
                dialogueText.text = fullText;
                yield break;
            }
            if (c == '<')
            {
                skipChars = true;
            }

            if (oneDelay)
            {
                oneDelay = false;
                dialogueSpeed = delay;
            }


            if (!skipChars)
            {
                dialogueText.text += c;
            }
            else
            {
                tagText += c;
            }


            if (c == '>')
            {
                skipChars = false;
                if (tagText.Contains("delay="))
                {
                    if (tagText.Contains(","))
                    {
                        oneDelay = true;
                    }
                    if (tagText.Contains("default"))
                    {
                        dialogueSpeed = delay;
                    }
                    else
                    {
                        dialogueSpeed = float.Parse(tagText.Substring(tagText.IndexOf('=') + 1, tagText.IndexOf('>') - tagText.IndexOf('=') - 1));
                    }
                }
                else
                {
                    dialogueText.text += tagText;
                }
                tagText = "";

            }

            if (skipChars)
            {
                continue;
            }
            yield return new WaitForSeconds(dialogueSpeed);
        }
        isTyping = false;
    }
    public bool isTyping;
    public bool SkipTyping()
    {
        if (typingCoroutine != null)
        {
            skipTyping = true;
            isTyping = false;
            return true;
        }
        return false;
    }





}
