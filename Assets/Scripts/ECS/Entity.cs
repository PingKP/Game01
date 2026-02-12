using UnityEngine;
using System.Collections.Generic;

public class Entity
{
    public uint id;
}


public class EntityManager
{
    private uint currentId = 0;
    private Dictionary<uint, Entity> entityPool = new Dictionary<uint, Entity>();
    private Queue<uint> recycledIds = new Queue<uint>();

    private List<IComponentManager> componentManagers = new List<IComponentManager>();

    public void RegisterComponentManager(IComponentManager componentManager)
    {
        if (!componentManagers.Contains(componentManager))
        {
            componentManagers.Add(componentManager);
        }
    }

    public void UnregisterComponentManager(IComponentManager componentManager)
    {
        componentManagers.Remove(componentManager);
    }

    private uint GenerateId()
    {
        if (recycledIds.Count > 0)
        {
            return recycledIds.Dequeue();
        }
        return currentId++;
    }

    private void RecycleId(uint id)
    {
        if (!recycledIds.Contains(id))
        {
            recycledIds.Enqueue(id);
        }
    }

    public Entity CreateEntity()
    {
        uint newId = GenerateId();
        Entity entity = new Entity
        {
            id = newId
        };

        entityPool.Add(newId, entity);
        return entity;
    }

    public void DestroyEntity(uint entityId)
    {
        if (entityPool.ContainsKey(entityId))
        {
            foreach (var manager in componentManagers)
            {
                manager.OnEntityDestroyed(entityId);
            }
            entityPool.Remove(entityId);
            RecycleId(entityId);
        }
    }

    public Entity GetEntity(uint entityId)
    {
        if (entityPool.TryGetValue(entityId, out Entity entity))
        {
            return entity;
        }
        return null;
    }

    public bool HasEntity(uint entityId)
    {
        return entityPool.ContainsKey(entityId);
    }

    public int GetEntityCount()
    {
        return entityPool.Count;
    }

    public void Clear()
    {
        entityPool.Clear();
        currentId = 0;
        recycledIds.Clear();
    }
}
