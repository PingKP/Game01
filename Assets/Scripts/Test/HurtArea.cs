using UnityEngine;

public class HurtArea : MonoBehaviour
{
    public int damage;

    public void OnTriggerEnter(Collider Other)
    {
        //Debug.Log("Entered!!");
        Debug.Log($"{ Other.gameObject.name} has entered");
        EventBus.Publish(new AfterTakeDamageEvent
        {
            target = Other.gameObject,
            damageDealt = damage,
            remainingHealth = 0,
            damageSource = "None"
        });
    }
}
