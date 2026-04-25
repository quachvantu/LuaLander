using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatsUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI statsTextMeshProGUI;
    [SerializeField] private GameObject leftVelocityArrow;
    [SerializeField] private GameObject rightVelocityArrow;
    [SerializeField] private GameObject upVelocityArrow;
    [SerializeField] private GameObject downVelocityArrow;
    [SerializeField] private Image fuelBarImage;
    private void Update()
    {
        UpdateStatsUI();
    }

    private void UpdateStatsUI()
    {
        leftVelocityArrow.SetActive(Lander.Instance.GetSpeedX() < 0);
        rightVelocityArrow.SetActive(Lander.Instance.GetSpeedX() >= 0);
        upVelocityArrow.SetActive(Lander.Instance.GetSpeedY() >= 0);
        downVelocityArrow.SetActive(Lander.Instance.GetSpeedY() < 0);
        fuelBarImage.fillAmount = Lander.Instance.GetFuelNormalized();
        statsTextMeshProGUI.text =
        GameManager.Instance.GetLevelNumber() + "\n" +
        GameManager.Instance.GetScore() + "\n" +
        Mathf.RoundToInt(GameManager.Instance.GetTime()) + "\n" +
        Mathf.Abs(Mathf.RoundToInt(Lander.Instance.GetSpeedX())) + "\n" +
        Mathf.Abs(Mathf.RoundToInt(Lander.Instance.GetSpeedY()));

    }

}
