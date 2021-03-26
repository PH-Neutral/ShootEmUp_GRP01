using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {
    public static MenuManager Instance = null;

    [SerializeField] GameObject panelPause;
    [SerializeField] GameObject panelGameOver;
    [SerializeField] GameObject panelVictory;
    [SerializeField] Text txtLife;
    [SerializeField] Text txtScore;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else if (Instance != this) {
            Destroy(gameObject);
        }
    }

    public void ShowPanelPause(bool show) {
        panelPause.SetActive(show);
    }

    public void ShowPanelGameOver(bool show) {
        panelGameOver.SetActive(show);
    }

    public void ShowPanelVictory(bool show) {
        panelVictory.SetActive(show);
    }

    public void ChangeLifeText(string newText) {
        txtLife.text = newText;
    }

    public void ChangeScoreText(string newText) {
        txtScore.text = newText;
    }

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
