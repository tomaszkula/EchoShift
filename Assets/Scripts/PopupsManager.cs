public class PopupsManager : BaseManager
{
    private Popup[] popups = new Popup[0];

    private void Awake()
    {
        popups = GetComponentsInChildren<Popup>(true);
    }

    public override void Initialize()
    {
        for (int i = 0; i < popups.Length; i++)
        {
            popups[i].Register(this);
        }

        base.Initialize();
    }

    public override void Deinitialize()
    {
        for (int i = 0; i < popups.Length; i++)
        {
            popups[i].Unregister();
        }

        base.Deinitialize();
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
