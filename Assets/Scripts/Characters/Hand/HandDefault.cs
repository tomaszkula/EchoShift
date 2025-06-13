using System;
using UnityEngine;

public class HandDefault : MonoBehaviour, IHand
{
    [Header("References")]
    [SerializeField] private Transform rightHand = null;
    [SerializeField] private Transform leftHand = null;

    private IFace iFace = null;

    private Transform hand = null;
    public Transform Hand
    {
        get => hand;
        set
        {
            hand = value;
            OnHandChanged?.Invoke(hand);
        }
    }

    public event Action<Transform> OnHandChanged = null;

    private void Awake()
    {
        iFace = GetComponent<IFace>();
    }

    private void OnEnable()
    {
        if (iFace != null)
            iFace.OnFaceDirectionChanged += OnFaceDirectionChanged;
    }

    private void OnDisable()
    {
        if (iFace != null)
            iFace.OnFaceDirectionChanged -= OnFaceDirectionChanged;
    }

    private void OnFaceDirectionChanged(Direction direction)
    {
        Hand = direction switch
        {
            Direction.Right => rightHand,
            Direction.Left => leftHand,
            _ => rightHand
        };
    }
}
