using UnityEngine;

public class MoveInput : MonoBehaviour
{
    private bool isMoveRequested = false;
    private Vector2 moveDirection = Vector2.zero;

    private IMove iMove = null;

    private void Awake()
    {
        iMove = GetComponent<IMove>();
    }

    private void OnEnable()
    {
        Manager.Instance.GetManager<InputsManager>().OnMove += OnMoveAction;
    }

    private void Update()
    {
        Move();
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
        isMoveRequested = true;
        moveDirection = direction;
    }

    private void Move()
    {
        if (!isMoveRequested)
            return;

        isMoveRequested = false;

        iMove?.Move(moveDirection);
    }
}