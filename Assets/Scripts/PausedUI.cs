using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PausedUI : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private TextMeshProUGUI musicButtonText;
    [SerializeField] private Button soundButton;
    [SerializeField] private TextMeshProUGUI soundButtonText;
    private void Awake()
    {
        Hide();
        GameManager.Instance.OnPausedGame += GameManager_OnPausedGame;
        GameManager.Instance.OnUnPausedGame += GameManager_OnUnPausedGame;
        resumeButton.onClick.AddListener(() =>
        {
            GameManager.Instance.UnPausedGame();
        });
        mainMenuButton.onClick.AddListener(() =>
        {
            SceneLoad.LoadScene(SceneLoad.Scene.Menu);
        });
        musicButton.onClick.AddListener(() =>
        {
            MusicManager.Instance.ChangeMusicVolume();
            MusicManager.Instance.UpdateMusicVolume();
            musicButtonText.text = "MUSIC: " + MusicManager.Instance.GetMusicVolume();
        });
        soundButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.ChangeSoundVolume();
            soundButtonText.text = "SOUND: " + SoundManager.Instance.GetSoundVolume();
        });

    }
    private void GameManager_OnUnPausedGame(object sender, EventArgs e)
    {
        Hide();
    }

    private void GameManager_OnPausedGame(object sender, EventArgs e)
    {
        Show();
    }

    private void Show()
    {
        gameObject.SetActive(true);
        resumeButton.Select();
        musicButtonText.text = "MUSIC: " + MusicManager.Instance.GetMusicVolume();
        soundButtonText.text = "SOUND: " + SoundManager.Instance.GetSoundVolume();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
