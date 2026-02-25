using UnityEngine;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

/*==============================================================
 * Camera System
  ==============================================================*/

public struct CameraTarget : IComponentData
{
    public UnityObjectRef<Transform> cameraTransform;
}


public struct InitializedCameraTargetTag : IComponentData { }


[UpdateInGroup(typeof(InitializationSystemGroup))]
public partial struct CameraTargetInitializeSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<InitializedCameraTargetTag>();
    }

    public void OnUpdate(ref SystemState state)
    {
        if (CameraTargetSingleton.instance == null) { return; }

        var ecb = new EntityCommandBuffer(state.WorldUpdateAllocator);
        var cameraTargetTransform = CameraTargetSingleton.instance.transform;
        foreach (var (cameraTarget, entity)
            in SystemAPI.Query<
                RefRW<CameraTarget>>().WithAll<InitializedCameraTargetTag, PlayerTag>()
                .WithEntityAccess())
        {
            cameraTarget.ValueRW.cameraTransform = cameraTargetTransform;
            ecb.RemoveComponent<InitializedCameraTargetTag>(entity);
        }

        ecb.Playback(state.EntityManager);
    }
}


[UpdateAfter(typeof(TransformSystemGroup))]
public partial struct CameraTargetFollowSystem : ISystem
{
    public void OnUpdate(ref SystemState state)
    {
        if (CameraTargetSingleton.instance == null) { return; }
        foreach (var (transform, cameraTarget)
            in SystemAPI.Query<
                LocalToWorld,
                CameraTarget>().WithNone<InitializedCameraTargetTag>())
        {
            cameraTarget.cameraTransform.Value.transform.position = transform.Position;
        }
    }
}
