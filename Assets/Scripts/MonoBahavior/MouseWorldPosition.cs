using UnityEngine;

public class MouseWorldPosition : MonoBehaviour
{
    public static MouseWorldPosition instance { get; private set; }

    private void Awake()
    {
        instance = this;
    }

    public Vector3 GetWorldPosition ()
    {
        Ray mouseCameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        Plane plane = new Plane(Vector3.up, Vector3.zero);

        if (plane.Raycast(mouseCameraRay, out float rayLength))
        {
            Vector3 worldPosition = mouseCameraRay.GetPoint(rayLength);
            return worldPosition;
        }
        else
        {
            return Vector3.zero;
        }
    }
}
