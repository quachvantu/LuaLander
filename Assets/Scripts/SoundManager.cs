using System;
using TMPro;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip coinPickupSound;
    [SerializeField] private AudioClip fuelPickupSound;
    [SerializeField] private AudioClip landingSuccessSound;
    [SerializeField] private AudioClip crashSound;
    public static SoundManager Instance { get; private set; }
    public event EventHandler OnSoundVolumeChanged;
    private static int soundVolume = 6;
    private const int SOUND_VOLUME_MAX = 10;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void Start()
    {
    }
    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Game")
        {
            Lander.Instance.OnCoinPickup += Lander_OnCoinPickup;
            Lander.Instance.OnFuelPickup += Lander_OnFuelPickup;
            Lander.Instance.OnLanded += Lander_OnLanded;
        }
    }


    private void Lander_OnLanded(object sender, Lander.OnLandedEventArgs e)
    {
        if (e.landingResult == Lander.LandingResult.Success)
        {
            PlaySound(landingSuccessSound);
        }
        else
        {
            PlaySound(crashSound);
        }
    }
    private void Lander_OnFuelPickup(object sender, EventArgs e)
    {
        PlaySound(fuelPickupSound);
    }

    private void Lander_OnCoinPickup(object sender, EventArgs e)
    {
        PlaySound(coinPickupSound);
    }
    private void PlaySound(AudioClip audioClip)
    {
        AudioSource.PlayClipAtPoint(audioClip, Camera.main.transform.position, GetSoundVolumeNormalized());
    }
    public float GetSoundVolumeNormalized()
    {
        return (float)soundVolume / (float)SOUND_VOLUME_MAX;
    }
    public void ChangeSoundVolume()
    {
        soundVolume = (soundVolume + 1) % SOUND_VOLUME_MAX;
        OnSoundVolumeChanged?.Invoke(this, EventArgs.Empty);
    }
    public int GetSoundVolume()
    {
        return soundVolume;
    }
}
