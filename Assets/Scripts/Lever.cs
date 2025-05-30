using UnityEngine;

public class Lever : MonoBehaviour, IActivatable
{
    [Header("References")]
    [SerializeField] private Animator animator = null;

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
        }
        else
        {
            animator.SetBool(ANIMATOR_IS_ACTIVATED_BOOL_KEY, false);
        }
    }
}
