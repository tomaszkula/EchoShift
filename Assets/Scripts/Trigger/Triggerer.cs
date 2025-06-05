using UnityEngine;

public class Triggerer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponent<ITriggerable>()?.Trigger();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.GetComponent<ITriggerable>()?.UnTrigger();
    }
}
