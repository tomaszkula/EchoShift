using UnityEngine;

public class DamageableDefault : MonoBehaviour, IDamageable
{
    private IHealth iHealth = null;

    private void Awake()
    {
        iHealth = GetComponent<IHealth>();
    }

    public void Damage(DamageType damageType, float damage)
    {
        if (iHealth != null)
        {
            iHealth.TakeHealth(damage);
        }
    }
}
