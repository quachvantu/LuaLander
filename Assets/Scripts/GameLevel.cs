using UnityEngine;

public class GameLevel : MonoBehaviour
{
    [SerializeField] private int levelNumber;
    [SerializeField] private Transform landerStartPosition;
    [SerializeField] private Transform cameraStartPosition;
    [SerializeField] private float cameraStartZoom;

    public int GetLevelNumber()
    {
        return levelNumber;
    }

    public Vector3 GetLanderStartPosition()
    {
        return landerStartPosition.position;
    }

    public Transform GetCameraStartPosition()
    {
        return cameraStartPosition;
    }

    public float GetCameraStartZoom()
    {
        return cameraStartZoom;
    }
}
