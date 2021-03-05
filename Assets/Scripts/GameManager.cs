using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public static GameManager Instance;


    public bool IsPaused
    {
        get { return Time.timeScale <= 0f; }
        set { Time.timeScale = value ? 0f : 1f; }
    }
    [HideInInspector] public GameObject poolMissile;
    public int life = 0;

    [SerializeField] GameObject panelPause;
    [SerializeField] GameObject panelGameOver;
    [SerializeField] Text txtLife;

    private void Start() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }

        poolMissile = new GameObject("MissilePool");
        poolMissile.transform.SetParent(transform);

        life = 0;
        AddLife(1);

        IsPaused = false;
    }

    void Update()
    {
        if(life <= 0) {
            IsPaused = true;
            panelGameOver.SetActive(true);
        } else {
            if(Input.GetKeyDown(KeyCode.Escape)) {
                IsPaused = !IsPaused;
                TogglePanelPause(IsPaused);
            }
            if(Input.GetKeyDown(KeyCode.M)) {
                AddLife(10);
            }

            if(Input.GetKeyDown(KeyCode.L)) {
                AddLife(-10);
            }
        }
    }

    void TogglePanelPause(bool pause)
    {
        panelPause.SetActive(pause);
    }

    void AddLife(int increment) {
        life += increment;
        if(life > 99) {
            txtLife.text = "Score : 99+";
        } else {
            txtLife.text = "Score : " + life;
        }
    }

    public static Vector3 GetScreenBounds() {
        return Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }
}
