using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class SpriteAtlasesManager : BaseManager
{
    [Serializable]
    public class Data
    {
        public SpriteAtlasType type = null;
        public SpriteAtlas spriteAtlas = null;
    }

    [Header("Settings")]
    [SerializeField] private Data[] spriteAtlasesData = new Data[0];

    [Header("References")]
    [SerializeField] private SpriteAtlasType uiSAT = null;

    private Dictionary<SpriteAtlasType, Data> spriteAtlases = new Dictionary<SpriteAtlasType, Data>();

    protected override void InitializeInternal()
    {
        base.InitializeInternal();

        for (int i = 0; i < spriteAtlasesData.Length; i++)
        {
            Data data = spriteAtlasesData[i];
            spriteAtlases.Add(data.type, data);
        }
    }

    protected override void DeinitializeInternal()
    {
        base.DeinitializeInternal();

        spriteAtlases.Clear();
    }

    public const string UI_SPRITE_ICON_MENU_NAME = "sp_Icon_Menu";
    public const string UI_SPRITE_ICON_PLAY_NAME = "sp_Icon_Play";
    public const string UI_SPRITE_ICON_RECORD_NAME = "sp_Icon_Record";
    public const string UI_SPRITE_ICON_STOP_NAME = "sp_Icon_Stop";
    public const string UI_SPRITE_ICON_VOLUME_NAME = "sp_Icon_Volume";
    public const string UI_SPRITE_ICON_STAR_NAME = "sp_Icon_Star";
    public Sprite GetUiSprite(string spriteName)
    {
        return spriteAtlases[uiSAT].spriteAtlas.GetSprite(spriteName);
    }
}
