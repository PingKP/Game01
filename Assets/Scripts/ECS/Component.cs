using UnityEngine;
using System.Collections.Generic;

public class Component
{
    public uint ownerId;
}


public interface IComponentManager
{
    void OnEntityDestroyed(uint entityId);
}


public class ComponentManager<T> : IComponentManager where T : Component
{
    protected Dictionary<uint, T> componentPool = new Dictionary<uint, T>();

    public void AddComponent(uint entityId, T component)
    {
        if (componentPool.ContainsKey(entityId))
            componentPool[entityId] = component;
        else
            componentPool.Add(entityId, component);
        component.ownerId = entityId;
    }

    public void RemoveComponent(uint entityId)
    {
        if (componentPool.ContainsKey(entityId))
            componentPool.Remove(entityId);
    }

    public T GetComponent(uint entityId)
    {
        if (componentPool.TryGetValue(entityId, out T component))
            return component;
        return null;
    }

    public bool HasComponent(uint entityId)
    {
        return componentPool.ContainsKey(entityId);
    }

    public void OnEntityDestroyed(uint entityId)
    {
        RemoveComponent(entityId);
    }
}