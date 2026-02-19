using Unity.Entities;
using UnityEngine;
using Unity.Mathematics;
using Unity.Burst;


public struct PlayerTag : IComponentData { }


public class PlayerAuthoring : MonoBehaviour
{
    class PlayerAuthoringBaker : Baker<PlayerAuthoring>
    {
        public override void Bake(PlayerAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent<PlayerTag>(entity);
            AddComponent<CameraTarget>(entity);
            AddComponent<InitializedCameraTargetTag>(entity);
        }
    }
}


public partial class PlayerInputSystem : SystemBase
{

    private PlayerMovementInput input;

    protected override void OnCreate()
    {
        input = new PlayerMovementInput();
        input.Enable();
    }

    [BurstCompile]
    protected override void OnUpdate()
    {
        var currentInput = (float2)input.Player.Move.ReadValue<Vector2>();
        foreach (var direction 
            in SystemAPI.Query<
                RefRW<CharactorDirectionXZ>>().WithAll<PlayerTag>())
        {
            direction.ValueRW.value = currentInput;
        }
    }
}