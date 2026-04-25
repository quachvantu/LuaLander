using System;
using UnityEngine;

public class LanderVisuals : MonoBehaviour
{
    [SerializeField] private ParticleSystem leftThrusterParticleSystem;
    [SerializeField] private ParticleSystem midThrusterParticleSystem;
    [SerializeField] private ParticleSystem rightThrusterParticleSystem;
    [SerializeField] private GameObject landerExplosionVFX;

    private void Awake()
    {
        Lander.Instance.OnUpForce += Lander_OnUpForce;
        Lander.Instance.OnLeftForce += Lander_OnLeftForce;
        Lander.Instance.OnRightForce += Lander_OnRightForce;
        Lander.Instance.OnBeforeForce += Lander_OnBeforeForce;
        Lander.Instance.OnLanded += Lander_OnLanded;

    }

    private void Lander_OnLanded(object sender, Lander.OnLandedEventArgs e)
    {
        switch (e.landingResult)
        {
            case Lander.LandingResult.TooFastLanding:
            case Lander.LandingResult.TooSteepAngle:
            case Lander.LandingResult.WrongAreaLanding:
                Instantiate(landerExplosionVFX, transform.position, Quaternion.identity);
                gameObject.SetActive(false);
                break;
        }
    }

    private void Lander_OnBeforeForce(object sender, EventArgs e)
    {
        thrusterParticleSystem(leftThrusterParticleSystem, false);
        thrusterParticleSystem(midThrusterParticleSystem, false);
        thrusterParticleSystem(rightThrusterParticleSystem, false);
    }

    private void Lander_OnLeftForce(object sender, EventArgs e)
    {
        thrusterParticleSystem(leftThrusterParticleSystem, true);
        thrusterParticleSystem(midThrusterParticleSystem, false);
        thrusterParticleSystem(rightThrusterParticleSystem, false);

    }

    private void Lander_OnRightForce(object sender, EventArgs e)
    {
        thrusterParticleSystem(leftThrusterParticleSystem, false);
        thrusterParticleSystem(midThrusterParticleSystem, false);
        thrusterParticleSystem(rightThrusterParticleSystem, true);
    }

    private void Lander_OnUpForce(object sender, EventArgs e)
    {
        thrusterParticleSystem(leftThrusterParticleSystem, true);
        thrusterParticleSystem(midThrusterParticleSystem, true);
        thrusterParticleSystem(rightThrusterParticleSystem, true);
    }

    private void thrusterParticleSystem(ParticleSystem particleSystem, bool enabled)
    {
        if (enabled)
        {
            particleSystem.Play();
        }
        else
        {
            particleSystem.Stop();
        }
    }
}
