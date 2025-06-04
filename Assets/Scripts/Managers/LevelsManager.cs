using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

public class LevelsManager : BaseManager
{
    [Header("Settings")]
    [SerializeField] private List<LevelData> levelsData = new List<LevelData>();

    public List<LevelData> LevelsData => levelsData;
    public bool IsLevelLoaded { get; private set; } = false;

    public async UniTask Load(LevelData levelData)
    {
        IsLevelLoaded = false;

        await Manager.Instance.GetManager<ScenesManager>().LoadMainScene(levelData.GameSceneAsset.name);

        for (int i = 0; i < levelData.LevelSceneAssets.Count; i++)
        {
            await Manager.Instance.GetManager<ScenesManager>().LoadAdditionalScene(levelData.LevelSceneAssets[i].name);
        }

        IsLevelLoaded = true;
    }

    public async UniTask Load()
    {
        await Load(levelsData[0]);
    }
}
