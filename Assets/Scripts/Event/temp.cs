// ============================================================
// EventBus.cs
// 靜態全域 EventBus，任何地方都可以發送與監聽事件
// ============================================================

using System;
using System.Collections.Generic;
using UnityEngine;

public static class EventBus
{
    private static readonly Dictionary<Type, List<Delegate>> handlers = new();

    public static void Subscribe<T>(Action<T> handler)
    {
        var type = typeof(T);
        if (!handlers.ContainsKey(type))
            handlers[type] = new List<Delegate>();

        handlers[type].Add(handler);
    }


    public static void Unsubscribe<T>(Action<T> handler)
    {
        var type = typeof(T);
        if (handlers.ContainsKey(type))
            handlers[type].Remove(handler);
    }


    public static void Publish<T>(T eventObj)
    {
        var type = typeof(T);
        if (!EventBus.handlers.ContainsKey(type)) return;

        var handlers = new List<Delegate>(EventBus.handlers[type]);
        foreach (var handler in handlers)
            ((Action<T>)handler)(eventObj);
    }


    public static void Clear()
    {
        handlers.Clear();
    }
}




public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Slider _slider;

    private void OnEnable()
    {
        EventBus.Subscribe<AfterTakeDamageEvent>(OnAfterTakeDamage);
        EventBus.Subscribe<AfterHealEvent>(OnAfterHeal);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<AfterTakeDamageEvent>(OnAfterTakeDamage);
        EventBus.Unsubscribe<AfterHealEvent>(OnAfterHeal);
    }
        
    private void OnAfterTakeDamage(AfterTakeDamageEvent e)
    {
        if (e.target != gameObject) return;
        UpdateSlider(e.remainingHealth);
    }

    private void OnAfterHeal(AfterHealEvent e)
    {
        if (e.target != gameObject) return;
        UpdateSlider(e.remainingHealth);
    }

    private void UpdateSlider(int currentHealth)
    {
        var health = GetComponent<HealthComponent>();
        _slider.value = (float)currentHealth / health.maxHealth;
    }
}