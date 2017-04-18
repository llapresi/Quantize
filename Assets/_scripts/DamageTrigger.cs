using UnityEngine;
using System.Collections;

// Class that deals damage to any colliding entity with a HealthComponent attached
public class DamageTrigger : MonoBehaviour
{

    [SerializeField]
    private int damageRate, enterDamage;

    [SerializeField]
    bool useTrigger = true;

    // Use Trigger Logic
    public void OnTriggerStay2D(Collider2D hit)
    {
        if (useTrigger)
        {
            HealthComponent collider = hit.gameObject.GetComponent<HealthComponent>();
            // Only do damage if colliding component has a HealthComponent
            if (collider)
            {
                collider.ChangeHealth((int)(-damageRate * Time.deltaTime));
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D hit)
    {
        if (useTrigger)
        {
            HealthComponent collider = hit.gameObject.GetComponent<HealthComponent>();
            // Only do damage if colliding component has a HealthComponent
            if (collider)
            {
                collider.ChangeHealth(-enterDamage);
            }
        }
    }

    public void OnCollisionEnter2D(Collision2D hit)
    {
        if (!useTrigger)
        {
            HealthComponent collider = hit.gameObject.GetComponent<HealthComponent>();
            // Only do damage if colliding component has a HealthComponent
            if (collider)
            {
                collider.ChangeHealth(-enterDamage);
            }
        }
    }

    public void OnCollisionStay2D(Collision2D hit)
    {
        if (!useTrigger)
        {
            HealthComponent collider = hit.gameObject.GetComponent<HealthComponent>();
            // Only do damage if colliding component has a HealthComponent
            if (collider)
            {
                collider.ChangeHealth((int)(-damageRate * Time.deltaTime));
            }
        }
    }
}