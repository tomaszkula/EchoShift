using System;
using UnityEngine;

public class TriggererDefault : MonoBehaviour, ITriggerer
{
    public event Action<ITriggerable> OnTriggered = null;
    public event Action<ITriggerable> OnUntriggered = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject target = collision.gameObject;
        if (target.TryGetComponent(out IOwner iOwner))
            target = iOwner.Owner;

        if (target.TryGetComponent(out ITriggerable iTriggerable))
            Trigger(iTriggerable);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject target = collision.gameObject;
        if (target.TryGetComponent(out IOwner iOwner))
            target = iOwner.Owner;

        if (target.TryGetComponent(out ITriggerable iTriggerable))
            Untrigger(iTriggerable);
    }

    public void Trigger(ITriggerable iTriggerable)
    {
        iTriggerable.Trigger();

        OnTriggered?.Invoke(iTriggerable);
    }

    public void Untrigger(ITriggerable iTriggerable)
    {
        iTriggerable?.Untrigger();

        OnUntriggered?.Invoke(iTriggerable);
    }
}
