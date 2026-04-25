using System;
using UnityEngine;

public class LanderAudio : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    private void Start()
    {
        Debug.Log(Lander.Instance);
        Lander.Instance.OnLeftForce += Lander_OnLeftForce;
        Lander.Instance.OnRightForce += Lander_OnRightForce;
        Lander.Instance.OnUpForce += Lander_OnUpForce;
        Lander.Instance.OnBeforeForce += Lander_OnBeforeForce;
        SoundManager.Instance.OnSoundVolumeChanged += SoundManager_OnSoundVolumeChanged;
        audioSource.Pause();
    }

    private void SoundManager_OnSoundVolumeChanged(object sender, EventArgs e)
    {
        audioSource.volume = SoundManager.Instance.GetSoundVolumeNormalized();
    }

    private void Lander_OnLeftForce(object sender, EventArgs e)
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    private void Lander_OnRightForce(object sender, EventArgs e)
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    private void Lander_OnUpForce(object sender, EventArgs e)
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
    private void Lander_OnBeforeForce(object sender, EventArgs e)
    {
        audioSource.Pause();
    }
}
