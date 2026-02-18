using UnityEngine;

public class Unit
{
    private string customizedName;
    public UnitBase unitBase;

    public int level;
    public int totalExp;

    public string Name => customizedName ?? unitBase.Name;

    public Unit(UnitBase unitBase)
    {
        this.unitBase = unitBase;
    }
}
