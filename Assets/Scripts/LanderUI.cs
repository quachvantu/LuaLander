using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LanderUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI stats;
    [SerializeField] private TextMeshProUGUI buttonTextMesh;
    [SerializeField] private Button buttonContinueRestart;
    [SerializeField] private Button buttonMainMenu;
    [SerializeField] private Button musicButton;
    [SerializeField] private TextMeshProUGUI musicButtonText;
    [SerializeField] private Button soundButton;
    [SerializeField] private TextMeshProUGUI soundButtonText;
    public Action buttonContinueRestartAction;
    private void Start()
    {

        Lander.Instance.OnLanded += Lander_OnLanded;
        buttonContinueRestart.onClick.AddListener(() =>
        {
            buttonContinueRestartAction();
        });
        buttonMainMenu.onClick.AddListener(() =>
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
        Hide();
    }

    private void Lander_OnLanded(object sender, Lander.OnLandedEventArgs e)
    {
        switch (e.landingResult)
        {
            case Lander.LandingResult.Success:
                title.text = "SUCCESSFUL LANDING";
                stats.text = Mathf.RoundToInt(e.landingSpeed) + "\n" +
                Math.Round(e.dotVector) + "\n" + e.scoreMultiplier + "\n" + e.score;
                buttonTextMesh.text = "CONTINUE";
                buttonContinueRestartAction = GameManager.Instance.GoToNextLevel;
                Show();
                break;
            case Lander.LandingResult.WrongAreaLanding:
            case Lander.LandingResult.TooSteepAngle:
            case Lander.LandingResult.TooFastLanding:
                title.text = "CRASH";
                stats.text = Mathf.RoundToInt(e.landingSpeed) + "\n" +
                Math.Round(e.dotVector) + "\n" + e.scoreMultiplier + "\n" + e.score;
                buttonTextMesh.text = "RESTART";
                buttonContinueRestartAction = GameManager.Instance.RetryLevel;
                Show();
                break;
        }
    }
    private void Show()
    {
        gameObject.SetActive(true);
        buttonContinueRestart.Select();
        musicButtonText.text = "MUSIC: " + MusicManager.Instance.GetMusicVolume();
        soundButtonText.text = "SOUND: " + SoundManager.Instance.GetSoundVolume();
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
