using System;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    [Serializable]
    public class Data
    {
        public ScreenType screenType = ScreenType.LevelSelect;
        public BaseScreen screen = null;
    }

    public enum ScreenType
    {
        None,
        MainMenu,
        LevelSelect,
    }

    [Header("References")]
    [SerializeField] private List<Data> screensData = new List<Data>();

    private Dictionary<ScreenType, Data> screensDictionary = new Dictionary<ScreenType, Data>();

    public ScreenType currentScreenType { get; private set; } = ScreenType.None;
    public Data currentScreenData { get; private set; } = null;

    private void Awake()
    {
        for (int i = 0; i < screensData.Count; i++)
        {
            screensData[i].screen.Initialize(this);
            screensDictionary.Add(screensData[i].screenType, screensData[i]);
        }
    }

    private void Start()
    {
        SelectScreen(ScreenType.MainMenu);
    }

    public void SelectScreen(ScreenType screenType)
    {
        if (currentScreenData != null)
        {
            currentScreenData.screen?.Hide();
        }

        currentScreenType = screenType;
        currentScreenData = GetScreen(screenType);

        if (currentScreenData != null)
        {
            currentScreenData.screen?.Show();
        }
    }

    public Data GetScreen(ScreenType screenType)
    {
        if(screensDictionary.TryGetValue(screenType, out Data screenData))
        {
            return screenData;
        }

        return null;
    }
}
