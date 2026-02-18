using UnityEngine;

[CreateAssetMenu(fileName = "UnitBase", menuName = "Scriptable Objects/UnitBase")]
public class UnitBase : ScriptableObject
{
    [SerializeField] private string name;
    [SerializeField] private Sprite sprite;
    [SerializeField] private int speed;

    public string Name => name;
    public Sprite Sprite => sprite;
    public int Speed => speed;
}
