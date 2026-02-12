using UnityEngine;

public class DamageEventComponent : Component
{
    public int damageAmount;
    public uint sourceEntityId;
    public uint targetEntityId;
}

public class DamageEventManager : ComponentManager<DamageEventComponent>
{
    
}