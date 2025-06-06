using System.Collections.Generic;

public class PopupsManager : BaseManager
{
    private List<BasePopup> popups = new List<BasePopup>();

    public void Register(BasePopup popup)
    {
        popup.OnShowed += OnPopupShowed;
        popup.OnHidden += OnPopupHidden;

        popups.Add(popup);
    }

    public void Unregister(BasePopup popup)
    {
        popup.OnShowed -= OnPopupShowed;
        popup.OnHidden -= OnPopupHidden;

        popups.Remove(popup);
    }

    private void OnPopupShowed(BasePopup popup)
    {
        popup.transform.SetParent(transform);
        popup.transform.SetAsLastSibling();
    }

    private void OnPopupHidden(BasePopup popup)
    {
        popup.transform.SetParent(popup.DefaultParent);
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
