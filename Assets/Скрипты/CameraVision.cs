using UnityEngine;

public class CameraVision : MonoBehaviour
{
    void OnBecameVisible()
    {
        enabled = true;
        Debug.Log("видно");
    }
    void OnBecameInvisible()
    {
        enabled = false;
        Debug.Log("не видно");
    }
}
