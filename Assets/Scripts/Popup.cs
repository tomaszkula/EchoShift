using UnityEngine;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject container = null;
    [SerializeField] private Button fullScreenCloseButton = null;

    private PopupsManager popupsManager = null;

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
    }

    public virtual void Hide()
    {
        container.SetActive(false);
    }

    public void Register(PopupsManager manager)
    {
        popupsManager = manager;
    }

    public void Unregister()
    {
        popupsManager = null;
    }
}
