using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private Button playGame;
    [SerializeField] private Button quitGame;
    [SerializeField] private Button musicButton;
    [SerializeField] private TextMeshProUGUI musicButtonText;
    [SerializeField] private Button soundButton;
    [SerializeField] private TextMeshProUGUI soundButtonText;

    private void Awake()
    {
        playGame.onClick.AddListener(() =>
        {
            SceneLoad.LoadScene(SceneLoad.Scene.Game);
            GameManager.ResetStatic();
        });
        quitGame.onClick.AddListener(() =>
        {
            Application.Quit();
        });
        musicButton.onClick.AddListener(() =>
        {
            MusicManager.Instance.ChangeMusicVolume();
            musicButtonText.text = "MUSIC: " + MusicManager.Instance.GetMusicVolume();
            MusicManager.Instance.UpdateMusicVolume();
        });
        soundButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.ChangeSoundVolume();
            soundButtonText.text = "SOUND: " + SoundManager.Instance.GetSoundVolume();
        });
    }
    private void Start()
    {
        musicButtonText.text = "MUSIC: " + MusicManager.Instance.GetMusicVolume();
        soundButtonText.text = "SOUND: " + SoundManager.Instance.GetSoundVolume();
        playGame.Select();

    }

}
