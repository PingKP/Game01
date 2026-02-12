using UnityEngine;

[CreateAssetMenu(fileName = "Skill", menuName = "Scriptable Objects/Skill")]
public class SkillBase : ScriptableObject
{
    [SerializeField] public string name;
    [SerializeField] public string description;
    [SerializeField] public int power;
    [SerializeField] public int executeDelay;

    public ActionType actionType = ActionType.skill;

    public string Name => name;
    public int ExecuteDelay => executeDelay;
    public string Description => description;
    public int Power => power;
}
