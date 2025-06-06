using UnityEngine;
using UnityEngine.UI;

public class BasePopup : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject container = null;
    [SerializeField] private Button fullScreenCloseButton = null;

    private PopupsManager popupsManager = null;

    private void Awake()
    {
        popupsManager = Manager.Instance.GetManager<PopupsManager>();

        popupsManager.Register(this);
    }

    protected virtual void OnEnable()
    {
        fullScreenCloseButton?.onClick.AddListener(Hide);
    }

    protected virtual void OnDisable()
    {
        fullScreenCloseButton?.onClick.RemoveListener(Hide);
    }

    private void OnDestroy()
    {
        popupsManager.Unregister(this);
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
