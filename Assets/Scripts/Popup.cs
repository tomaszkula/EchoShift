using UnityEngine;

public class Popup : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject container = null;

    private PopupsManager popupsManager = null;

    public void Register(PopupsManager manager)
    {
        popupsManager = manager;
    }

    public void Unregister()
    {
        popupsManager = null;
    }

    public virtual void Show()
    {
        container.SetActive(true);
    }

    public virtual void Hide()
    {
        container.SetActive(false);
    }
}
