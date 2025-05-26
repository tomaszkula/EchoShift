using Game;
using UnityEngine;

public class FaceFlipper : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer = null;

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

    private void OnMove(Vector2 direction)
    {
        if (direction.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (direction.x < 0)
        {
            spriteRenderer.flipX = true;
        }
    }
}
