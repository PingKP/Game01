using UnityEngine;
using Unity.Entities;
using Unity.Collections;

public class UnitSeletedSystem : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var mouseWorldPosition = MouseWorldPosition.instance.GetWorldPosition();

            var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            var entityQuery = new EntityQueryBuilder(Allocator.Temp)
                        .WithAll<MoveTarget, SelectedFlag, ControllableFlag>()
                        .Build(entityManager);
            
            var entityArray = entityQuery.ToEntityArray(Allocator.Temp);
            var moveTargetArray = entityQuery.ToComponentDataArray<MoveTarget>(Allocator.Temp);
            for (int i = 0; i < moveTargetArray.Length; i++)
            {
                var moveTarget = moveTargetArray[i];
                moveTarget.targetPosition = mouseWorldPosition;
                moveTargetArray[i] = moveTarget;
            }
            entityQuery.CopyFromComponentDataArray(moveTargetArray);
        }
    }
}
