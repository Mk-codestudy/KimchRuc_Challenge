using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum Gamestate
{
    Intro,
    Playing,
    Dead
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Gamestate state = Gamestate.Intro;

    public int lives = 3;

    public float gamestartTime;

    [Header("게임오브제들")]
    public GameObject introUI;
    public GameObject deadUI;

    public GameObject EnemySpawner;
    public GameObject FoodSpawner;
    public GameObject GoldenSpawner;
    public PlayerJump playersc;

    public TMP_Text scoreText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        introUI.SetActive(true);
        deadUI.SetActive(false);
        EnemySpawner.SetActive(false);
        FoodSpawner.SetActive(false);
        GoldenSpawner.SetActive(false);
    }

    float CalculateScore()
    {
        return Time.time - gamestartTime;
    }

    int GetHighScore()
    {
        return PlayerPrefs.GetInt("highScore");
    }

    void SaveHighScore()
    {
        int score = Mathf.FloorToInt(CalculateScore());
        int currentHighScore = PlayerPrefs.GetInt("highScore");
        if (score > currentHighScore)
        {
            PlayerPrefs.SetInt("highScore", score);
            PlayerPrefs.Save();
        }
    }

    public float CalculateSpeed(float current)
    {
        if (state != Gamestate.Playing)
        {
            return current;
        }
        else
        {
            float speed = current + (0.5f * Mathf.Floor(CalculateScore() / 10f));
            return Mathf.Min(speed, 30f);
        }
    }


    void Update()
    {
        if (state == Gamestate.Playing)
        {
            scoreText.text = "Score " + Mathf.FloorToInt(CalculateScore());
        }
        else if (state == Gamestate.Dead)
        {
            scoreText.text = "HighScore " + GetHighScore();
        }

        if (state == Gamestate.Intro && Input.GetKeyDown(KeyCode.Space))
        {
            state = Gamestate.Playing;
            introUI.SetActive(false);
            EnemySpawner.SetActive(true);
            FoodSpawner.SetActive(true);
            GoldenSpawner.SetActive(true);
            gamestartTime = Time.time;
        }
        else if (state == Gamestate.Playing && lives == 0)
        {
            playersc.KillPlayer();
            EnemySpawner.SetActive(false);
            FoodSpawner.SetActive(false);
            GoldenSpawner.SetActive(false);
            state = Gamestate.Dead;
            deadUI.SetActive(true);
            SaveHighScore();
        }
        else if (state == Gamestate.Dead && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("Main");
        }
    }

}
