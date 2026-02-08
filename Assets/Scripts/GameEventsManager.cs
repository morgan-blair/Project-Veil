using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventsManager : MonoBehaviour
{
    public static GameEventsManager instance { get; private set; }

    public QuestEvents questEvents { get; private set; }
    public DialogueEvents dialogueEvents { get; private set; }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Debug.LogError("Found more than one Game Events Manager in the scene.");
            Destroy(gameObject);
            return;
        }

        instance = this;

        // create event systems immediately
        questEvents = new QuestEvents();
        dialogueEvents = new DialogueEvents();
    }
}

