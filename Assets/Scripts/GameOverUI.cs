using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private Button menuButton;
    [SerializeField] private TextMeshProUGUI finalScore;
    [SerializeField] private Button musicButton;
    [SerializeField] private TextMeshProUGUI musicButtonText;
    [SerializeField] private Button soundButton;
    [SerializeField] private TextMeshProUGUI soundButtonText;
    private void Awake()
    {
        menuButton.onClick.AddListener(() =>
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
        finalScore.text = "FINAL SCORE: " + GameManager.Instance.GetScoreTotal();
    }
    private void Start()
    {
        musicButtonText.text = "MUSIC: " + MusicManager.Instance.GetMusicVolume();
        soundButtonText.text = "SOUND: " + SoundManager.Instance.GetSoundVolume();
        menuButton.Select();

    }
}
