using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [Header("References")]
    [SerializeField] private Animator animator = null;
    [SerializeField] private Collider2D collider = null;

    private const string ANIMATOR_IS_OPENED_BOOL_KEY = "IsOpened";

    public void Interact()
    {
        animator.SetBool(ANIMATOR_IS_OPENED_BOOL_KEY, true);

        collider.enabled = false;
    }

    public void Deinteract()
    {
        animator.SetBool(ANIMATOR_IS_OPENED_BOOL_KEY, false);

        collider.enabled = true;
    }
}