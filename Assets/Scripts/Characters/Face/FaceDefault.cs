using System;
using UnityEngine;

public class FaceDefault : MonoBehaviour, IFace
{
    [Header("Settings")]
    [SerializeField] private Direction defaultDirection = Direction.Right;

    private IMove iMove = null;

    public Direction FaceDirection { get; set; } = Direction.Right;

    public event Action<Direction> OnFaceDirectionChanged = null;

    private void Awake()
    {
        iMove = GetComponent<IMove>();

        FaceDirection = defaultDirection;
    }

    private void Start()
    {
        OnFaceDirectionChanged?.Invoke(FaceDirection);
    }

    private void OnEnable()
    {
        if (iMove != null)
        {
            iMove.OnMove += OnMove;
        }
    }

    private void OnDisable()
    {
        if (iMove != null)
        {
            iMove.OnMove -= OnMove;
        }
    }

    private void OnMove(Vector2 moveDirection)
    {
        if (moveDirection.x > 0)
        {
            FaceDirection = Direction.Right;
            OnFaceDirectionChanged?.Invoke(FaceDirection);
        }
        else if (moveDirection.x < 0)
        {
            FaceDirection = Direction.Left;
            OnFaceDirectionChanged?.Invoke(FaceDirection);
        }
    }
}
