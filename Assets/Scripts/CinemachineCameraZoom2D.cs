using Unity.Cinemachine;
using UnityEngine;

public class CinemachineCameraZoom2D : MonoBehaviour
{
    public static CinemachineCameraZoom2D Instance { get; private set; }
    [SerializeField] private CinemachineCamera cinemachineCamera;
    private const float CAMERA_ZOOM_NORMAL = 10f;
    private float size = 10f;
    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        float zoomSpeed = 2f;
        cinemachineCamera.Lens.OrthographicSize = Mathf.Lerp(cinemachineCamera.Lens.OrthographicSize, size, zoomSpeed * Time.deltaTime);
    }

    public void SetSize(float size)
    {
        this.size = size;
    }

    public void SetCameraZoom()
    {
        SetSize(CAMERA_ZOOM_NORMAL);
    }
}
