using Unity.Entities;
using UnityEngine;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Burst;

public struct InitializedCharactorFlag : IComponentData, IEnableableComponent { }

public struct CharactorMoveSpeed : IComponentData
{
    public float value;
}

public struct CharactorDirectionXZ : IComponentData
{
    public float2 value;
}

public class CharactorAuthoring : MonoBehaviour
{
    public float moveSpeed;
    public float2 directionXZ;

    class CharactorAuthoringBaker : Baker<CharactorAuthoring>
    {
        public override void Bake(CharactorAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent<InitializedCharactorFlag>(entity);
            AddComponent(entity, new CharactorMoveSpeed
            {
                value = authoring.moveSpeed
            });
            AddComponent(entity, new CharactorDirectionXZ
            {
                value = authoring.directionXZ
            });
        }
    }
}

[UpdateInGroup(typeof(InitializationSystemGroup))]
public partial struct CharactorInitializeSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        foreach (var (initializedFlag, mass) in SystemAPI.Query<EnabledRefRW<InitializedCharactorFlag>, RefRW<PhysicsMass>>())
        {
            mass.ValueRW.InverseInertia = float3.zero;
            initializedFlag.ValueRW = false;
        }
    }
}

public partial struct CharactorMoveSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var deltaTime = SystemAPI.Time.DeltaTime;
        foreach (var (moveSpeed, direction, velocity) in SystemAPI.Query<RefRO<CharactorMoveSpeed>, RefRW<CharactorDirectionXZ>, RefRW<PhysicsVelocity>>())
        {
            var originalVelocityY = velocity.ValueRO.Linear.y;
            var VelocityXZ = direction.ValueRO.value * moveSpeed.ValueRO.value;
            velocity.ValueRW.Linear = new float3(VelocityXZ.x, originalVelocityY, VelocityXZ.y);
        }
    }
}
