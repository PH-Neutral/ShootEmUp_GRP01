using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public static GameManager Instance;

    public bool IsPaused
    {
        get { return Time.timeScale <= 0f; }
        set { Time.timeScale = gamePlaying ? (value ? 0f : 1f) : Time.timeScale; }
    }
    [HideInInspector] public GameObject poolMissile;
    public int life = 0;
    public int score = 0;

    [SerializeField] int lifeUpScore;
    [SerializeField] int bossSpawnPalier;
    [SerializeField] EnemySpawner spawner;
    int lifeUpPalier;
    bool gamePlaying = false;

    private void Start() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }

        poolMissile = new GameObject("MissilePool");
        poolMissile.transform.SetParent(transform);

        lifeUpPalier = lifeUpScore;

        AddLife(0);
        AddScore(0);
        
        gamePlaying = true;
        IsPaused = false;
    }

    void Update()
    {
        if(life <= 0) {
            Invoke(nameof(LoseGame), 1f);
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
        if (score >= bossSpawnPalier && !spawner.spawnBossNext) {
            spawner.spawnBossNext = true;
        }
    }

    void TogglePanelPause(bool pause)
    {
        MenuManager.Instance.ShowPanelPause(pause);
    }

    public void LoseGame() {
        MenuManager.Instance.ShowPanelGameOver(true);
        IsPaused = true;
        gamePlaying = false;
    }

    public void WinGame() {
        MenuManager.Instance.ShowPanelVictory(true);
        IsPaused = true;
        gamePlaying = false;
    }

    public void AddLife(int increment) {
        life += increment;
        if (life < 0) { life = 0; }
        if(life > 99) {
            MenuManager.Instance.ChangeLifeText("Lives : 99+");
        } else {
            MenuManager.Instance.ChangeLifeText("Lives : " + life);
        }
    }

    public void AddScore(int increment) {
        score += increment;
        if (score < 0) { score = 0; }
        MenuManager.Instance.ChangeScoreText("Score: " + score);
        if (score >= lifeUpPalier)
        {
            AddLife(15);
            lifeUpPalier += lifeUpScore;
        }
    }

    public static Vector3 GetScreenBounds() {
        return Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }
}
