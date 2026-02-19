using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

public struct InitializedCharactorFlag : IComponentData, IEnableableComponent { }


public struct UnitMover : IComponentData
{
    public float moveSpeed;
    public float2 moveDirectionXZ;
}


public struct MoveTarget : IComponentData 
{
    public float stopDistance;
    public float3 targetPosition;
}


public struct SelectedFlag : IComponentData, IEnableableComponent { }


public struct ControllableFlag : IComponentData, IEnableableComponent { }


public class CharactorAuthoring : MonoBehaviour
{
    public float moveSpeed;
    public float2 moveDirection;
    public float stopDistance;

    public class Baker : Baker<CharactorAuthoring>
    {
        public override void Bake(CharactorAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent<InitializedCharactorFlag>(entity);
            AddComponent(entity, new UnitMover
            {
                moveSpeed = authoring.moveSpeed,
                moveDirectionXZ = authoring.moveDirection,
            });
            AddComponent(entity, new MoveTarget
            {
                targetPosition = float3.zero,
                stopDistance = authoring.stopDistance,
            });
            AddComponent<SelectedFlag>(entity);
            SetComponentEnabled<SelectedFlag>(entity, false);
            AddComponent<ControllableFlag>(entity);
        }
    }
}



