using UnityEngine;
using UnityEngine.UI;

public class PopupPause : BasePopup
{
    [Header("References")]
    [SerializeField] private Button resumeBtn = null;
    [SerializeField] private Button mainMenuBtn = null;
    [SerializeField] private Button settingsBtn = null;
    [SerializeField] private Button quitBtn = null;

    protected override void OnEnable()
    {
        base.OnEnable();

        resumeBtn.onClick.AddListener(OnResumeBtnClicked);
        mainMenuBtn.onClick.AddListener(OnMainMenuBtnClicked);
        settingsBtn.onClick.AddListener(OnSettingsBtnClicked);
        quitBtn.onClick.AddListener(OnQuitBtnClicked);
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        resumeBtn.onClick.RemoveListener(OnResumeBtnClicked);
        mainMenuBtn.onClick.RemoveListener(OnMainMenuBtnClicked);
        settingsBtn.onClick.RemoveListener(OnSettingsBtnClicked);
        quitBtn.onClick.RemoveListener(OnQuitBtnClicked);
    }

    public override void Show()
    {
        base.Show();

        Manager.Instance.GetManager<PauseManager>().Pause();
    }

    public override void Hide()
    {
        base.Hide();

        Manager.Instance.GetManager<PauseManager>().Resume();
    }

    private void OnResumeBtnClicked()
    {
        Manager.Instance.GetManager<AudioManager>().PlayButtonClickSound();

        Hide();
    }

    private async void OnMainMenuBtnClicked()
    {
        Manager.Instance.GetManager<AudioManager>().PlayButtonClickSound();

        await Manager.Instance.GetManager<ScenesManager>().LoadMainMenuAsync();

        Hide();
    }

    private void OnSettingsBtnClicked()
    {
        Manager.Instance.GetManager<AudioManager>().PlayButtonClickSound();

        Manager.Instance.GetManager<PopupsManager>().GetPopup<PopupSettings>().Show();
    }

    private void OnQuitBtnClicked()
    {
        Manager.Instance.GetManager<AudioManager>().PlayButtonClickSound();

        Application.Quit();
    }
}
