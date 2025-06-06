using System;
using UnityEngine;
using UnityEngine.UI;

public class BasePopup : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject container = null;
    [SerializeField] private Button fullScreenCloseButton = null;

    public Transform DefaultParent { get; private set; } = null;

    public event Action<BasePopup> OnShowed = null;
    public event Action<BasePopup> OnHidden = null;

    private void Awake()
    {
        DefaultParent = transform.parent;
    }

    protected virtual void OnEnable()
    {
        fullScreenCloseButton?.onClick.AddListener(Hide);
    }

    protected virtual void OnDisable()
    {
        fullScreenCloseButton?.onClick.RemoveListener(Hide);
    }

    public virtual void Show()
    {
        container.SetActive(true);

        OnShowed?.Invoke(this);
    }

    public virtual void Hide()
    {
        container.SetActive(false);

        OnHidden?.Invoke(this);
    }
}
