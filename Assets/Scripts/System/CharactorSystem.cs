using UnityEngine;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;


[UpdateInGroup(typeof(InitializationSystemGroup))]
public partial struct CharactorInitializeSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        foreach (var (initializedFlag, mass)
                        in SystemAPI.Query<
                            EnabledRefRW<InitializedCharactorFlag>,
                            RefRW<PhysicsMass>>())
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
        foreach (var (moveSpeed, direction, velocity)
                        in SystemAPI.Query<
                            RefRO<CharactorMoveSpeed>,
                            RefRW<CharactorDirectionXZ>,
                            RefRW<PhysicsVelocity>>())
        {
            var originalVelocityY = velocity.ValueRO.Linear.y;
            var VelocityXZ = direction.ValueRO.value * moveSpeed.ValueRO.value;
            velocity.ValueRW.Linear = new float3(VelocityXZ.x, originalVelocityY, VelocityXZ.y);
        }
    }
}
