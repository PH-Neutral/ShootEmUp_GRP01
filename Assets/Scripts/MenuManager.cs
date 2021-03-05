using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {
    public void Goto_GameScene()
    {
        SceneManager.LoadScene(1);
    }

    public void Goto_MainMenuScene()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitApp() {
        Application.Quit(0);
    }
}
