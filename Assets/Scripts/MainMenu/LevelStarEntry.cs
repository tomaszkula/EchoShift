using System;
using UnityEngine;
using UnityEngine.UI;

public class LevelStarEntry : MonoBehaviour
{
    [Serializable]
    public class Data
    {
        public int Id = 0;
        public bool IsUnlocked = false;
    }    

    [Header("References")]
    [SerializeField] private Image iconImage = null;

    public Data CurrentData { get; private set; }

    public void Init(Data data)
    {
        CurrentData = data;

        RefreshIconImage();
    }

    private void RefreshIconImage()
    {
        iconImage.sprite = Manager.Instance.GetManager<SpriteAtlasesManager>().GetUiSprite(SpriteAtlasesManager.UI_SPRITE_ICON_STAR_NAME);
        iconImage.color = CurrentData.IsUnlocked ? Color.yellow : Color.white;
    }
}
