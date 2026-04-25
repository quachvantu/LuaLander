using TMPro;
using UnityEngine;

public class LandingPad : MonoBehaviour
{
    [SerializeField] private int scoreMultiplier;
    [SerializeField] private TextMeshPro scoreMultiplierTextMeshPro;
    private void Awake()
    {
        scoreMultiplierTextMeshPro.text = "x" + scoreMultiplier;
    }
    public int GetScoreMultiplier()
    {
        return scoreMultiplier;
    }
}
