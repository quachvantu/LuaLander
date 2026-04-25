using UnityEngine;

public class Fuel : MonoBehaviour
{
    public void OnDestroy()
    {
        Destroy(gameObject);
    }
}
