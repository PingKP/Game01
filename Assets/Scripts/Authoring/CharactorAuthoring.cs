using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

public struct InitializedCharactorFlag : IComponentData, IEnableableComponent { }


public struct CharactorMoveSpeed : IComponentData
{
    public float value;
}


public struct CharactorDirectionXZ : IComponentData
{
    public float2 value;
}


public struct Selected : IComponentData, IEnableableComponent { }


public struct Controllable : IComponentData, IEnableableComponent { }


public class CharactorAuthoring : MonoBehaviour
{
    public float moveSpeed;
    public float2 directionXZ;

    public class Baker : Baker<CharactorAuthoring>
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
            AddComponent<Selected>(entity);
            AddComponent<Controllable>(entity);
        }
    }
}



