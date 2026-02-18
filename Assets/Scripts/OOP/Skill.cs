using UnityEngine;

public class Skill : IBattleAction
{
    private SkillBase skillBase;

    private int masteryLevel;
    private int masteryLevelExp;

    public Skill(SkillBase skillBase)
    {
        this.skillBase = skillBase;
    }

    public void Use(Unit user, Unit target)
    {
        // Implement the logic for using the skill on the target
        // This could involve calculating damage, applying effects, etc.
        Debug.Log($"{user.Name} uses {skillBase.Name} on {target.Name}!");
    }

    public int ExecuteDelay => skillBase.ExecuteDelay;
}
