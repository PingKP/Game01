using UnityEngine;

public class HealthComponentSystem : ComponentSystem
{
    
    public override void OnEnable()
    {
        EventBus.Subscribe<OnAttackEvent>(TakeDamage);
        EventBus.Subscribe<TakingDamageEvent>(TakeDamageSample);
        //EventBus.Subscribe<AfterHealEvent>(Heal);
        EventBus.Subscribe<AfterDeathEvent>(TriggerDeath);
    }

    public override void OnDisable()
    {
        EventBus.Unsubscribe<OnAttackEvent>(TakeDamage);
        EventBus.Subscribe<TakingDamageEvent>(TakeDamageSample);
        //EventBus.Unsubscribe<AfterHealEvent>(Heal);
        EventBus.Unsubscribe<AfterDeathEvent>(TriggerDeath);
    }


    public void TakeDamageSample(TakingDamageEvent e)
    {
        var healthComponent = e.target.GetComponent<HealthComponent>();
        if (healthComponent == null) return;

        healthComponent.currentHealth -= e.damageDealt;
        if (healthComponent.currentHealth <= 0)
        {
            EventBus.Publish(new BeforeDeathEvent
            {
                target = e.target,
                cancelled = false,
            });
        }
    }

    public void TakeDamage(OnAttackEvent e)
    {

        //var skillTriggerEvent = new OnSkillTriggerEvent
        //{
        //    user = e.attacker,
        //    usedSkill = e.skill,
        //    damageDealt = 1,
        //    damageType = ""
        //};

        //EventBus.Publish(skillTriggerEvent);


        //var healthComponent = e.target.GetComponent<HealthComponent>();
        //if (healthComponent == null) return;

        //healthComponent.currentHealth -= e.damageDealt;
        //if (healthComponent.currentHealth <= 0)
        //{
        //    EventBus.Publish(new BeforeDeathEvent
        //        {
        //            target = e.target,
        //            cancelled = false,
        //        });
        //}
        
    }

    /*
    public void Heal(AfterHealEvent e)
    {
      
        var beforeEvent = new BeforeHealEvent
        {
            Target = gameObject,
            HealAmount = amount,
            Cancelled = false
        };
        EventBus.Publish(beforeEvent);

        if (beforeEvent.Cancelled) return;

        int actualHeal = Mathf.Min(beforeEvent.HealAmount, _maxHealth - _currentHealth);
        _currentHealth += actualHeal;

        // Hook 4：治療後
        EventBus.Publish(new AfterHealEvent
        {
            Target = gameObject,
            HealedAmount = actualHeal,
            RemainingHealth = _currentHealth
        });
    }
    */

    private void TriggerDeath(AfterDeathEvent e)
    {
        Destroy(e.target); 
    }
}