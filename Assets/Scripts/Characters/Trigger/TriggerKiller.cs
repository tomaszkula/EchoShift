using UnityEngine;

public class TriggerKiller : MonoBehaviour
{
    private IOwner iOwner = null;
    private IAttacker iAttacker = null;
    private IKillable iKillable = null;

    private void Awake()
    {
        iOwner = GetComponent<IOwner>();
        iAttacker = iOwner.Owner.GetComponent<IAttacker>();
        iKillable = iOwner.Owner.GetComponent<IKillable>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject target = collision.gameObject;
        if(target.TryGetComponent(out IOwner iOwner))
            target = iOwner.Owner;

        if (target == this.iOwner.Owner)
            return;

        if (iAttacker != null && target == iAttacker.Attacker)
            return;

        iKillable?.Kill(); 
    }
}
