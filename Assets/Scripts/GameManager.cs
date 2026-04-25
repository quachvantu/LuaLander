using UnityEngine;
using System.Collections.Generic;
using Unity.Cinemachine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private static int levelNumber = 1;
    public event EventHandler OnPausedGame;
    public event EventHandler OnUnPausedGame;
    private int score = 0;
    private static int scoreTotal = 0;
    private float time = 0f;
    private bool isTimeActive;
    [SerializeField] private List<GameLevel> gameLevelList;
    [SerializeField] private CinemachineCamera cinemachineCamera;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        Lander.Instance.OnStateChanged += Lander_OnStateChanged;
        Lander.Instance.OnCoinPickup += Lander_OnCoinPickup;
        Lander.Instance.OnLanded += Lander_OnLanded;
        GameInput.Instance.OnMenuButtonPressed += GameInput_OnMenuButtonPressed;
        LoadCurrentLevel();
    }
    private void Update()
    {
        if (isTimeActive)
        {
            time += Time.deltaTime;
        }
    }
    private void GameInput_OnMenuButtonPressed(object sender, EventArgs e)
    {
        Pause_UnPauseGame();
    }
    private void Lander_OnLanded(object sender, Lander.OnLandedEventArgs e)
    {
        AddScore(e.score);
        scoreTotal += score;

    }

    private void Lander_OnStateChanged(object sender, Lander.OnStateChangedEventArgs e)
    {
        if (isTimeActive = e.state == Lander.State.Normal)
        {
            cinemachineCamera.Target.TrackingTarget = Lander.Instance.transform;
            CinemachineCameraZoom2D.Instance.SetCameraZoom();
        }
    }
    private void Lander_OnCoinPickup(object sender, EventArgs e)
    {
        AddScore(40);
    }
    private GameLevel GetGameLevel()
    {
        foreach (GameLevel gameLevel in gameLevelList)
        {
            if (gameLevel.GetLevelNumber() == levelNumber)
            {
                return gameLevel;
            }
        }
        return null;
    }
    private void LoadCurrentLevel()
    {
        GameLevel gameLevel = GetGameLevel();
        Instantiate(gameLevel, Vector3.zero, Quaternion.identity);
        Lander.Instance.transform.position = gameLevel.GetLanderStartPosition();
        cinemachineCamera.Target.TrackingTarget = gameLevel.GetCameraStartPosition();
        CinemachineCameraZoom2D.Instance.SetSize(gameLevel.GetCameraStartZoom());
    }
    private void AddScore(int amount)
    {
        score += amount;
    }
    public void Pause_UnPauseGame()
    {
        if (Time.timeScale == 1f)
        {
            PausedGame();
        }
        else
        {
            UnPausedGame();
        }
    }
    public void PausedGame()
    {
        Time.timeScale = 0f;
        OnPausedGame?.Invoke(this, EventArgs.Empty);
    }
    public void UnPausedGame()
    {
        Time.timeScale = 1f;
        OnUnPausedGame?.Invoke(this, EventArgs.Empty);
    }
    public void GoToNextLevel()
    {
        levelNumber++;
        if (GetGameLevel() == null)
        {
            SceneLoad.LoadScene(SceneLoad.Scene.GameOver);
        }
        else
        {
            SceneLoad.LoadScene(SceneLoad.Scene.Game);
        }
    }
    public void RetryLevel()
    {
        SceneLoad.LoadScene(SceneLoad.Scene.Game);
    }
    public static void ResetStatic()
    {
        levelNumber = 1;
        scoreTotal = 0;
    }
    public int GetLevelNumber()
    {
        return levelNumber;
    }
    public int GetScoreTotal()
    {
        return scoreTotal;
    }

    public int GetScore()
    {
        return score;
    }
    public float GetTime()
    {
        return time;
    }
}
