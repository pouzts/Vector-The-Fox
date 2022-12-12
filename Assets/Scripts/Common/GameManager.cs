using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : Singleton<GameManager>
{
    public enum eGameState
    { 
        GameStart,
        LevelStart,
        Game,
        LevelEnd,
        PlayerDead,
        GameOver,
        Win
    }

    [SerializeField] GameObject playerSpawn;
    [SerializeField] GameObject playerPrefab;

    [SerializeField] GameObject gameOverPanel;
    [SerializeField] TMP_Text livesText;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text timeText;

    [SerializeField] AudioSource backgroundMus;

    [SerializeField] float gameTime = 100;
    [SerializeField] int startingLives = 3;

    private int lives = 0;
    public int Lives 
    { 
        get => lives;
        set 
        {
            lives = value;
            livesText.text = "LIVES: " + lives.ToString("00");
        } 
    }

    private int score = 0;
    public int Score
    {
        get => score;
        set
        {
            score = value;
            scoreText.text = "SCORE: " + score.ToString("000000");
        }
    }

    float timer = 0;
    public float Timer {
        get => timer;
        set
        {
            if (value <= 0)
                timer = 0;
            else
                timer = value;

            timeText.text = "TIME: " + timer.ToString("000");
        }
    }

    private eGameState gameState;
    public eGameState GameState
    {
        get => gameState;
        set
        {
            gameState = value;
            switch (gameState)
            {
                case eGameState.GameStart:
                    stateEvent = GameStart;
                    break;
                case eGameState.LevelStart:
                    stateTimer = 2;
                    stateEvent = LevelStart;
                    break;
                case eGameState.Game:
                    Timer = gameTime;
                    stateEvent = Game;
                    break;
                case eGameState.LevelEnd:
                    stateEvent = LevelEnd;
                    break;
                case eGameState.PlayerDead:
                    stateTimer = 2;
                    stateEvent = PlayerDead;
                    break;
                case eGameState.GameOver:
                    stateTimer = 5;
                    stateEvent = GameOver;
                    break;
                case eGameState.Win:
                    stateEvent = Win;
                    break;
            }
        }
    }

    public float stateTimer { get; set; } = 0;

    public bool gameEnded { get; set; } = false;

    private delegate void GameEvent();
    private event GameEvent stateEvent;

    void Start()
    {
        gameOverPanel.SetActive(false);
        GameState = eGameState.LevelStart;
        Lives = startingLives;
    }

    void Update()
    {
        if (!gameEnded)
        { 
            Timer -= Time.deltaTime;
        }

        stateTimer -= Time.deltaTime;
        stateEvent?.Invoke();
    }

    private void GameStart()
    {
        gameEnded = false;
        GameState = eGameState.LevelStart;
    }

    private void LevelStart()
    {
        if (stateTimer <= 0)
        {
            Instantiate(playerPrefab, playerSpawn.transform);
            GameState = eGameState.Game;
            backgroundMus.Play();
        }
    }

    private void Game()
    {
        if (Timer <= 0)
        {
            var players = FindObjectsOfType<Player>();

            foreach (var player in players)
            {
                player.DestroySelf();
            }
        }
    }

    private void LevelEnd()
    {
        gameState = eGameState.Win;
    }

    private void PlayerDead()
    {
        if (stateTimer <= 0)
        {
            backgroundMus.Stop();
            Lives -= 1;
            if (Lives <= 0)
                GameState = eGameState.GameOver;
            else
                GameState = eGameState.LevelStart;
        }
    }

    private void GameOver()
    {
        gameOverPanel.SetActive(true);
        gameEnded = true;

        if (stateTimer <= 0)
            SceneManager.LoadSceneAsync("Title");
    }

    private void Win()
    {
        gameEnded = true;
        print("You win");

        backgroundMus.Stop();

        if (SceneManager.GetActiveScene().name != "Win")
            SceneManager.LoadSceneAsync("Win");
    }
}
