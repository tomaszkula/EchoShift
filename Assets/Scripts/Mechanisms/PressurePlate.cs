using UnityEngine;


public class PressurePlate : MonoBehaviour, ITriggerable
{
    [Header("Settings")]
    [SerializeField] private Interactable target = null;

    [Header("References")]
    [SerializeField] private Animator animator = null;
    [SerializeField] private BoxCollider2D collider = null;

    private const string ANIMATOR_IS_PRESSED_BOOL_KEY = "IsPressed";

    public void Trigger()
    {
        animator.SetBool(ANIMATOR_IS_PRESSED_BOOL_KEY, true);

        target?.Interact();
    }

    public void UnTrigger()
    {
        animator.SetBool(ANIMATOR_IS_PRESSED_BOOL_KEY, false);

        target?.Deinteract();
    }
}