using UnityEngine;

public class ClimbInput : MonoBehaviour
{
    private bool isClimbRequested = false;
    private Vector2 climbDirection = Vector2.zero;

    private IClimb iClimb = null;

    private void Awake()
    {
        iClimb = GetComponent<IClimb>();
    }

    private void OnEnable()
    {
        Manager.Instance.GetManager<InputsManager>().OnMove += OnMoveAction;
    }

    private void Update()
    {
        Climb();
    }

    private void OnDisable()
    {
        if (Manager.IsInitialized)
        {
            Manager.Instance.GetManager<InputsManager>().OnMove -= OnMoveAction;
        }
    }

    private void OnMoveAction(Vector2 direction)
    {
        isClimbRequested = true;
        climbDirection = direction;
    }

    private void Climb()
    {
        if (!isClimbRequested)
            return;

        isClimbRequested = false;

        iClimb?.Climb(climbDirection);
    }
}
