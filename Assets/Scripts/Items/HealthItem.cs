using UnityEngine;

public class HealthItem : MonoBehaviour, IPickable
{
    [Header("Settings")]
    [SerializeField] private float healthToHeal = 10f;

    private IKillable iKillable = null;

    public float HealthToHeal => healthToHeal;

    private void Awake()
    {
        iKillable = GetComponent<IKillable>();
    }

    public void Pick(IPicker iPicker)
    {
        if((iPicker as MonoBehaviour).TryGetComponent(out IHealth iHealth))
        {
            iHealth.Heal(HealthToHeal);
        }

        iKillable?.Kill();
    }
}
