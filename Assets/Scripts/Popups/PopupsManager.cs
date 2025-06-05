using System.Collections.Generic;

public class PopupsManager : BaseManager
{
    private List<BasePopup> popups = new List<BasePopup>();

    public void Register(BasePopup popup)
    {
        popups.Add(popup);
    }

    public void Unregister(BasePopup popup)
    {
        popups.Remove(popup);
    }

    public T GetPopup<T>() where T : BasePopup
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
