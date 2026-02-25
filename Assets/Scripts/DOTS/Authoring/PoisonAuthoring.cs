using UnityEngine;
using Unity.Entities;

public struct Poison : IComponentData
{
    public float valuePerSecond;
    public float timeLeft;
}

public class PoisonAuthoring : MonoBehaviour
{
    public float value;
    public float duration;

    class PoisonAuthoringBaker : Baker<PoisonAuthoring>
    {
        public override void Bake(PoisonAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new Poison
            {
                valuePerSecond = authoring.value,
                timeLeft = authoring.duration
            });
        }
    }
}

