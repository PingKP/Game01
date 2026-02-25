using UnityEngine;

public class ArmorComponentSystem : ComponentSystem
{
    public override void OnEnable()
    {
        EventBus.Subscribe<BeforeTakeDamageEvent>(OnBeforeTakeDamage);
    }

    public override void OnDisable()
    {
        EventBus.Unsubscribe<BeforeTakeDamageEvent>(OnBeforeTakeDamage);
    }

    private void OnBeforeTakeDamage(BeforeTakeDamageEvent e)
    {
        var armorComponent = e.target.GetComponent<ArmorComponent>();
        if (armorComponent == null) return;

        e.modifiableDamage = Mathf.Max(0, e.modifiableDamage - armorComponent.armorValue);
    }
}
