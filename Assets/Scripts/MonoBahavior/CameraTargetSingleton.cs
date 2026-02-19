using UnityEngine;

public class CameraTargetSingleton : MonoBehaviour
{
    public static CameraTargetSingleton instance;

    public void Awake ()
    {
        instance = this;
    }
}
