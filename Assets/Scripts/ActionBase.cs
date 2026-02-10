using UnityEngine;

public interface IUsable
{
    public void Use(Unit user, Unit target);
}

public interface IBattleAction : IUsable
{
    public int ExecuteDelay { get; }
}