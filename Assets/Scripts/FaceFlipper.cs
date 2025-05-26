using Game;
using UnityEngine;

public class FaceFlipper : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private bool flipOnRight = false;

    [Header("References")]
    [SerializeField] private SpriteRenderer characterSR = null;

    private IOnMove _onMove = null;

    private void Awake()
    {
        _onMove = GetComponent<IOnMove>();
    }

    private void OnEnable()
    {
        if (_onMove != null)
        {
            _onMove.OnMove += OnMove;
        }
    }

    private void OnDisable()
    {
        if (_onMove != null)
        {
            _onMove.OnMove -= OnMove;
        }
    }

    private void OnMove(Vector2 moveDirection)
    {
        if (moveDirection.x > 0)
        {
            characterSR.flipX = flipOnRight;
        }
        else if (moveDirection.x < 0)
        {
            characterSR.flipX = !flipOnRight;
        }
    }
}
