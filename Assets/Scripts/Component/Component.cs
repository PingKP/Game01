using UnityEngine;
using System.Collections.Generic;

public class Component
{
    public int ownerId;
}


public interface IComponentManager
{
    void OnEntityDestroyed(int entityId);
}


public class ComponentManager<T> : IComponentManager where T : Component
{
    protected Dictionary<int, T> componentPool = new Dictionary<int, T>();

    public void AddComponent(int entityId, T component)
    {
        if (componentPool.ContainsKey(entityId))
            componentPool[entityId] = component;
        else
            componentPool.Add(entityId, component);
        component.ownerId = entityId;
    }

    public void RemoveComponent(int entityId)
    {
        if (componentPool.ContainsKey(entityId))
            componentPool.Remove(entityId);
    }

    public T GetComponent(int entityId)
    {
        if (componentPool.TryGetValue(entityId, out T component))
            return component;
        return null;
    }

    public bool HasComponent(int entityId)
    {
        return componentPool.ContainsKey(entityId);
    }

    public void OnEntityDestroyed(int entityId)
    {
        RemoveComponent(entityId);
    }
}