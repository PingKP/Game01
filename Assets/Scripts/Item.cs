using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
public class Item : ScriptableObject, IBattleAction
{
    [SerializeField] protected string name;
    [SerializeField] protected string description;
    [SerializeField] protected int executeDelay;

    protected ActionType actionType = ActionType.item;

    public void Use(Unit user, Unit target)
    {
        // Implement the logic for using the skill on the target
        // This could involve calculating damage, applying effects, etc.
        Debug.Log($"{user.Name} uses {this.name} on {target.Name}!");
    }

    public int ExecuteDelay => this.ExecuteDelay;
}
