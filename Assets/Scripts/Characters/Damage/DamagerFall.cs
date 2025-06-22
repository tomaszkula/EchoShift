using UnityEngine;

public class DamagerFall : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Vector2 kinematicEnergyRange = Vector2.zero;
    [SerializeField] private Vector2 damageToKinematicEnergy = Vector2.zero;

    private IFeet iFeet = null;
    private IDamageable iDamageable = null;
    new private Rigidbody2D rigidbody = null;

    private void Awake()
    {
        iFeet = GetComponent<IFeet>();
        iDamageable = GetComponent<IDamageable>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        CheckFallDamage();
    }

    private void CheckFallDamage()
    {
        if (!iFeet.IsGrounded())
            return;

        float kinematicEnergy = 0.5f * rigidbody.mass * Mathf.Pow(rigidbody.linearVelocityY, 2);
        if (kinematicEnergy < kinematicEnergyRange.x)
            return;

        kinematicEnergy = Mathf.Clamp(kinematicEnergy, kinematicEnergyRange.x, kinematicEnergyRange.y);

        float t = (kinematicEnergy - kinematicEnergyRange.x) / (kinematicEnergyRange.y - kinematicEnergyRange.x);
        float damage = Mathf.Lerp(damageToKinematicEnergy.x, damageToKinematicEnergy.y, t);
        iDamageable.Damage(null, damage);
    }
}
