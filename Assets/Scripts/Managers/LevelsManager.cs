using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsManager : BaseManager
{
    [Header("Settings")]
    [SerializeField] private List<LevelData> levelsData = new List<LevelData>();

    public List<LevelData> LevelsData => levelsData;
    public bool IsLevelLoaded { get; private set; } = false;

    public async UniTask Load(LevelData levelData)
    {
        IsLevelLoaded = false;

        await Manager.Instance.GetManager<ScenesManager>().LoadAsync(levelData.GameSceneName, LoadSceneMode.Single);

        for (int i = 0; i < levelData.LevelSceneNames.Count; i++)
        {
            await Manager.Instance.GetManager<ScenesManager>().LoadAsync(levelData.LevelSceneNames[i], LoadSceneMode.Additive);
        }

        IsLevelLoaded = true;
    }

    public async UniTask Load()
    {
        await Load(levelsData[0]);
    }
}
