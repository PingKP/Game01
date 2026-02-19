using UnityEngine;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;

/*===============================================================
 * Include charactor movement and initialization system
  ==============================================================*/

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


public partial struct UnitMoveSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var deltaTime = SystemAPI.Time.DeltaTime;
        foreach (var (unitMover, velocity)
                        in SystemAPI.Query<
                            RefRO<UnitMover>,
                            RefRW<PhysicsVelocity>>())
        {
            var originalVelocity = velocity.ValueRW.Linear;
            var moveDirection = unitMover.ValueRO.moveDirectionXZ;
            var moveVelocity = moveDirection * unitMover.ValueRO.moveSpeed;
            velocity.ValueRW.Linear = new float3(moveVelocity.x, originalVelocity.y, moveVelocity.y)  ;
        }
    }
}


public partial struct MoveTargetSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        foreach (var (moveTarget, localTransform, unitMover, entity)
                        in SystemAPI.Query<
                            RefRW<MoveTarget>,
                            RefRO<LocalTransform>,
                            RefRW<UnitMover>>()
                            .WithEntityAccess())
        {
            var targetPosition = moveTarget.ValueRO.targetPosition;
            if (targetPosition.x == 0f &&
                targetPosition.z == 0f)
            {
                continue;
            }
            var currentPosition = localTransform.ValueRO.Position;
            var distance = math.distance(targetPosition, currentPosition);
            if (distance < moveTarget.ValueRO.stopDistance)
            {
                Debug.Log("Arrived at target");
                moveTarget.ValueRW.targetPosition = float3.zero;
                unitMover.ValueRW.moveDirectionXZ = float2.zero;
                continue;
            }
            Debug.Log("On My Way");
            float3 moveDirection = targetPosition - currentPosition;
            unitMover.ValueRW.moveDirectionXZ = math.normalize(new float2(moveDirection.x, moveDirection.z));

        }
    }
}