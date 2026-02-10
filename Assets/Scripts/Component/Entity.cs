using UnityEngine;
using System.Collections.Generic;

public class Entity
{
    public int id;
}


public class EntityManager
{
    private int currentId = 0;
    private Dictionary<int, Entity> entityPool = new Dictionary<int, Entity>();
    private Queue<int> recycledIds = new Queue<int>();

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

    private int GenerateId()
    {
        if (recycledIds.Count > 0)
        {
            return recycledIds.Dequeue();
        }
        return currentId++;
    }

    private void RecycleId(int id)
    {
        if (!recycledIds.Contains(id))
        {
            recycledIds.Enqueue(id);
        }
    }

    public Entity CreateEntity()
    {
        int newId = GenerateId();
        Entity entity = new Entity
        {
            id = newId
        };

        entityPool.Add(newId, entity);
        return entity;
    }

    public void DestroyEntity(int entityId)
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

    public Entity GetEntity(int entityId)
    {
        if (entityPool.TryGetValue(entityId, out Entity entity))
        {
            return entity;
        }
        return null;
    }

    public bool HasEntity(int entityId)
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
