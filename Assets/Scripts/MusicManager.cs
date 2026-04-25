using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }
    [SerializeField] private AudioSource audioSource;
    private static float musicTime = 0;
    private static int musicVolume = 4;
    private const int MUSIC_VOLUME_MAX = 10;
    private void Update()
    {
        musicTime = audioSource.time;
    }
    private void Awake()
    {
        Instance = this;
        audioSource.time = musicTime;
        UpdateMusicVolume();
    }
    private float GetMusicVolumeNormalized()
    {
        return (float)musicVolume / (float)MUSIC_VOLUME_MAX;
    }
    public void ChangeMusicVolume()
    {
        musicVolume = (musicVolume + 1) % MUSIC_VOLUME_MAX;
    }
    public int GetMusicVolume()
    {
        return musicVolume;
    }
    public void UpdateMusicVolume()
    {
        audioSource.volume = GetMusicVolumeNormalized();
    }
}
