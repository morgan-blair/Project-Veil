using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MusicManager.Instance.PlayMusic("Main Menu");
    }

    public void Play()
    {
        MusicManager.Instance.Stop();
        SceneManager.LoadScene("menuTutorial");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
