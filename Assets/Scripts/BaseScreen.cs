using UnityEngine;

public class BaseScreen : MonoBehaviour
{
    [SerializeField] protected GameObject container = null;

    public MainMenuUI mainMenuUI { get; protected set; }

    public virtual void Initialize(MainMenuUI mmu)
    {
        mainMenuUI = mmu;
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
