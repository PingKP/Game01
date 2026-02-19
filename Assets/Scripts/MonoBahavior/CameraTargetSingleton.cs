using UnityEngine;

public class CameraTargetSingleton : MonoBehaviour
{
    public static CameraTargetSingleton instance;

    public void Awake ()
    {
        if (instance != null)
        {
            Debug.LogWarning("CameraTargetSingleton already exists. Destroying the new one.", instance);
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
}
