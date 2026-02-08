using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Town : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        MusicManager.Instance.PlayMusic("Village");
    }

    // Update is called once per frame
    void Update()
    {
        if(((bool)DialogueManager.Instance.story.variablesState["inBattle"]) == true)
        {
            SceneManager.LoadScene("battleTutorial");
            MusicManager.Instance.Stop();
        }
    }
}
