using UnityEngine;

public abstract class ComponentSystem : MonoBehaviour
{
    public abstract void OnEnable();
    public abstract void OnDisable();
}
