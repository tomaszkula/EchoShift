using UnityEngine;

public class Lever : MonoBehaviour, IActivatable
{
    [Header("Settings")]
    [SerializeField] private Interactable target = null;

    [Header("References")]
    [SerializeField] private Animator animator = null;
    [SerializeField] private BoxCollider2D collider = null;

    private bool isActivated = false;

    private const string ANIMATOR_IS_ACTIVATED_BOOL_KEY = "IsActivated";

    public void Activate()
    {
        ToggleActivation();
    }

    public void Deactivate()
    {
        ToggleActivation();
    }

    private void ToggleActivation()
    {
        isActivated = !isActivated;

        if (isActivated)
        {
            animator.SetBool(ANIMATOR_IS_ACTIVATED_BOOL_KEY, true);

            target?.Interact();
        }
        else
        {
            animator.SetBool(ANIMATOR_IS_ACTIVATED_BOOL_KEY, false);

            target?.Deinteract();
        }
    }
}
