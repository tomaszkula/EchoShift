using UnityEngine;

public class DamageableDefault : MonoBehaviour, IDamageable
{
    private IHealth iHealth = null;

    private void Awake()
    {
        iHealth = GetComponent<IHealth>();
    }

    public bool Damage(DamageType damageType, float damage)
    {
        if (iHealth != null)
        {
            return iHealth.TakeHealth(damage);
        }

        return false;
    }
}
