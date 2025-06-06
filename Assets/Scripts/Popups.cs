using UnityEngine;

public class Popups : MonoBehaviour
{
    private BasePopup[] popups = new BasePopup[0];

    private PopupsManager popupsManager = null;

    private void Awake()
    {
        popups = GetComponentsInChildren<BasePopup>();

        popupsManager = Manager.Instance.GetManager<PopupsManager>();

        for(int i = 0; i < popups.Length; i++)
        {
            popupsManager.Register(popups[i]);
        }
    }

    private void OnDestroy()
    {
        for (int i = 0; i < popups.Length; i++)
        {
            popupsManager.Unregister(popups[i]);

            if(popups[i] != null)
                Destroy(popups[i].gameObject);
        }
    }
}
