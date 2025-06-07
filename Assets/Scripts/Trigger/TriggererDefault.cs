using UnityEngine;

public class TriggererDefault : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject target = collision.gameObject;
        if (collision.TryGetComponent(out IOwner iOwner))
        {
            target = iOwner.Owner;
        }

        target.GetComponent<ITriggerable>()?.Trigger();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject target = collision.gameObject;
        if (collision.TryGetComponent(out IOwner iOwner))
        {
            target = iOwner.Owner;
        }

        target.GetComponent<ITriggerable>()?.UnTrigger();
    }
}
