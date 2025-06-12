using UnityEngine;

public class HealthItem : MonoBehaviour, IPickable
{
    public enum ActionType
    {
        Heal,
        TakeHealth,
    }

    [Header("Settings")]
    [SerializeField] private ActionType actionType = ActionType.Heal;
    [SerializeField] private float healthValue = 10f;

    private IKillable iKillable = null;

    public float HealthValue => healthValue;

    private void Awake()
    {
        iKillable = GetComponent<IKillable>();
    }

    public void Pick(IPicker iPicker)
    {
        bool result = false;
        if((iPicker as MonoBehaviour).TryGetComponent(out IHealth iHealth))
        {
            switch(actionType)
            {
                case ActionType.Heal:
                    result = iHealth.Heal(HealthValue);
                    break;

                case ActionType.TakeHealth:
                    result = iHealth.TakeHealth(HealthValue);
                    break;
            }
        }

        if(result)
            iKillable?.Kill();
    }
}
