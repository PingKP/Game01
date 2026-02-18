using UnityEngine;
using Unity.Entities;

public struct Health : IComponentData
{
    public float maxValue;
    public float currentValue;
}

public class HealthAuthoring : MonoBehaviour
{
    public float maxValue;

    class HealthAuthoringBaker : Baker<HealthAuthoring>
    {
        public override void Bake(HealthAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new Health
            {
                maxValue = authoring.maxValue,
                currentValue = authoring.maxValue
            });
        }
    }
}

public partial struct HealthSystem : ISystem
{
    public void OnUpdate(ref SystemState state)
    {
        var deltaTime = SystemAPI.Time.DeltaTime;
        
    }
}
