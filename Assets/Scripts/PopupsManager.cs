public class PopupsManager : BaseGameManager
{
    private Popup[] popups = new Popup[0];

    private void Awake()
    {
        popups = GetComponentsInChildren<Popup>(true);
    }

    protected override void InitializeInternal()
    {
        base.InitializeInternal();

        for (int i = 0; i < popups.Length; i++)
        {
            popups[i].Register(this);
        }
    }

    protected override void DeinitializeInternal()
    {
        base.DeinitializeInternal();

        for (int i = 0; i < popups.Length; i++)
        {
            popups[i].Unregister();
        }
    }

    public T GetPopup<T>() where T : Popup
    {
        foreach (var popup in popups)
        {
            if (popup is T typedPopup)
            {
                return typedPopup;
            }
        }

        return null;
    }
}
